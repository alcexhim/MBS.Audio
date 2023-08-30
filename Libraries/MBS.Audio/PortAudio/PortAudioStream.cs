using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MBS.Audio.PortAudio
{
    public class PortAudioStream : AudioStream
	{
		public PortAudioStream(AudioDevice inputDevice, AudioDevice outputDevice, AudioSampleFormat sampleFormat)
				  : this(inputDevice, outputDevice, sampleFormat, sampleFormat)
		{
		}
		public PortAudioStream(AudioDevice inputDevice, AudioDevice outputDevice, AudioSampleFormat sampleFormat, double sampleRate)
			: this(inputDevice, outputDevice, sampleFormat, sampleFormat, sampleRate)
		{
		}
		public PortAudioStream(AudioDevice inputDevice, AudioDevice outputDevice, AudioSampleFormat inputSampleFormat, AudioSampleFormat outputSampleFormat)
			: this(inputDevice, inputDevice.MaximumInputChannels, inputSampleFormat, outputDevice, outputDevice.MaximumOutputChannels, outputSampleFormat, outputDevice.DefaultSampleRate, 0)
		{
		}
		public PortAudioStream(AudioDevice inputDevice, AudioDevice outputDevice, AudioSampleFormat inputSampleFormat, AudioSampleFormat outputSampleFormat, double sampleRate)
			: this(inputDevice, inputDevice.MaximumInputChannels, inputSampleFormat, outputDevice, outputDevice.MaximumOutputChannels, outputSampleFormat, sampleRate, 0)
		{
		}
		public PortAudioStream(AudioDevice inputDevice, int inputChannelCount, AudioSampleFormat inputSampleFormat, AudioDevice outputDevice, int outputChannelCount, AudioSampleFormat outputSampleFormat, double sampleRate)
			: this(inputDevice, inputChannelCount, inputSampleFormat, (inputDevice == null) ? 0 : inputDevice.DefaultLowInputLatency, outputDevice, outputChannelCount, outputSampleFormat, (outputDevice == null) ? 0 : outputDevice.DefaultLowOutputLatency, sampleRate, 0, AudioStreamFlags.None)
		{
		}
		public PortAudioStream(AudioDevice inputDevice, int inputChannelCount, AudioSampleFormat inputSampleFormat, AudioDevice outputDevice, int outputChannelCount, AudioSampleFormat outputSampleFormat, double sampleRate, int framesPerBuffer)
			: this(inputDevice, inputChannelCount, inputSampleFormat, (inputDevice == null) ? 0 : inputDevice.DefaultLowInputLatency, outputDevice, outputChannelCount, outputSampleFormat, (outputDevice == null) ? 0 : outputDevice.DefaultLowOutputLatency, sampleRate, framesPerBuffer, AudioStreamFlags.None)
		{
		}
		public PortAudioStream(AudioDevice inputDevice, int inputChannelCount, AudioSampleFormat inputSampleFormat, AudioDevice outputDevice, int outputChannelCount, AudioSampleFormat outputSampleFormat, double sampleRate, int framesPerBuffer, AudioStreamFlags flags)
			: this(inputDevice, inputChannelCount, inputSampleFormat, (inputDevice == null) ? 0 : inputDevice.DefaultLowInputLatency, outputDevice, outputChannelCount, outputSampleFormat, (outputDevice == null) ? 0 : outputDevice.DefaultLowOutputLatency, sampleRate, framesPerBuffer, flags)
		{
		}
		public PortAudioStream(AudioDevice inputDevice, int inputChannelCount, AudioSampleFormat inputSampleFormat, double inputSuggestedLatency, AudioDevice outputDevice, int outputChannelCount, AudioSampleFormat outputSampleFormat, double outputSuggestedLatency, double sampleRate, int framesPerBuffer)
			: this(inputDevice, inputChannelCount, inputSampleFormat, inputSuggestedLatency, outputDevice, outputChannelCount, outputSampleFormat, outputSuggestedLatency, sampleRate, framesPerBuffer, AudioStreamFlags.None)
		{
		}
		public PortAudioStream(AudioDevice inputDevice, int inputChannelCount, AudioSampleFormat inputSampleFormat, double inputSuggestedLatency, AudioDevice outputDevice, int outputChannelCount, AudioSampleFormat outputSampleFormat, double outputSuggestedLatency, double sampleRate, int framesPerBuffer, AudioStreamFlags flags) :
			base(inputDevice, inputChannelCount, inputSampleFormat, inputSuggestedLatency, outputDevice, outputChannelCount, outputSampleFormat, outputSuggestedLatency, sampleRate, framesPerBuffer, flags)
		{
		}

		private IntPtr _handle = IntPtr.Zero;

		protected override void InitializeInternal()
		{
			base.InitializeInternal();

			Internal.PortAudio.Structures.PaStreamParameters inputParameters = new Internal.PortAudio.Structures.PaStreamParameters();
			Internal.PortAudio.Structures.PaStreamParameters outputParameters = new Internal.PortAudio.Structures.PaStreamParameters();

			if (InputDevice != null)
			{
				inputParameters.channelCount = InputChannelCount;
				inputParameters.device = (InputDevice as PortAudioDevice).Handle;
				inputParameters.sampleFormat = InputSampleFormat;
				inputParameters.suggestedLatency = InputSuggestedLatency;
			}
			else if (OutputDevice != null)
			{
				inputParameters.channelCount = OutputChannelCount;
				inputParameters.device = (OutputDevice as PortAudioDevice).Handle;
				inputParameters.sampleFormat = OutputSampleFormat;
				inputParameters.suggestedLatency = OutputSuggestedLatency;
			}

			if (OutputDevice != null)
			{
				outputParameters.channelCount = OutputChannelCount;
				outputParameters.device = (OutputDevice as PortAudioDevice).Handle;
				outputParameters.sampleFormat = OutputSampleFormat;
				outputParameters.suggestedLatency = OutputSuggestedLatency;
			}
			else
			{
				outputParameters.channelCount = InputChannelCount;
				outputParameters.device = (InputDevice as PortAudioDevice).Handle;
				outputParameters.sampleFormat = InputSampleFormat;
				outputParameters.suggestedLatency = InputSuggestedLatency;
			}
			mvarOutputChannelCount = outputParameters.channelCount;

			Internal.PortAudio.Delegates.PaStreamCallbackDelegate streamCallback = null; // new Internal.PortAudio.Delegates.PaStreamCallbackDelegate(_streamCallback);
			IntPtr userData = IntPtr.Zero;

			Internal.PortAudio.Constants.PaError result1 = Internal.PortAudio.Methods.Pa_OpenStream(out _handle, ref inputParameters, ref outputParameters, SampleRate, (uint)FramesPerBuffer, AudioStreamFlagsToPortAudioStreamFlags(Flags), streamCallback, userData);
			if (result1 == Internal.PortAudio.Constants.PaError.paNoError)
			{
				Internal.PortAudio.Constants.PaError result2 = Internal.PortAudio.Methods.Pa_StartStream(_handle);
				Internal.PortAudio.Methods.Pa_ResultToException(result2);
			}
			else
			{
				// result1 = Internal.PortAudio.Methods.Pa_OpenDefaultStream(out mvarHandle, inputChannelCount, outputChannelCount, (uint)outputSampleFormat, sampleRate, framesPerBuffer, streamCallback, userData);
				Internal.PortAudio.Methods.Pa_ResultToException(result1);
			}
		}

		private static PortAudioStreamFlags AudioStreamFlagsToPortAudioStreamFlags(AudioStreamFlags flags)
		{
			PortAudioStreamFlags flags2 = PortAudioStreamFlags.None;
			if ((flags & AudioStreamFlags.ClipOff) == AudioStreamFlags.ClipOff) flags2 |= PortAudioStreamFlags.ClipOff;
			if ((flags & AudioStreamFlags.DitherOff) == AudioStreamFlags.DitherOff) flags2 |= PortAudioStreamFlags.DitherOff;
			if ((flags & AudioStreamFlags.NeverDropInput) == AudioStreamFlags.NeverDropInput) flags2 |= PortAudioStreamFlags.NeverDropInput;
			// if ((flags & AudioStreamFlags.PlatformSpecificFlags) == AudioStreamFlags.PlatformSpecificFlags) flags2 |= PortAudioStreamFlags.PlatformSpecificFlags;
			if ((flags & AudioStreamFlags.PrimeOutputBuffersUsingStreamCallback) == AudioStreamFlags.PrimeOutputBuffersUsingStreamCallback) flags2 |= PortAudioStreamFlags.PrimeOutputBuffersUsingStreamCallback;
			return flags2;
		}

		private int mvarOutputChannelCount = 0;

        private Internal.PortAudio.Constants.PaStreamCallbackResult _streamCallback(IntPtr input, IntPtr output, uint frameCount, ref Internal.PortAudio.Structures.PaStreamCallbackTimeInfo timeInfo, Internal.PortAudio.Constants.PaStreamCallbackFlags statusFlags, IntPtr userData)
        {
            return Internal.PortAudio.Constants.PaStreamCallbackResult.paComplete;
        }

        public override void Flush()
        {
            Internal.PortAudio.Methods.Pa_StopStream(_handle);
            Internal.PortAudio.Methods.Pa_StartStream(_handle);
        }

		public override void Read(short[] buffer)
        {
            Internal.PortAudio.Constants.PaError result = Internal.PortAudio.Methods.Pa_ReadStream(_handle, buffer, (uint)buffer.Length);
            Internal.PortAudio.Methods.Pa_ResultToException(result);
        }

        public override void Write(short[] buffer)
        {
            Internal.PortAudio.Constants.PaError result = Internal.PortAudio.Methods.Pa_WriteStream(_handle, buffer, (uint)(buffer.Length));
            Internal.PortAudio.Methods.Pa_ResultToException(result);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            Internal.PortAudio.Constants.PaError result = Internal.PortAudio.Methods.Pa_WriteStream(_handle, buffer, (uint)count);
            Internal.PortAudio.Methods.Pa_ResultToException(result);
        }
    }
}
