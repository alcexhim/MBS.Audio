using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MBS.Audio.MIDI.Internal.Windows
{
	internal static class Constants
	{
		// from:
		// https://github.com/downpoured/downpoured_midi_audio/blob/master/benmidi/trilled-midisketch/CSharpMidiToolkitV4_demo/Multimedia.Midi/Device%20Classes/DeviceException.cs

		public enum MidiError
		{
			None = 0,
			/// <summary>
			/// Unspecified error.
			/// </summary>
			Unspecified = 1,
			/// <summary>
			/// The specified device identifier is out of range.
			/// </summary>
			BadDeviceID = 2,
			/// <summary>
			/// Driver failed to enable.
			/// </summary>
			NotEnabled = 3,
			/// <summary>
			/// The specified resource is already allocated.
			/// </summary>
			AlreadyAllocated = 4,
			/// <summary>
			/// The device handle is invalid.
			/// </summary>
			InvalidHandle = 5,
			/// <summary>
			/// No device driver is present.
			/// </summary>
			NoDriver = 6,
			/// <summary>
			/// The system is unable to allocate or lock memory.
			/// </summary>
			MemoryError = 7,
			Unsupported = 8,
			BadErrorNumber = 9,
			InvalidFlag = 10,
			/// <summary>
			/// The specified pointer or structure is invalid.
			/// </summary>
			InvalidParameter = 11,
			HandleBusy = 12,
			/// <summary>
			/// No MIDI port was found. This error occurs only when the mapper is opened.
			/// </summary>
			NoDevice = 68
		}
		/// <summary>
		/// Callback flag for opening the device.
		/// </summary>
		public enum MidiOpenFlags
		{
			/// <summary>
			/// There is no callback mechanism. This value is the default setting.
			/// </summary>
			None = 0x00000000,
			/// <summary>
			/// The dwCallback parameter is a window handle.
			/// </summary>
			Window = 0x00010000,
			/// <summary>
			/// The dwCallback parameter is a thread identifier.
			/// </summary>
			Thread = 0x00020000,
			/// <summary>
			/// The dwCallback parameter is a callback function address.
			/// </summary>
			Function = 0x00030000,
			/// <summary>
			/// The dwCallback parameter is an event handle. This callback mechanism is for output
			/// only.
			/// </summary>
			Event = 0x00050000
		}
	}
}
