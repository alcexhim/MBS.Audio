using System;
using System.Collections.Generic;
using System.Text;

using UniversalEditor.ObjectModels.Multimedia.Audio.Waveform;

namespace Surodoine
{
    public class AudioPlayer
    {
        private bool mvarIsPlaying = false;
        public bool IsPlaying { get { return mvarIsPlaying; } }

        private System.Threading.Thread PlayThread = null;

        private AudioDevice mvarInputDevice = AudioDevice.DefaultInput;
        public AudioDevice InputDevice { get { return mvarInputDevice; } set { mvarInputDevice = value; } }

        private AudioDevice mvarOutputDevice = AudioDevice.DefaultOutput;
		public AudioDevice OutputDevice { get { return mvarOutputDevice; } set { mvarOutputDevice = value; } }

		public event AudioPlayerStateChangedEventHandler StateChanged;
		protected virtual void OnStateChanged(AudioPlayerStateChangedEventArgs e)
		{
			if (StateChanged != null) StateChanged(this, e);
		}

		private AudioPlayerState mvarState = AudioPlayerState.Stopped;
		public AudioPlayerState State { get { return mvarState; } private set { if (mvarState != value) { mvarState = value; OnStateChanged(new AudioPlayerStateChangedEventArgs(value)); } } }

        private AudioTimestamp mvarTimestamp = AudioTimestamp.Empty;
        public AudioTimestamp Timestamp { get { return mvarTimestamp; } set { mvarTimestamp = value; i = mvarTimestamp.TotalSamples; } }

		public void Play()
		{
			Play(false);
		}
		public void Play(bool async)
		{
			if (mvarDocument == null) throw new NullReferenceException();

			Internal.PortAudio.Constants.PaError result = Internal.PortAudio.Methods.Pa_Initialize();

			mvarIsPlaying = true;

			PlayThread = new System.Threading.Thread(PlayThread_ThreadStart);
			PlayThread.Start();

			if (!async)
			{
				while (mvarIsPlaying)
				{
					System.Threading.Thread.Sleep(500);
				}
			}
		}
        public void Play(WaveformAudioObjectModel wave)
        {
            Play(wave, false);
        }
        public void Play(WaveformAudioObjectModel wave, bool async)
        {
			mvarDocument = wave;
			Play(async);
        }

        private long i = 0;
        private void PlayThread_ThreadStart()
        {
			AudioStream audio = new AudioStream(mvarInputDevice, 2, AudioSampleFormat.Int16, mvarOutputDevice, mvarDocument.Header.ChannelCount, AudioSampleFormat.Int16, mvarDocument.Header.SampleRate * mvarDocument.Header.ChannelCount, 0, Surodoine.AudioStreamFlags.ClipOff);

			State = AudioPlayerState.Playing;
            long start = 0;
            
            mvarTimestamp = new AudioTimestamp((int)0, mvarDocument.Header.SampleRate * 2);

            while (State != AudioPlayerState.Stopped)
            {
                if (State != AudioPlayerState.Paused)
                {
                    for (i = mvarTimestamp.TotalSamples; i < mvarDocument.RawSamples.Length; )
                    {
                        short sampleL = mvarDocument.RawSamples[mvarTimestamp.TotalSamples];
                        short sampleR = sampleL;
                        if (i + 1 < mvarDocument.RawSamples.Length)
                        {
                            sampleR = mvarDocument.RawSamples[i + 1];
                        }

                        audio.Write(new short[] { sampleL, sampleR });

                        mvarTimestamp.TotalSamples += 2;
                        i = mvarTimestamp.TotalSamples;
                        // start = i;
                        if (State != AudioPlayerState.Playing) break;
                    }
                }
                System.Threading.Thread.Sleep(500);
                if (mvarTimestamp.TotalSamples >= mvarDocument.RawSamples.Length)
                {
                    State = AudioPlayerState.Stopped;
                }
            }
			State = AudioPlayerState.Stopped;

            mvarIsPlaying = false;
        }
        public void Stop()
        {
            if (!mvarIsPlaying) return;

            State = AudioPlayerState.Stopped;
        }

		public void Pause()
		{
			if (State == AudioPlayerState.Playing)
			{
				State = AudioPlayerState.Paused;
			}
			else if (State == AudioPlayerState.Paused)
			{
				State = AudioPlayerState.Playing;
			}
		}

		private WaveformAudioObjectModel mvarDocument = null;
		public WaveformAudioObjectModel Document { get { return mvarDocument; } set { mvarDocument = value; } }

		private int mvarChannelCount = 2;
		public int ChannelCount { get { return mvarChannelCount; } set { mvarChannelCount = value; } }
	}
}
