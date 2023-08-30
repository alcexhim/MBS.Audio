using System;
namespace MBS.Audio
{
	public abstract class AudioStream : System.IO.Stream
	{
		public AudioDevice InputDevice { get; }
		public int InputChannelCount { get; }
		public AudioSampleFormat InputSampleFormat { get; }
		public double InputSuggestedLatency { get; }

		public AudioDevice OutputDevice { get; }
		public int OutputChannelCount { get; }
		public AudioSampleFormat OutputSampleFormat { get; }
		public double OutputSuggestedLatency { get; }

		public double SampleRate { get; }
		public int FramesPerBuffer { get; }
		public AudioStreamFlags Flags { get; }

		public AudioStream(AudioDevice inputDevice, AudioDevice outputDevice, AudioSampleFormat sampleFormat)
			   : this(inputDevice, outputDevice, sampleFormat, sampleFormat)
		{
		}
		public AudioStream(AudioDevice inputDevice, AudioDevice outputDevice, AudioSampleFormat sampleFormat, double sampleRate)
			: this(inputDevice, outputDevice, sampleFormat, sampleFormat, sampleRate)
		{
		}
		public AudioStream(AudioDevice inputDevice, AudioDevice outputDevice, AudioSampleFormat inputSampleFormat, AudioSampleFormat outputSampleFormat)
			: this(inputDevice, inputDevice.MaximumInputChannels, inputSampleFormat, outputDevice, outputDevice.MaximumOutputChannels, outputSampleFormat, outputDevice.DefaultSampleRate, 0)
		{
		}
		public AudioStream(AudioDevice inputDevice, AudioDevice outputDevice, AudioSampleFormat inputSampleFormat, AudioSampleFormat outputSampleFormat, double sampleRate)
			: this(inputDevice, inputDevice.MaximumInputChannels, inputSampleFormat, outputDevice, outputDevice.MaximumOutputChannels, outputSampleFormat, sampleRate, 0)
		{
		}
		public AudioStream(AudioDevice inputDevice, int inputChannelCount, AudioSampleFormat inputSampleFormat, AudioDevice outputDevice, int outputChannelCount, AudioSampleFormat outputSampleFormat, double sampleRate)
			: this(inputDevice, inputChannelCount, inputSampleFormat, (inputDevice == null) ? 0 : inputDevice.DefaultLowInputLatency, outputDevice, outputChannelCount, outputSampleFormat, (outputDevice == null) ? 0 : outputDevice.DefaultLowOutputLatency, sampleRate, 0, AudioStreamFlags.None)
		{
		}
		public AudioStream(AudioDevice inputDevice, int inputChannelCount, AudioSampleFormat inputSampleFormat, AudioDevice outputDevice, int outputChannelCount, AudioSampleFormat outputSampleFormat, double sampleRate, int framesPerBuffer)
			: this(inputDevice, inputChannelCount, inputSampleFormat, (inputDevice == null) ? 0 : inputDevice.DefaultLowInputLatency, outputDevice, outputChannelCount, outputSampleFormat, (outputDevice == null) ? 0 : outputDevice.DefaultLowOutputLatency, sampleRate, framesPerBuffer, AudioStreamFlags.None)
		{
		}
		public AudioStream(AudioDevice inputDevice, int inputChannelCount, AudioSampleFormat inputSampleFormat, AudioDevice outputDevice, int outputChannelCount, AudioSampleFormat outputSampleFormat, double sampleRate, int framesPerBuffer, AudioStreamFlags flags)
			: this(inputDevice, inputChannelCount, inputSampleFormat, (inputDevice == null) ? 0 : inputDevice.DefaultLowInputLatency, outputDevice, outputChannelCount, outputSampleFormat, (outputDevice == null) ? 0 : outputDevice.DefaultLowOutputLatency, sampleRate, framesPerBuffer, flags)
		{
		}
		public AudioStream(AudioDevice inputDevice, int inputChannelCount, AudioSampleFormat inputSampleFormat, double inputSuggestedLatency, AudioDevice outputDevice, int outputChannelCount, AudioSampleFormat outputSampleFormat, double outputSuggestedLatency, double sampleRate, int framesPerBuffer)
			: this(inputDevice, inputChannelCount, inputSampleFormat, inputSuggestedLatency, outputDevice, outputChannelCount, outputSampleFormat, outputSuggestedLatency, sampleRate, framesPerBuffer, AudioStreamFlags.None)
		{
		}
		public AudioStream(AudioDevice inputDevice, int inputChannelCount, AudioSampleFormat inputSampleFormat, double inputSuggestedLatency, AudioDevice outputDevice, int outputChannelCount, AudioSampleFormat outputSampleFormat, double outputSuggestedLatency, double sampleRate, int framesPerBuffer, AudioStreamFlags flags)
		{
			InputDevice = inputDevice;
			InputChannelCount = inputChannelCount;
			InputSampleFormat = inputSampleFormat;
			InputSuggestedLatency = inputSuggestedLatency;
			OutputDevice = outputDevice;
			OutputChannelCount = outputChannelCount;
			OutputSampleFormat = outputSampleFormat;
			OutputSuggestedLatency = outputSuggestedLatency;
			SampleRate = sampleRate;
			FramesPerBuffer = framesPerBuffer;
			Flags = flags;

			Initialize();
		}

		public override bool CanRead
		{
			get { throw new NotImplementedException(); }
		}

		public override bool CanSeek
		{
			get { throw new NotImplementedException(); }
		}

		public override bool CanWrite
		{
			get { return true; }
		}

		protected virtual void InitializeInternal()
		{
		}
		private void Initialize()
		{
			InitializeInternal();
		}


		public override long Length
		{
			get { throw new NotImplementedException(); }
		}

		public override long Position
		{
			get
			{
				throw new NotImplementedException();
			}
			set
			{
				throw new NotImplementedException();
			}
		}

		public override int Read(byte[] buffer, int offset, int count)
		{
			throw new NotImplementedException();
		}

		public abstract void Read(short[] buffer);
		public abstract void Write(short[] buffer);

		public override long Seek(long offset, System.IO.SeekOrigin origin)
		{
			throw new NotImplementedException();
		}

		public override void SetLength(long value)
		{
			throw new NotImplementedException();
		}

	}
}
