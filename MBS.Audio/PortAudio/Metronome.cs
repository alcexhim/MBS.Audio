using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UniversalEditor;
using UniversalEditor.Accessors;
using UniversalEditor.DataFormats.Multimedia.Audio.Waveform.MicrosoftWave;
using UniversalEditor.ObjectModels.Multimedia.Audio.Waveform;

namespace MBS.Audio.PortAudio
{
    public class Metronome
    {
        private string mvarAudioSamplePath = String.Empty;
        public string AudioSamplePath { get { return mvarAudioSamplePath; } set { mvarAudioSamplePath = value; } }

        private Dictionary<string, WaveformAudioObjectModel> waves = new Dictionary<string, WaveformAudioObjectModel>();

        private double mvarTempo = 120.0;
        public double Tempo { get { return mvarTempo; } set { mvarTempo = value; } }

        private System.Threading.Thread _thread = null;

        public bool IsPlaying
        {
            get { return (_thread != null && _thread.IsAlive); }
        }

        public void Start()
        {
            if (_thread != null)
            {
                _thread.Abort();
                _thread = null;
            }
            _thread = new System.Threading.Thread(_thread_ThreadStart);
            _thread.Start();
        }
        public void Stop()
        {
            if (_thread == null) return;
            _thread.Abort();
            _thread = null;
        }


        private void _thread_ThreadStart()
        {
            WaveformAudioObjectModel click = waves["Click"];
			using (AudioEngine ae = new PortAudioEngine())
			{
				AudioStream stream = new AudioStream((ae as PortAudioEngine).DefaultInput, 2, AudioSampleFormat.Int16, (ae as PortAudioEngine).DefaultOutput, click.Header.ChannelCount, AudioSampleFormat.Int16, click.Header.SampleRate * click.Header.ChannelCount, 0, AudioStreamFlags.ClipOff);

	            WaveformAudioObjectModel one = waves["One"];
	            WaveformAudioObjectModel two = waves["Two"];
	            WaveformAudioObjectModel three = waves["Three"];
	            WaveformAudioObjectModel four = waves["Four"];

	            WaveformAudioObjectModel[] countoffs = new WaveformAudioObjectModel[]
	            {
	                one,
	                null,
	                two,
	                null,
	                one,
	                two,
	                three,
	                four
	            };
	            
	            // 1/120 minutes per beat = 
	            double bpm = (1000 - (mvarTempo * ((double)500 / (double)120)));
	            int ms = (int)bpm;

	            int icountoff = 0;

				while (true)
				{
					// short[] rawSamples = (click.RawSamples.Clone() as short[]);
					short[] rawSamples = click.RawSamples.RawData;

					if (icountoff < countoffs.Length)
					{
						WaveformAudioObjectModel countoff = countoffs[icountoff];
						if (countoff != null)
						{
							rawSamples = countoff.RawSamples.RawData;
							/*
							// mix the countoff into the click
							if (countoff.RawSamples.Length > click.RawSamples.Length)
							{
								rawSamples = (countoff.RawSamples.Clone() as short[]);
								for (int i = 0; i < click.RawSamples.Length; i++)
								{
									rawSamples[i] = (short)((click.RawSamples[i] + rawSamples[i]) - ((click.RawSamples[i] + rawSamples[i]) / short.MaxValue));
								}
							}
							else
							{
								for (int i = 0; i < rawSamples.Length; i++)
								{
									rawSamples[i] = (short)((countoff.RawSamples[i] + rawSamples[i]) - ((countoff.RawSamples[i] + rawSamples[i]) / short.MaxValue));
								}
							}
							*/
						}
					}

					stream.Write(rawSamples);
					OnTick(EventArgs.Empty);

					System.Threading.Thread.Sleep(ms - 30);

					if (icountoff <= countoffs.Length) icountoff++;
				}
            }
        }

        public event EventHandler Tick;
        protected virtual void OnTick(EventArgs e)
        {
            if (Tick != null) Tick(this, e);
        }

        public Metronome(string path, double tempo = 120.0)
        {
            string[] FileNames = new string[]
            {
                path + System.IO.Path.DirectorySeparatorChar.ToString() + "Click.wav",
                path + System.IO.Path.DirectorySeparatorChar.ToString() + "One.wav",
                path + System.IO.Path.DirectorySeparatorChar.ToString() + "Two.wav",
                path + System.IO.Path.DirectorySeparatorChar.ToString() + "Three.wav",
                path + System.IO.Path.DirectorySeparatorChar.ToString() + "Four.wav"
            };

            foreach (string filename in FileNames)
            {
                string filetitle = System.IO.Path.GetFileNameWithoutExtension(filename);
                WaveformAudioObjectModel wave = new WaveformAudioObjectModel();
                MicrosoftWaveDataFormat wav = new MicrosoftWaveDataFormat();
                Document.Load(wave, wav, new FileAccessor(filename, false, false), true);

                waves.Add(filetitle, wave);
            }

            WaveformAudioObjectModel one = waves["One"];
            WaveformAudioObjectModel two = waves["Two"];
            WaveformAudioObjectModel three = waves["Three"];
            WaveformAudioObjectModel four = waves["Four"];
            WaveformAudioObjectModel click = waves["Click"];
            WaveformAudioObjectModel[] countoffs = new WaveformAudioObjectModel[] { one, two, three, four };

            foreach (WaveformAudioObjectModel countoff in countoffs)
            {
                short[] rawSamples = null;
                if (countoff.RawSamples.Length > click.RawSamples.Length)
                {
                    rawSamples = countoff.RawSamples.RawData;
                    for (int i = 0; i < click.RawSamples.Length; i++)
                    {
                        rawSamples[i] = (short)((click.RawSamples[i] + rawSamples[i]) - ((click.RawSamples[i] + rawSamples[i]) / (short.MaxValue + 1)));
                    }
                }
                else
                {
                    rawSamples = (click.RawSamples.Clone() as short[]);
                    for (int i = 0; i < rawSamples.Length; i++)
                    {
                        rawSamples[i] = (short)((countoff.RawSamples[i] + rawSamples[i]) - ((countoff.RawSamples[i] + rawSamples[i]) / (short.MaxValue + 1)));
                    }
                    countoff.RawSamples = new WaveformAudioSamples(rawSamples);
                }
            }

            mvarAudioSamplePath = path;
            mvarTempo = tempo;
        }
    }
}
