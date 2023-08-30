using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MBS.Audio.MIDI
{
	public enum MessageType : byte
	{
		NoteOff = 0x80,
		NoteOn = 0x90,
		Aftertouch = 0xA0,
		ControlChange = 0xB0,
		PatchChange = 0xC0,
		ChannelPressure = 0xD0,
		PitchBend = 0xE0,

		/// <summary>
		/// start of system exclusive message
		/// </summary>
		SysexStart = 0xF0,
		SysexTimecode = 0xF1,
		SysexSongPosition = 0xF2,
		SysexSongSelect = 0xF3,
		SysexF4 = 0xF4,
		SysexF5 = 0xF5,
		SysexTuneRequest = 0xF6,
		SysexEnd = 0xF7,
		SysexRealtimeClock = 0xF8,
		SysexF9 = 0xF9,
		SysexRealtimeStart = 0xFA,
		SysexRealtimeContinue = 0xFB,
		SysexRealtimeStop = 0xFC,
		SysexFD = 0xFD,
		SysexActiveSensing = 0xFE,
		SysexReset = 0xFF
	}
}
