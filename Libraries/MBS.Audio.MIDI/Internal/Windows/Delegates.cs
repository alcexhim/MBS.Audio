using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MBS.Audio.MIDI.Internal.Windows
{
	internal static class Delegates
	{
		/// <summary>
		/// The MidiCallback function is the callback function for handling incoming and outgoing MIDI
		/// messages. MidiCallback is a placeholder for the application-supplied function name. The
		/// address of the function can be specified in the callback-address parameter of the
		/// midiOutOpen/midiInOpen functions.
		/// </summary>
		/// <param name="hmo">Handle to the MIDI device associated with the callback function.</param>
		/// <param name="wMsg">MIDI input/output message.</param>
		/// <param name="dwInstance">Instance data supplied by using the midiInOpen/midiOutOpen function.</param>
		/// <param name="dwParam1">Message parameter.</param>
		/// <param name="dwParam2">Message parameter.</param>
		/// <remarks>
		/// Applications should not call any multimedia functions from inside the callback function, as
		/// doing so can cause a deadlock. Other system functions can safely be called from the
		/// callback.
		/// </remarks>
		public delegate void MidiCallback(IntPtr hmo, uint wMsg, IntPtr dwInstance, uint dwMidiMessage, uint dwTimestamp);
	}
}
