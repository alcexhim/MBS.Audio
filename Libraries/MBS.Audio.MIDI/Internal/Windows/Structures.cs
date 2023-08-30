using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace MBS.Audio.MIDI.Internal.Windows
{
	internal static class Structures
	{
		[StructLayout(LayoutKind.Sequential)]
		public struct MIDIOUTCAPS
		{
			/// <summary>
			/// Manufacturer identifier of the device driver for the MIDI output device.
			/// </summary>
			public ushort wMid;
			/// <summary>
			/// Product identifier of the MIDI output device.
			/// </summary>
			public ushort wPid;

			#region MMVERSION
			/// <summary>
			/// Major version number of the device driver for the MIDI output device.
			/// </summary>
			public byte vDriverVersionMinor;
			/// <summary>
			/// Minor version number of the device driver for the MIDI output device.
			/// </summary>
			public byte vDriverVersionMajor;

			// extra two bytes in Windows 7 MMVERSION ???
			public byte vDriverVersionBuild;
			public byte vDriverVersionRevision;
			#endregion

			/// <summary>
			/// Product name in a null-terminated string.
			/// </summary>
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
			public string szPname;
			/// <summary>
			/// Type of the MIDI output device.
			/// </summary>
			public DeviceType wTechnology;
			/// <summary>
			/// Number of voices supported by an internal synthesizer device. If the device is a port,
			/// this member is not meaningful and is set to 0.
			/// </summary>
			public ushort wVoices;
			/// <summary>
			/// Maximum number of simultaneous notes that can be played by an internal synthesizer
			/// device. If the device is a port, this member is not meaningful and is set to 0.
			/// </summary>
			public ushort wNotes;
			/// <summary>
			/// Channels that an internal synthesizer device responds to, where the least significant
			/// bit refers to channel 0 and the most significant bit to channel 15. Port devices that
			/// transmit on all channels set this member to 0xFFFF.
			/// </summary>
			public ushort wChannelMask;
			/// <summary>
			/// Optional functionality supported by the device.
			/// </summary>
			public DeviceOptionalFunctionality dwSupport;
		}

		[StructLayout(LayoutKind.Sequential)]
		public struct MIDIINCAPS
		{
			public ushort wMid;
			public ushort wPid;

			#region MMVERSION
			/// <summary>
			/// Major version number of the device driver for the MIDI input device.
			/// </summary>
			public byte vDriverVersionMinor;
			/// <summary>
			/// Minor version number of the device driver for the MIDI input device.
			/// </summary>
			public byte vDriverVersionMajor;

			// extra two bytes in Windows 7 MMVERSION ???
			public byte vDriverVersionBuild;
			public byte vDriverVersionRevision;
			#endregion

			/// <summary>
			/// Product name in a null-terminated string.
			/// </summary>
			[MarshalAs(UnmanagedType.ByValTStr, SizeConst = 32)]
			public string szPname;

			/// <summary>
			/// Optional functionality supported by the device.
			/// </summary>
			public DeviceOptionalFunctionality dwSupport;
		}
	}
}
