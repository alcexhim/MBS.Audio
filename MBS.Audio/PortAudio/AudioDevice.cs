using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MBS.Audio.PortAudio
{
    public class AudioDevice
    {
        internal AudioDevice(int handle)
        {
            mvarHandle = handle;

            Internal.PortAudio.Structures.PaDeviceInfo devinfo = Internal.PortAudio.Methods.Pa_GetDeviceInfo(handle);
            mvarDefaultHighInputLatency = devinfo.defaultHighInputLatency;
            mvarDefaultHighOutputLatency = devinfo.defaultHighOutputLatency;
            mvarDefaultLowInputLatency = devinfo.defaultLowInputLatency;
            mvarDefaultLowOutputLatency = devinfo.defaultLowOutputLatency;
            mvarDefaultSampleRate = devinfo.defaultSampleRate;
            mvarHostAPI = devinfo.hostApi;
            mvarMaximumInputChannels = devinfo.maxInputChannels;
            mvarMaximumOutputChannels = devinfo.maxOutputChannels;
            mvarName = devinfo.name;
        }

        private static AudioDevice[] mvarDevices = null;
        public static AudioDevice[] Devices
        {
            get
            {
                if (mvarDevices == null || mvarDevices.Length == 0)
                {
                    List<AudioDevice> devices = new List<AudioDevice>();
                    int count = Internal.PortAudio.Methods.Pa_GetDeviceCount();
                    for (int i = 0; i < count; i++)
                    {
                        AudioDevice device = new AudioDevice(i);
                        devices.Add(device);
                    }
                    mvarDevices = devices.ToArray();
                }
                return mvarDevices;
            }
        }

        private string mvarName = String.Empty;
        public string Name { get { return mvarName; } }

        private int mvarHostAPI = 0;
        public int HostAPI { get { return mvarHostAPI; } }

        private int mvarMaximumInputChannels = 0;
        public int MaximumInputChannels { get { return mvarMaximumInputChannels; } }
        private int mvarMaximumOutputChannels = 0;
        public int MaximumOutputChannels { get { return mvarMaximumOutputChannels; } }

        private double mvarDefaultHighInputLatency = 0;
        public double DefaultHighInputLatency { get { return mvarDefaultHighInputLatency; } }
        private double mvarDefaultHighOutputLatency = 0;
        public double DefaultHighOutputLatency { get { return mvarDefaultHighOutputLatency; } }
        private double mvarDefaultLowInputLatency = 0;
        public double DefaultLowInputLatency { get { return mvarDefaultLowInputLatency; } }
        private double mvarDefaultLowOutputLatency = 0;
        public double DefaultLowOutputLatency { get { return mvarDefaultLowOutputLatency; } }

        private double mvarDefaultSampleRate = 0;
        public double DefaultSampleRate { get { return mvarDefaultSampleRate; } }

        private static AudioDevice mvarNone = null;
        public static AudioDevice None { get { return mvarNone; } }

        private int mvarHandle = 0;
        public int Handle { get { return mvarHandle; } }

        public override string ToString()
        {
            return mvarName;
        }
    }
}
