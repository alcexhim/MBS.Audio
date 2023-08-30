using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MBS.Audio.MIDI
{
	public enum DeviceType : ushort
	{
		None = 0,
		/// <summary>
		/// MIDI hardware port.
		/// </summary>
		HardwarePort = 1,
		/// <summary>
		/// Synthesizer.
		/// </summary>
		Synthesizer = 2,
		/// <summary>
		/// Square wave synthesizer.
		/// </summary>
		SquareWaveSynthesizer = 3,
		/// <summary>
		/// FM synthesizer.
		/// </summary>
		FMSynthesizer = 4,
		/// <summary>
		/// Microsoft MIDI mapper.
		/// </summary>
		MicrosoftMIDIMapper = 5,
		/// <summary>
		/// Hardware wavetable synthesizer.
		/// </summary>
		HardwareWavetable = 6,
		/// <summary>
		/// Software synthesizer.
		/// </summary>
		Software = 7
	}
}
