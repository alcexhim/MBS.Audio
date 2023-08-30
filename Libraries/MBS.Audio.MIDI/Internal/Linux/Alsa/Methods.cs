using System;
using System.Text;
using System.Runtime.InteropServices;

namespace MBS.Audio.MIDI.Internal.Linux.Alsa
{
	internal static class Methods
	{
		private const string LIBRARY_FILENAME = "libasound.so.2";

		/// <summary>
		/// Retrieves the ID of the sound card directly after the sound card with ID cardNum.
		/// </summary>
		/// <param name="cardNum">The ID of the sound card at which to begin enumeration. This can be -1 to retrieve the first sound card.</param>
		[DllImport(LIBRARY_FILENAME)]
		public static extern int snd_card_next(ref int cardNum);

		/// <summary>
		/// Opens the card located at the specified device path and returns the handle in <see cref="cardHandle" />.
		/// </summary>
		/// <param name="cardHandle">Returns a handle to the sound card associated with the device at the specified path.</param>
		/// <param name="devicePath">The path to the device to open.</param>
		/// <param name="flags"></param>
		[DllImport(LIBRARY_FILENAME)]
		public static extern int snd_ctl_open(ref IntPtr cardHandle, string devicePath, Constants.SoundOpenFlags flags);

		/// <summary>
		/// Closes the sound card referenced by the specified handle.
		/// </summary>
		/// <param name="cardHandle">The handle of the sound card to close.</param>
		[DllImport(LIBRARY_FILENAME)]
		public static extern void snd_ctl_close(IntPtr cardHandle);

		/// <summary>
		/// Retrieves a friendly description for the specified error code.
		/// </summary>
		/// <param name="error">The error code for which to return a description.</param>
		[DllImport(LIBRARY_FILENAME)]
		public static extern string snd_strerror(int error);

		/// <summary>
		/// Get the number of the next MIDI device on the specified card.
		/// </summary>
		/// <param name="cardHandle">Handle to the sound card on which to fetch the next MIDI device.</param>
		/// <param name="devNum">Receives the ID of the next MIDI device on the specified card.</param>
		[DllImport(LIBRARY_FILENAME)]
		public static extern int snd_ctl_rawmidi_next_device(IntPtr cardHandle, ref int devNum);

		/// <summary>
		/// Opens the MIDI device at the specified device path.
		/// </summary>
		/// <param name="unknown">Unknown.</param>
		/// <param name="handle">Receives a handle to the MIDI device at the specified device path.</param>
		/// <param name="devicePath">The path of the MIDI device to open; i.e. "hw:cardnum,devnum,subdevnum"</param>
		/// <param name="flags"></param>
		[DllImport(LIBRARY_FILENAME)]
		public static extern int snd_rawmidi_open(ref IntPtr inputHandle, ref IntPtr outputHandle, string devicePath, int flags);

		/// <summary>
		/// Writes the specified buffer to the MIDI device with the specified handle.
		/// </summary>
		/// <param name="handle">The handle to the MIDI device on which to write.</param>
		/// <param name="buffer">The data to write to the MIDI device.</param>
		/// <param name="bufferLength">The length of the buffer.</param>
		[DllImport(LIBRARY_FILENAME)]
		public static extern int snd_rawmidi_write(IntPtr handle, byte[] buffer, int bufferLength);
		/// <summary>
		/// Read the specified buffer from the MIDI device with the specified handle.
		/// </summary>
		/// <param name="handle">The handle to the MIDI device on which to read.</param>
		/// <param name="buffer">The data to write to the MIDI device.</param>
		/// <param name="bufferLength">The length of the buffer.</param>
		[DllImport(LIBRARY_FILENAME)]
		public static extern int snd_rawmidi_read(IntPtr handle, byte[] buffer, int bufferLength);

		/// <summary>
		/// Closes the MIDI device referenced by the specified handle.
		/// </summary>
		/// <param name="handle">The handle of the MIDI device to close.</param>
		[DllImport(LIBRARY_FILENAME)]
		public static extern int snd_rawmidi_close(IntPtr handle);

		#region RawMidi Info
		[DllImport(LIBRARY_FILENAME)]
		public static extern int snd_rawmidi_info_malloc(ref IntPtr hInfo);

		[DllImport(LIBRARY_FILENAME)]
		public static extern void snd_rawmidi_info_free(IntPtr hInfo);

		[DllImport(LIBRARY_FILENAME)]
		public static extern int snd_rawmidi_info(IntPtr handle, IntPtr hInfo);

		[DllImport(LIBRARY_FILENAME)]
		public static extern int snd_ctl_rawmidi_info(IntPtr handle, IntPtr hInfo);

		[DllImport(LIBRARY_FILENAME)]
		public static extern string snd_rawmidi_info_get_name(ref IntPtr hInfo);

		[DllImport(LIBRARY_FILENAME)]
		public static extern void snd_rawmidi_info_set_device(IntPtr hInfo, uint deviceID);
		[DllImport(LIBRARY_FILENAME)]
		public static extern void snd_rawmidi_info_set_subdevice(IntPtr hInfo, int subDeviceID);
		[DllImport(LIBRARY_FILENAME)]
		public static extern void snd_rawmidi_info_set_stream(IntPtr hInfo, Constants.snd_rawmidi_stream stream);
		#endregion

		public static void snd_error_code_to_exception(int error)
		{
			if (error < 0)
			{
				throw new Exception(snd_strerror(error));
			}
		}
	}
}
