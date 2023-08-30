using System;
using System.Collections.Generic;

namespace MBS.Audio.MIDI
{
	public class SoundCard
	{
		public static SoundCard GetDefaultSoundCard()
		{
			return GetSoundCardByID(0);
		}
		public static SoundCard GetSoundCardByID(int id)
		{
			SoundCard[] cards = GetAllSoundCards();
			if (id >= 0 && id < cards.Length)
			{
				return cards[id];
			}
			return null;
		}
		public static SoundCard[] GetAllSoundCards()
		{
			switch (Environment.OSVersion.Platform)
			{
				case PlatformID.MacOSX:
				case PlatformID.Unix:
				{
					int cardNum = -1;
					List<SoundCard> cards = new List<SoundCard>();
					while (true)
					{
						int error = Internal.Linux.Alsa.Methods.snd_card_next(ref cardNum);
						Internal.Linux.Alsa.Methods.snd_error_code_to_exception(error);
						if (cardNum == -1)
						{
							break;
						}
						cards.Add(SoundCard.FromID(cardNum));
					}
					return cards.ToArray();
				}
				case PlatformID.Win32NT:
				case PlatformID.Win32S:
				case PlatformID.Win32Windows:
				case PlatformID.WinCE:
				{
					List<SoundCard> cards = new List<SoundCard>();
					SoundCard card = new SoundCard(0);
					cards.Add(card);
					return cards.ToArray();
				}
				case PlatformID.Xbox:
				{
					break;
				}
			}
			throw new PlatformNotSupportedException();
		}

		private int mvarID = 0;
		public int ID { get { return mvarID; } }

		private SoundCard(int id)
		{
			mvarID = id;
		}

		public static SoundCard FromID(int id)
		{
			return new SoundCard(id);
		}

		private IntPtr mvarHandle = IntPtr.Zero;
		public IntPtr Handle { get { return mvarHandle; } }

		public void Open()
		{
			switch (Environment.OSVersion.Platform)
			{
				case PlatformID.MacOSX:
				case PlatformID.Unix:
				{
					mvarHandle = IntPtr.Zero;
					int error = Internal.Linux.Alsa.Methods.snd_ctl_open(ref mvarHandle, "hw:" + mvarID.ToString(), Internal.Linux.Alsa.Constants.SoundOpenFlags.None);
					Internal.Linux.Alsa.Methods.snd_error_code_to_exception(error);
					return;
				}
				case PlatformID.Win32NT:
				case PlatformID.Win32S:
				case PlatformID.Win32Windows:
				case PlatformID.WinCE:
				{
					return;
				}
				case PlatformID.Xbox:
				{
					break;
				}
			}
			throw new PlatformNotSupportedException();
		}
		public void Close()
		{
			switch (Environment.OSVersion.Platform)
			{
				case PlatformID.MacOSX:
				case PlatformID.Unix:
				{
					Internal.Linux.Alsa.Methods.snd_ctl_close(mvarHandle);
					mvarHandle = IntPtr.Zero;
					return;
				}
				case PlatformID.Win32NT:
				case PlatformID.Win32S:
				case PlatformID.Win32Windows:
				case PlatformID.WinCE:
				{
					return;
				}
				case PlatformID.Xbox:
				{
					break;
				}
			}
			throw new PlatformNotSupportedException();
		}

		public MidiDevice GetDefaultMidiInputDevice()
		{
			MidiDevice[] devices = GetMidiInputDevices();
			if (devices.Length == 0) return null;
			return devices[0];
		}
		public MidiDevice[] GetMidiInputDevices()
		{
			switch (Environment.OSVersion.Platform)
			{
				case PlatformID.MacOSX:
				case PlatformID.Unix:
				{
					if (mvarHandle == IntPtr.Zero) throw new InvalidOperationException("Sound card is not open");

					// Start with the first MIDI device on this card
					int devNum = -1;
					List<MidiDevice> devices = new List<MidiDevice>();
					while (true)
					{
						// Get the number of the next MIDI device on this card
						int error = Internal.Linux.Alsa.Methods.snd_ctl_rawmidi_next_device(mvarHandle, ref devNum);
						Internal.Linux.Alsa.Methods.snd_error_code_to_exception(error);

						// No more MIDI devices on this card? ALSA sets "devNum" to -1 if so.
						// NOTE: It's possible that this sound card may have no MIDI devices on it
						// at all, for example if it's only a digital audio card
						if (devNum < 0) break;

						MidiDevice device = new MidiDevice((uint)devNum, this);
						if (device == null) continue;

						devices.Add(device);
					}
					return devices.ToArray();
				}
				case PlatformID.Win32NT:
				case PlatformID.Win32S:
				case PlatformID.Win32Windows:
				case PlatformID.WinCE:
				{
					uint count = Internal.Windows.Methods.midiInGetNumDevs();
					List<MidiDevice> list = new List<MidiDevice>();
					for (uint i = 0; i < count; i++)
					{
						MidiDevice device = new MidiDevice(i, this);
						if (device == null) continue;
						list.Add(device);
					}
					return list.ToArray();
				}
				case PlatformID.Xbox:
				{
					break;
				}
			}
			throw new PlatformNotSupportedException();
		}
		public MidiDevice GetDefaultMidiOutputDevice()
		{
			MidiDevice[] devices = GetMidiOutputDevices();
			if (devices.Length == 0) return null;
			return devices[0];
		}
		public MidiDevice[] GetMidiOutputDevices()
		{
			switch (Environment.OSVersion.Platform)
			{
				case PlatformID.MacOSX:
				case PlatformID.Unix:
				{
					// Start with the first MIDI device on this card
					int devNum = -1;
					List<MidiDevice> devices = new List<MidiDevice>();
					while (true)
					{
						// Get the number of the next MIDI device on this card
						int error = Internal.Linux.Alsa.Methods.snd_ctl_rawmidi_next_device(mvarHandle, ref devNum);
						Internal.Linux.Alsa.Methods.snd_error_code_to_exception(error);

						// No more MIDI devices on this card? ALSA sets "devNum" to -1 if so.
						// NOTE: It's possible that this sound card may have no MIDI devices on it
						// at all, for example if it's only a digital audio card
						if (devNum < 0) break;

						MidiDevice device = new MidiDevice((uint)devNum, this);
						if (device == null) continue;

						devices.Add(device);
					}
					return devices.ToArray();
				}
				case PlatformID.Win32NT:
				case PlatformID.Win32S:
				case PlatformID.Win32Windows:
				case PlatformID.WinCE:
					{
						uint count = Internal.Windows.Methods.midiOutGetNumDevs();
						List<MidiDevice> list = new List<MidiDevice>();
						for (uint i = 0; i < count; i++)
						{
							MidiDevice device = new MidiDevice(i, this);
							if (device == null) continue;
							list.Add(device);
						}
						return list.ToArray();
					}
				case PlatformID.Xbox:
				{
					break;
				}
			}
			throw new PlatformNotSupportedException();
		}

		public MidiDevice[] GetMidiDevices(MidiDeviceFunctionality functionality)
		{
			List<MidiDevice> list = new List<MidiDevice>();
			if ((functionality & MidiDeviceFunctionality.Input) == MidiDeviceFunctionality.Input)
			{
				list.AddRange(GetMidiInputDevices());
			}
			if ((functionality & MidiDeviceFunctionality.Input) == MidiDeviceFunctionality.Input)
			{
				list.AddRange(GetMidiOutputDevices());
			}
			return list.ToArray();
		}
	}
}
