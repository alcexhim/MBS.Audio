using System;
using System.Runtime.InteropServices;

namespace MBS.Audio.Jack.Networking.Internal
{
	internal static class Structures
	{
		public struct jack_slave_t
		{
			public int audio_input;    // from master or to slave (-1 to take master audio physical inputs)
			public int audio_output;   // to master or from slave (-1 to take master audio physical outputs)
			public int midi_input;     // from master or to slave (-1 to take master MIDI physical inputs)
			public int midi_output;    // to master or from slave (-1 to take master MIDI physical outputs)
			public int mtu;            // network Maximum Transmission Unit
			public int time_out;       // in second, -1 means infinite
			public int encoder;        // encoder type (one of JackNetEncoder)
			public int kbps;           // KB per second for CELT or OPUS codec
			public int latency;        // network latency in number of buffers
		}

		public struct jack_master_t
		{
			public int audio_input;                    // master audio physical outputs (-1 to take slave wanted audio inputs)
			public int audio_output;                   // master audio physical inputs (-1 to take slave wanted audio outputs)
			public int midi_input;                     // master MIDI physical outputs (-1 to take slave wanted MIDI inputs)
			public int midi_output;                    // master MIDI physical inputs (-1 to take slave wanted MIDI outputs)
			public uint /*jack_nframes_t*/ buffer_size;         // master buffer size
			public uint /*jack_nframes_t*/ sample_rate;         // master sample rate
			[MarshalAs(UnmanagedType.LPWStr, SizeConst = Constants.MASTER_NAME_SIZE)]
			public string master_name; // master machine name
			int time_out;                       // in second, -1 means infinite
			int partial_cycle;                  // if 'true', partial buffers will be used 
		}
	}
}
