using System;
using System.Collections.Generic;
using System.Text;

using UniversalEditor.ObjectModels.Multimedia.Audio.Waveform;

namespace MBS.Audio
{
	public class AudioPlayer : ITransport
	{
		public bool IsPlaying { get { return mvarState != AudioPlayerState.Stopped; } }

		private System.Threading.Thread PlayThread = null;

		public AudioDevice InputDevice { get; set; } = AudioDevice.None;
		public AudioDevice OutputDevice { get; set; } = AudioDevice.None;

		public event EventHandler<AudioPlayerStateChangedEventArgs> StateChanged;

		protected virtual void OnStateChanged(AudioPlayerStateChangedEventArgs e)
		{
			StateChanged?.Invoke(this, e);
		}

		private AudioPlayerState mvarState = AudioPlayerState.Stopped;
		public AudioPlayerState State { get { return mvarState; } private set { if (mvarState != value) { mvarState = value; OnStateChanged(new AudioPlayerStateChangedEventArgs(value, AudioPlayerStateChangedReason.UserAction)); } } }

		private AudioTimestamp mvarTimestamp = AudioTimestamp.Empty;
		public AudioTimestamp Timestamp { get { return mvarTimestamp; } set { mvarTimestamp = value; i = mvarTimestamp.TotalSamples; } }

		public void Play()
		{
			Play(false);
		}
		public void Play(bool async)
		{
			if (Document == null) throw new NullReferenceException();

			if (PlayThread != null)
			{
				PlayThread.Abort();
				PlayThread = null;
			}

			PlayThread = new System.Threading.Thread(PlayThread_ThreadStart);
			PlayThread.Start();

			if (!async)
			{
				while (IsPlaying)
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
			Document = wave;
			Play(async);
		}

		public double Volume { get; set; } = 0.5;

		public AudioEngine AudioEngine { get; }

		public AudioPlayer(AudioEngine ae)
		{
			AudioEngine = ae;
		}

		private long i = 0;
		private void PlayThread_ThreadStart()
		{
			int bufferSize = Document.Header.ChannelCount;

			AudioSampleFormat asf = AudioSampleFormat.Int16;
			switch (Document.Header.BitsPerSample)
			{
				case 8: asf = AudioSampleFormat.Int8; break;
				case 16: asf = AudioSampleFormat.Int16; break;
				case 24: asf = AudioSampleFormat.Int24; break;
				case 32: asf = AudioSampleFormat.Int32; break;
			}

			AudioStream audio = AudioEngine.CreateAudioStream(AudioEngine.DefaultInputDevice, Document.Header.ChannelCount, asf, 0, AudioEngine.DefaultOutputDevice, Document.Header.ChannelCount, asf, 0, Document.Header.SampleRate * Document.Header.ChannelCount, 0, AudioStreamFlags.None);

			mvarState = AudioPlayerState.Playing;
			OnStateChanged(new AudioPlayerStateChangedEventArgs(AudioPlayerState.Playing, AudioPlayerStateChangedReason.UserAction));

			long start = 0;

			mvarTimestamp = AudioTimestamp.FromSamples((int)0, Document.Header.SampleRate * Document.Header.ChannelCount);

			while (State != AudioPlayerState.Stopped)
			{
				if (State != AudioPlayerState.Paused)
				{
					for (i = mvarTimestamp.TotalSamples; i < Document.RawSamples.Length;)
					{
						short[] buffer = Document.RawSamples[(int)(i * bufferSize), bufferSize];

						/*
						short sampleL = mvarDocument.RawSamples[mvarTimestamp.TotalSamples];
						short sampleR = sampleL;
						if (i + 1 < mvarDocument.RawSamples.Length)
						{
							sampleR = mvarDocument.RawSamples[i + 1];
						}

						audio.Write(new short[] { sampleL, sampleR });

						mvarTimestamp.TotalSamples += 2;
						*/

						for (int j = 0; j < buffer.Length; j++)
						{
							buffer[j] = (byte)(Volume * buffer[j]);
						}

						audio.Write(buffer);
						mvarTimestamp.TotalSamples += buffer.Length;
												
						i = mvarTimestamp.TotalSamples;
						// start = i;
						if (State != AudioPlayerState.Playing) break;
					}
				}
				System.Threading.Thread.Sleep(500);
				if (mvarTimestamp.TotalSamples >= Document.RawSamples.Length)
				{
					mvarState = AudioPlayerState.Stopped;
					OnStateChanged(new AudioPlayerStateChangedEventArgs(mvarState, AudioPlayerStateChangedReason.SongEnded));
				}
			}
			audio.Flush();

		}
		public void Stop()
		{
			if (!IsPlaying) return;

			mvarState = AudioPlayerState.Stopped;
			OnStateChanged(new AudioPlayerStateChangedEventArgs(AudioPlayerState.Stopped, AudioPlayerStateChangedReason.UserAction));

			mvarTimestamp = AudioTimestamp.FromSamples((int)0, Document.Header.SampleRate * 2);
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

		public WaveformAudioObjectModel Document { get; set; } = null;
		public int ChannelCount { get; set; } = 2;
	}
}
