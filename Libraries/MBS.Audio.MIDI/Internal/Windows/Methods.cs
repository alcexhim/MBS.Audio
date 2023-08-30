using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace MBS.Audio.MIDI.Internal.Windows
{
	internal static class Methods
	{
		public const string LIBRARY_FILENAME = "winmm.dll";

		#region Input
		[DllImport(LIBRARY_FILENAME)]
		public static extern uint midiInGetNumDevs();
		[DllImport(LIBRARY_FILENAME)]
		public static extern Constants.MidiError midiInOpen(out IntPtr lphMidiIn, uint uDeviceID, Delegates.MidiCallback dwCallback, IntPtr dwCallbackInstance, Constants.MidiOpenFlags dwFlags);
		[DllImport(LIBRARY_FILENAME)]
		public static extern Constants.MidiError midiInGetDevCaps(uint uDeviceID, out Structures.MIDIINCAPS lpMidiInCaps, uint cbMidiInCaps);
		[DllImport(LIBRARY_FILENAME)]
		public static extern Constants.MidiError midiInReset(IntPtr hmo);
		[DllImport(LIBRARY_FILENAME)]
		public static extern Constants.MidiError midiInStart(IntPtr hMidiIn);
		[DllImport(LIBRARY_FILENAME)]
		public static extern Constants.MidiError midiInStop(IntPtr hMidiIn);
		[DllImport(LIBRARY_FILENAME)]
		public static extern Constants.MidiError midiInClose(IntPtr hmo);
		#endregion

		#region Output
		[DllImport(LIBRARY_FILENAME)]
		public static extern uint midiOutGetNumDevs();
		[DllImport(LIBRARY_FILENAME)]
		public static extern Constants.MidiError midiOutOpen(out IntPtr lphmo, uint uDeviceID, Delegates.MidiCallback dwCallback, IntPtr dwCallbackInstance, Constants.MidiOpenFlags dwFlags);
		/// <summary>
		/// Queries a specified MIDI output device to determine its capabilities.
		/// </summary>
		/// <param name="uDeviceID">
		/// Identifier of the MIDI output device. The device identifier specified by this parameter
		/// varies from zero to one less than the number of devices present. The MIDI_MAPPER constant
		/// is also a valid device identifier. This parameter can also be a properly cast device
		/// handle.
		/// </param>
		/// <param name="lpMidiOutCaps">
		/// Pointer to a <see cref="MIDIOUTCAPS"/> structure. This structure is filled with
		/// information about the capabilities of the device.
		/// </param>
		/// <param name="cbMidiOutCaps">
		/// Size, in bytes, of the <see cref="MIDIOUTCAPS" /> structure. Only cbMidiOutCaps bytes (or
		/// less) of information is copied to the location pointed to by lpMidiOutCaps. If
		/// cbMidiOutCaps is zero, nothing is copied, and the function returns MMSYSERR_NOERROR.
		/// </param>
		/// <returns></returns>
		[DllImport(LIBRARY_FILENAME)]
		public static extern Constants.MidiError midiOutGetDevCaps(uint uDeviceID, out Structures.MIDIOUTCAPS lpMidiOutCaps, uint cbMidiOutCaps);
		[DllImport(LIBRARY_FILENAME)]
		public static extern Constants.MidiError midiOutReset(IntPtr hmo);
		[DllImport(LIBRARY_FILENAME)]
		public static extern Constants.MidiError midiOutClose(IntPtr hmo);
		[DllImport(LIBRARY_FILENAME)]
		public static extern Constants.MidiError midiOutShortMsg(uint mvarID, int dwMsg);
		#endregion

		[System.Diagnostics.DebuggerNonUserCode()]
		internal static void midiErrorToException(Constants.MidiError error)
		{
			switch (error)
			{
				case Constants.MidiError.InvalidHandle:
				{
					throw new ArgumentException("Invalid handle");
				}
				case Constants.MidiError.InvalidParameter:
				{
					throw new ArgumentException();
				}
				case Constants.MidiError.BadDeviceID:
				{
					throw new ArgumentException("Bad device ID");
				}
				case Constants.MidiError.Unsupported:
				{
					throw new NotSupportedException();
				}
			}
		}
	}
}
