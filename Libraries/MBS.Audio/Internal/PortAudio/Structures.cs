using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace MBS.Audio.Internal.PortAudio
{
    public static class Structures
    {
        public struct PaDeviceInfo
        {
            public int structVersion;
            [MarshalAs(UnmanagedType.LPStr)]
            public string name;
            public int hostApi;
            public int maxInputChannels;
            public int maxOutputChannels;
            public double defaultLowInputLatency;
            public double defaultLowOutputLatency;
            public double defaultHighInputLatency;
            public double defaultHighOutputLatency;
            public double defaultSampleRate;
            public override string ToString()
            {
                return string.Concat(new object[]
		        {
			        "[", 
			        base.GetType().Name, 
			        "]\nname: ", 
			        this.name, 
			        "\nhostApi: ", 
			        this.hostApi, 
			        "\nmaxInputChannels: ", 
			        this.maxInputChannels, 
			        "\nmaxOutputChannels: ", 
			        this.maxOutputChannels, 
			        "\ndefaultLowInputLatency: ", 
			        this.defaultLowInputLatency, 
			        "\ndefaultLowOutputLatency: ", 
			        this.defaultLowOutputLatency, 
			        "\ndefaultHighInputLatency: ", 
			        this.defaultHighInputLatency, 
			        "\ndefaultHighOutputLatency: ", 
			        this.defaultHighOutputLatency, 
			        "\ndefaultSampleRate: ", 
			        this.defaultSampleRate
		        });
                    }
        }
        public struct PaStreamCallbackTimeInfo
        {
            public double inputBufferAdcTime;
            public double currentTime;
            public double outputBufferDacTime;
            public override string ToString()
            {
                return string.Concat(new object[]
		        {
			        "[", 
			        base.GetType().Name, 
			        "]\ncurrentTime: ", 
			        this.currentTime, 
			        "\ninputBufferAdcTime: ", 
			        this.inputBufferAdcTime, 
			        "\noutputBufferDacTime: ", 
			        this.outputBufferDacTime
		        });
            }
        }
        public struct PaStreamParameters
        {
            public int device;
            public int channelCount;
            public AudioSampleFormat sampleFormat;
            public double suggestedLatency;
            public IntPtr hostApiSpecificStreamInfo;
            public override string ToString()
            {
                return string.Concat(new object[]
                {
                    "[", 
                    base.GetType().Name, 
                    "]\ndevice: ", 
			        this.device, 
			        "\nchannelCount: ", 
			        this.channelCount, 
			        "\nsampleFormat: ", 
			        this.sampleFormat, 
			        "\nsuggestedLatency: ", 
			        this.suggestedLatency
                });
            }
        }
    }
}
