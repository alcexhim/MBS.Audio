using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MBS.Audio.MIDI
{
	[Flags()]
	public enum DeviceOptionalFunctionality : uint
	{
		None = 0x0,
		/// <summary>
		/// Supports volume control.
		/// </summary>
		Volume = 0x1,
		/// <summary>
		/// Supports separate left and right volume control.
		/// </summary>
		StereoVolume = 0x2,
		/// <summary>
		/// Supports patch caching.
		/// </summary>
		PatchCaching = 0x4,
		/// <summary>
		/// Provides direct support for the midiStreamOut function.
		/// </summary>
		Streaming = 0x8
	}
}
