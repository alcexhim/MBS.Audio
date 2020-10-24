using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Surodoine
{
    public class AudioStream : System.IO.Stream
    {
        private IntPtr mvarHandle = IntPtr.Zero;
        public IntPtr Handle { get { return mvarHandle; } }

        private int mvarOutputChannelCount = 0;

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
        public AudioStream(AudioDevice inputDevice, int inputChannelCount, AudioSampleFormat inputSampleFormat, AudioDevice outputDevice, int outputChannelCount, AudioSampleFormat outputSampleFormat, double sampleRate, uint framesPerBuffer)
            : this(inputDevice, inputChannelCount, inputSampleFormat, (inputDevice == null) ? 0 : inputDevice.DefaultLowInputLatency, outputDevice, outputChannelCount, outputSampleFormat, (outputDevice == null) ? 0 : outputDevice.DefaultLowOutputLatency, sampleRate, framesPerBuffer, AudioStreamFlags.None)
        {
        }
        public AudioStream(AudioDevice inputDevice, int inputChannelCount, AudioSampleFormat inputSampleFormat, AudioDevice outputDevice, int outputChannelCount, AudioSampleFormat outputSampleFormat, double sampleRate, uint framesPerBuffer, AudioStreamFlags flags)
            : this(inputDevice, inputChannelCount, inputSampleFormat, (inputDevice == null) ? 0 : inputDevice.DefaultLowInputLatency, outputDevice, outputChannelCount, outputSampleFormat, (outputDevice == null) ? 0 : outputDevice.DefaultLowOutputLatency, sampleRate, framesPerBuffer, flags)
        {
        }
        public AudioStream(AudioDevice inputDevice, int inputChannelCount, AudioSampleFormat inputSampleFormat, double inputSuggestedLatency, AudioDevice outputDevice, int outputChannelCount, AudioSampleFormat outputSampleFormat, double outputSuggestedLatency, double sampleRate, uint framesPerBuffer)
            : this(inputDevice, inputChannelCount, inputSampleFormat, inputSuggestedLatency, outputDevice, outputChannelCount, outputSampleFormat, outputSuggestedLatency, sampleRate, framesPerBuffer, AudioStreamFlags.None)
        {
        }
        public AudioStream(AudioDevice inputDevice, int inputChannelCount, AudioSampleFormat inputSampleFormat, double inputSuggestedLatency, AudioDevice outputDevice, int outputChannelCount, AudioSampleFormat outputSampleFormat, double outputSuggestedLatency, double sampleRate, uint framesPerBuffer, AudioStreamFlags flags)
        {
            Internal.PortAudio.Structures.PaStreamParameters inputParameters = new Internal.PortAudio.Structures.PaStreamParameters();
            Internal.PortAudio.Structures.PaStreamParameters outputParameters = new Internal.PortAudio.Structures.PaStreamParameters();

            if (inputDevice != null)
            {
                inputParameters.channelCount = inputChannelCount;
                inputParameters.device = inputDevice.Handle;
                inputParameters.sampleFormat = inputSampleFormat;
                inputParameters.suggestedLatency = inputSuggestedLatency;
            }
            else if (outputDevice != null)
            {
                inputParameters.channelCount = outputChannelCount;
                inputParameters.device = outputDevice.Handle;
                inputParameters.sampleFormat = outputSampleFormat;
                inputParameters.suggestedLatency = outputSuggestedLatency;
            }

            if (outputDevice != null)
            {
                outputParameters.channelCount = outputChannelCount;
                outputParameters.device = outputDevice.Handle;
                outputParameters.sampleFormat = outputSampleFormat;
                outputParameters.suggestedLatency = outputSuggestedLatency;
            }
            else
            {
                outputParameters.channelCount = inputChannelCount;
                outputParameters.device = inputDevice.Handle;
                outputParameters.sampleFormat = inputSampleFormat;
                outputParameters.suggestedLatency = inputSuggestedLatency;
            }
            mvarOutputChannelCount = outputParameters.channelCount;

            Internal.PortAudio.Delegates.PaStreamCallbackDelegate streamCallback = null; // new Internal.PortAudio.Delegates.PaStreamCallbackDelegate(_streamCallback);
            IntPtr userData = IntPtr.Zero;

            Internal.PortAudio.Constants.PaError result1 = Internal.PortAudio.Methods.Pa_OpenStream(out mvarHandle, ref inputParameters, ref outputParameters, sampleRate, framesPerBuffer, flags, streamCallback, userData);
            if (result1 == Internal.PortAudio.Constants.PaError.paNoError)
            {
                Internal.PortAudio.Constants.PaError result2 = Internal.PortAudio.Methods.Pa_StartStream(mvarHandle);
                Internal.PortAudio.Methods.Pa_ResultToException(result2);
            }
            else
            {
                // result1 = Internal.PortAudio.Methods.Pa_OpenDefaultStream(out mvarHandle, inputChannelCount, outputChannelCount, (uint)outputSampleFormat, sampleRate, framesPerBuffer, streamCallback, userData);
                Internal.PortAudio.Methods.Pa_ResultToException(result1);
            }
        }

        private Internal.PortAudio.Constants.PaStreamCallbackResult _streamCallback(IntPtr input, IntPtr output, uint frameCount, ref Internal.PortAudio.Structures.PaStreamCallbackTimeInfo timeInfo, Internal.PortAudio.Constants.PaStreamCallbackFlags statusFlags, IntPtr userData)
        {
            return Internal.PortAudio.Constants.PaStreamCallbackResult.paComplete;
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

        public override void Flush()
        {
            Internal.PortAudio.Methods.Pa_StopStream(mvarHandle);
            Internal.PortAudio.Methods.Pa_StartStream(mvarHandle);
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

        public void Read(short[] buffer)
        {
            Internal.PortAudio.Constants.PaError result = Internal.PortAudio.Methods.Pa_ReadStream(mvarHandle, buffer, (uint)buffer.Length);
            Internal.PortAudio.Methods.Pa_ResultToException(result);
        }

        public override long Seek(long offset, System.IO.SeekOrigin origin)
        {
            throw new NotImplementedException();
        }

        public override void SetLength(long value)
        {
            throw new NotImplementedException();
        }

        public void Write(short[] buffer)
        {
            Internal.PortAudio.Constants.PaError result = Internal.PortAudio.Methods.Pa_WriteStream(mvarHandle, buffer, (uint)(buffer.Length));
            Internal.PortAudio.Methods.Pa_ResultToException(result);
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            Internal.PortAudio.Constants.PaError result = Internal.PortAudio.Methods.Pa_WriteStream(mvarHandle, buffer, (uint)count);
            Internal.PortAudio.Methods.Pa_ResultToException(result);
        }
    }
}
