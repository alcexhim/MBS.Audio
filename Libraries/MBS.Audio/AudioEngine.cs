using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MBS.Audio
{
    public abstract class AudioEngine : IDisposable
    {
		public abstract AudioDevice DefaultInputDevice { get; }
		public abstract AudioDevice DefaultOutputDevice { get; }

		public void Initialize()
		{
			InitializeInternal();
		}
		protected abstract void InitializeInternal();

		public void Terminate()
		{
			TerminateInternal();
		}
		protected abstract void TerminateInternal();

		public abstract Guid ID { get; }
		public abstract string Title { get; }

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		private bool _disposed = false;

		protected virtual void Dispose(bool disposing)
		{
			if (_disposed)
			{
				return;
			}

			if (disposing)
			{
				// Dispose managed state (managed objects).
			}

			// free unmanaged resources (unmanaged objects) and override a finalizer below.
			// set large fields to null.
			Terminate();

			_disposed = true;
		}

		public AudioEngine()
		{
			Initialize();
		}

		~AudioEngine() => Dispose(false);

		protected abstract AudioStream CreateAudioStreamInternal(AudioDevice inputDevice, short inputChannelCount, AudioSampleFormat inputSampleFormat, double inputSuggestedLatency, AudioDevice outputDevice, short outputChannelCount, AudioSampleFormat outputSampleFormat, double outputSuggestedLatency, double sampleRate, int framesPerBuffer, AudioStreamFlags flags);
		public AudioStream CreateAudioStream(AudioDevice inputDevice, short inputChannelCount, AudioSampleFormat inputSampleFormat, double inputSuggestedLatency, AudioDevice outputDevice, short outputChannelCount, AudioSampleFormat outputSampleFormat, double outputSuggestedLatency, double sampleRate, int framesPerBuffer, AudioStreamFlags flags)
		{
			return CreateAudioStreamInternal(inputDevice, inputChannelCount, inputSampleFormat, inputSuggestedLatency, outputDevice, outputChannelCount, outputSampleFormat, outputSuggestedLatency, sampleRate, framesPerBuffer, flags);
		}
	}
}
