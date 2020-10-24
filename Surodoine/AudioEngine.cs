using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Surodoine
{
    public class AudioEngine : IDisposable
    {
        public void Initialize()
        {
            Internal.PortAudio.Constants.PaError result = Internal.PortAudio.Methods.Pa_Initialize();
            Internal.PortAudio.Methods.Pa_ResultToException(result);

			int defaultInputDeviceHandle = Internal.PortAudio.Methods.Pa_GetDefaultInputDevice();
			Internal.PortAudio.Methods.Pa_ResultToException(result);

			int defaultOutputDeviceHandle = Internal.PortAudio.Methods.Pa_GetDefaultOutputDevice();
			Internal.PortAudio.Methods.Pa_ResultToException(result);
			mvarDefaultInput = new AudioDevice(defaultInputDeviceHandle);
			mvarDefaultOutput = new AudioDevice(defaultOutputDeviceHandle);
		}
		public void Terminate()
		{
			Internal.PortAudio.Constants.PaError result = Internal.PortAudio.Methods.Pa_Terminate();
			Internal.PortAudio.Methods.Pa_ResultToException(result);
		}

		public void Dispose()
		{
			Dispose(true);
			GC.SuppressFinalize(this);
		}

		private bool _disposed = false;

		private AudioDevice mvarDefaultInput = null;
		public AudioDevice DefaultInput
		{
			get
			{
				int defaultOutputDeviceHandle = Internal.PortAudio.Methods.Pa_GetDefaultInputDevice();
				mvarDefaultOutput = new AudioDevice(defaultOutputDeviceHandle);
				return mvarDefaultOutput;
			}
		}
		private AudioDevice mvarDefaultOutput = null;
		public AudioDevice DefaultOutput
		{
			get
			{
				int defaultOutputDeviceHandle = Internal.PortAudio.Methods.Pa_GetDefaultOutputDevice();
				mvarDefaultOutput = new AudioDevice(defaultOutputDeviceHandle);
				return mvarDefaultOutput;
			}
		}

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
	}
}
