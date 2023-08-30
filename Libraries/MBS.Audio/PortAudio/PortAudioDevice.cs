using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MBS.Audio.PortAudio
{
    public class PortAudioDevice : AudioDevice
    {
        internal PortAudioDevice(int handle)
		{
			Handle = handle;

			Internal.PortAudio.Structures.PaDeviceInfo devinfo = Internal.PortAudio.Methods.Pa_GetDeviceInfo(handle);
			_maximumInputChannels = devinfo.maxInputChannels;
			_maximumOutputChannels = devinfo.maxOutputChannels;
			_defaultSampleRate = devinfo.defaultSampleRate;
			_defaultLowInputLatency = devinfo.defaultLowInputLatency;
			_defaultHighInputLatency = devinfo.defaultHighInputLatency;
			_defaultLowOutputLatency = devinfo.defaultLowOutputLatency;
			_defaultHighOutputLatency = devinfo.defaultHighOutputLatency;
			HostAPI = devinfo.hostApi;
			Name = devinfo.name;
		}

		public string Name { get; }
		public int HostAPI { get; } = 0;

		private int _maximumInputChannels;
		public override int MaximumInputChannels => _maximumInputChannels;
		private int _maximumOutputChannels;
		public override int MaximumOutputChannels => _maximumOutputChannels;

		private double _defaultHighInputLatency;
		public override double DefaultHighInputLatency => _defaultHighInputLatency;
		private double _defaultHighOutputLatency;
		public override double DefaultHighOutputLatency => _defaultHighOutputLatency;
		private double _defaultLowInputLatency;
		public override double DefaultLowInputLatency => _defaultLowInputLatency;
		private double _defaultLowOutputLatency;
		public override double DefaultLowOutputLatency => _defaultLowOutputLatency;

		private double _defaultSampleRate;
		public override double DefaultSampleRate => _defaultSampleRate;
		public int Handle { get; } = 0;

		public override string ToString()
        {
            return Name;
        }
    }
}
