using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MBS.Audio.MIDI
{
#if !DEBUG
	[System.Diagnostics.DebuggerNonUserCode()]
#endif
	public class MidiDevice
	{
		protected uint mvarID = 0;
		public uint ID { get { return mvarID; } }

		protected IntPtr mvarInputHandle = IntPtr.Zero;
		protected IntPtr mvarOutputHandle = IntPtr.Zero;
		public IntPtr Handle { get { return mvarInputHandle; } }

		public SoundCard Parent { get; private set; }

		internal MidiDevice(uint id, SoundCard parent)
		{
			mvarID = id;
			Parent = parent;
			try
			{
				Refresh();
			}
			catch
			{
			}
		}

		public void Open(MidiDeviceFunctionality functionality = MidiDeviceFunctionality.Any)
		{
			switch (Environment.OSVersion.Platform)
			{
				case PlatformID.MacOSX:
				case PlatformID.Unix:
				{
					mvarInputHandle = IntPtr.Zero;
					mvarOutputHandle = IntPtr.Zero;
					IntPtr dummy = IntPtr.Zero;
					string str = "hw:" + Parent.ID.ToString() + "," + mvarID.ToString() + ",0";
					int error = Internal.Linux.Alsa.Methods.snd_rawmidi_open(ref mvarInputHandle, ref mvarOutputHandle, str, 0);
					Internal.Linux.Alsa.Methods.snd_error_code_to_exception(error);

					Refresh();
					return;
				}
				case PlatformID.Win32NT:
				case PlatformID.Win32S:
				case PlatformID.Win32Windows:
				case PlatformID.WinCE:
				{
					mvarInputHandle = IntPtr.Zero;

                    mvarCallback = new Internal.Windows.Delegates.MidiCallback(MidiCallback);
					Internal.Windows.Constants.MidiError error = Internal.Windows.Methods.midiInOpen(out mvarInputHandle, mvarID, mvarCallback, IntPtr.Zero, Internal.Windows.Constants.MidiOpenFlags.Function);
					Internal.Windows.Methods.midiErrorToException(error);

                    byHandle[mvarInputHandle] = this;

					error = Internal.Windows.Methods.midiOutOpen(out mvarOutputHandle, mvarID, new Internal.Windows.Delegates.MidiCallback(MidiCallback), IntPtr.Zero, Internal.Windows.Constants.MidiOpenFlags.Function);
					Internal.Windows.Methods.midiErrorToException(error);
					return;
				}
				case PlatformID.Xbox:
				{
					break;
				}
			}
			throw new PlatformNotSupportedException();
		}

		public bool AutoFlush { get; set; } = true;

		/// <summary>
		/// Updates the MIDI device information.
		/// </summary>
		public void Refresh()
		{
			switch (Environment.OSVersion.Platform)
			{
				case PlatformID.MacOSX:
				case PlatformID.Unix:
				{
					if (mvarInputHandle == IntPtr.Zero) return;

					IntPtr hInfo = IntPtr.Zero;
					Internal.Linux.Alsa.Methods.snd_rawmidi_info_malloc(ref hInfo);
					Internal.Linux.Alsa.Methods.snd_rawmidi_info_set_device(hInfo, mvarID);
					Internal.Linux.Alsa.Methods.snd_rawmidi_info_set_stream(hInfo, Internal.Linux.Alsa.Constants.snd_rawmidi_stream.Output);
					Internal.Linux.Alsa.Methods.snd_rawmidi_info_set_subdevice(hInfo, -1);

					Internal.Linux.Alsa.Methods.snd_rawmidi_info(mvarInputHandle, hInfo);
					// mvarName = Internal.Linux.Alsa.Methods.snd_rawmidi_info_get_name(ref hInfo);
					Internal.Linux.Alsa.Methods.snd_rawmidi_info_free(hInfo);
					return;
				}
				case PlatformID.Win32NT:
				case PlatformID.Win32S:
				case PlatformID.Win32Windows:
				case PlatformID.WinCE:
				{
						// output 
					Internal.Windows.Structures.MIDIOUTCAPS caps = new Internal.Windows.Structures.MIDIOUTCAPS();
					Internal.Windows.Methods.midiOutGetDevCaps(mvarID, out caps, (uint)System.Runtime.InteropServices.Marshal.SizeOf(caps));

					mvarManufacturerID = caps.wMid;
					mvarProductID = caps.wPid;
					mvarName = caps.szPname;
					mvarDeviceType = caps.wTechnology;
					mvarMaximumVoices = caps.wVoices;
					mvarMaximumNotes = caps.wNotes;
					mvarDriverVersion = new Version(caps.vDriverVersionMajor, caps.vDriverVersionMinor);
					mvarSupportedChannels = caps.wChannelMask;
					mvarSupportedFunctionality = caps.dwSupport;

					// input 
					Internal.Windows.Structures.MIDIINCAPS capsIn = new Internal.Windows.Structures.MIDIINCAPS();
					Internal.Windows.Methods.midiInGetDevCaps(mvarID, out capsIn, (uint)System.Runtime.InteropServices.Marshal.SizeOf(capsIn));

					mvarManufacturerID = capsIn.wMid;
					mvarProductID = capsIn.wPid;
					mvarName = capsIn.szPname;
					mvarDriverVersion = new Version(capsIn.vDriverVersionMajor, capsIn.vDriverVersionMinor);
					mvarSupportedFunctionality = capsIn.dwSupport;
					return;
				}
				case PlatformID.Xbox:
				{
					break;
				}
			}
			throw new PlatformNotSupportedException();
		}
		/// <summary>
		/// Turns off all notes on all MIDI channels for this MIDI output device.
		/// </summary>
		public void Reset()
		{
			switch (Environment.OSVersion.Platform)
			{
				case PlatformID.MacOSX:
				case PlatformID.Unix:
				{
					break;
				}
				case PlatformID.Win32NT:
				case PlatformID.Win32S:
				case PlatformID.Win32Windows:
				case PlatformID.WinCE:
				{
					if (mvarInputHandle != IntPtr.Zero)
					{
						Internal.Windows.Constants.MidiError error = Internal.Windows.Methods.midiInReset(mvarInputHandle);
						Internal.Windows.Methods.midiErrorToException(error);
					}
					if (mvarOutputHandle != IntPtr.Zero)
					{
						Internal.Windows.Constants.MidiError error = Internal.Windows.Methods.midiInReset(mvarOutputHandle);
						Internal.Windows.Methods.midiErrorToException(error);
					}
					return;
				}
				case PlatformID.Xbox:
				{
					break;
				}
			}
			throw new PlatformNotSupportedException();
		}
		public int Flush()
		{
			// get the bytes currently in the stream
			byte[] buffer = stream.ToArray();

			// clear out the memory stream
			stream = new System.IO.MemoryStream();

			switch (Environment.OSVersion.Platform)
			{
				case PlatformID.MacOSX:
				case PlatformID.Unix:
				{
					int error = Internal.Linux.Alsa.Methods.snd_rawmidi_write(mvarOutputHandle, buffer, buffer.Length);
					Internal.Linux.Alsa.Methods.snd_error_code_to_exception(error);
					return error;
				}
				case PlatformID.Win32NT:
				case PlatformID.Win32S:
				case PlatformID.Win32Windows:
				case PlatformID.WinCE:
				{
					System.IO.BinaryReader br = new System.IO.BinaryReader(new System.IO.MemoryStream(buffer));
					while (br.BaseStream.Position < br.BaseStream.Length)
					{
						byte msgtype = br.ReadByte();
						byte param1 = 0;
						byte param2 = 0;
						if (br.BaseStream.Position < br.BaseStream.Length)
						{
							param1 = br.ReadByte();
							if (br.BaseStream.Position < br.BaseStream.Length)
							{
								param2 = br.ReadByte();
							}
						}

						int dwMsg = BitConverter.ToInt32(new byte[] { msgtype, param1, param2, 0 }, 0);

						Internal.Windows.Constants.MidiError error = Internal.Windows.Methods.midiOutShortMsg((uint)mvarOutputHandle.ToInt32(), dwMsg);
						Internal.Windows.Methods.midiErrorToException(error);
					}
					br.Close();
					buffer = new byte[0];
					return 0;
				}
				case PlatformID.Xbox:
				{
					break;
				}
			}
			throw new PlatformNotSupportedException();
		}
		/// <summary>
		/// Closes this MIDI device.
		/// </summary>
		public void Close()
		{
			switch (Environment.OSVersion.Platform)
			{
				case PlatformID.MacOSX:
				case PlatformID.Unix:
				{
					if (mvarInputHandle != IntPtr.Zero)
					{
						int error = Internal.Linux.Alsa.Methods.snd_rawmidi_close(mvarInputHandle);
						Internal.Linux.Alsa.Methods.snd_error_code_to_exception(error);
						mvarInputHandle = IntPtr.Zero;
					}
					if (mvarOutputHandle != IntPtr.Zero)
					{
						int error = Internal.Linux.Alsa.Methods.snd_rawmidi_close(mvarOutputHandle);
						Internal.Linux.Alsa.Methods.snd_error_code_to_exception(error);
						mvarOutputHandle = IntPtr.Zero;
					}
					return;
				}
				case PlatformID.Win32NT:
				case PlatformID.Win32S:
				case PlatformID.Win32Windows:
				case PlatformID.WinCE:
				{
					try
					{
						if (mvarInputHandle != IntPtr.Zero)
						{
							Internal.Windows.Methods.midiInClose(mvarInputHandle);
						}
						if (mvarOutputHandle != IntPtr.Zero)
						{
							Internal.Windows.Methods.midiOutClose(mvarOutputHandle);
						}
					}
					catch
					{
					}
					if (byHandle.ContainsKey(mvarInputHandle))
					{
						byHandle.Remove(mvarInputHandle);
					}
					if (byHandle.ContainsKey(mvarOutputHandle))
					{
						byHandle.Remove(mvarOutputHandle);
					}

					mvarInputHandle = IntPtr.Zero;
					mvarOutputHandle = IntPtr.Zero;
					return;
				}
				case PlatformID.Xbox:
				{
					break;
				}
			}
			throw new InvalidOperationException();
		}

		
		private System.IO.MemoryStream stream = new System.IO.MemoryStream();
		public int Write(byte[] buffer)
		{
			return Write(buffer, 0, buffer.Length);
		}
		public int Write(byte[] buffer, int start, int length)
		{
			stream.Write(buffer, start, length);
			if (AutoFlush) return Flush();
			return length;
		}

		protected string mvarName = String.Empty;
		public string Name { get { return mvarName; } }

		public event MessageReceivedEventHandler MessageReceived;
		protected virtual void OnMessageReceived(MessageReceivedEventArgs e)
		{
			MessageReceived?.Invoke(this, e);
		}

		private static Dictionary<IntPtr, MidiDevice> byHandle = new Dictionary<IntPtr, MidiDevice>();

		private static void MidiCallback(IntPtr hmo, uint wMsg, IntPtr dwInstance, uint dwMidiMessage, uint dwTimestamp)
		{
			MidiDevice device = null;
			if (byHandle.ContainsKey(hmo)) device = byHandle[hmo];
			if (device == null) return;

			byte[] msgdata = BitConverter.GetBytes(dwMidiMessage);
			MessageType type = (MessageType)(msgdata[0] & 0xF0);
			byte channel = (byte)((msgdata[0] & 0x0F) + 1);

			byte parameter1 = (byte)msgdata[1];
			byte parameter2 = (byte)msgdata[2];

			device.OnMessageReceived(new MessageReceivedEventArgs(new Message(type, channel, parameter1, parameter2), dwInstance));
		}

		public void Send(params Message[] messages)
		{
			foreach (Message message in messages)
			{
				byte status = (byte)((byte)message.MessageType | (message.Channel - 1));

				System.IO.MemoryStream ms = new System.IO.MemoryStream();
				System.IO.BinaryWriter bw = new System.IO.BinaryWriter(ms);
				byte[] payload = new byte[] { status, message.Parameter1, message.Parameter2 }; // 93, 90 };
				bw.Write(payload);
				bw.Close();

				Write(ms.ToArray());
			}
		}

		private Internal.Windows.Delegates.MidiCallback mvarCallback = null;

		private System.Threading.Thread tLinuxThread = null;
		private void tLinuxThread_ThreadStart()
		{
			while (true)
			{
				byte[] buffer = new byte[16];
				Internal.Linux.Alsa.Methods.snd_rawmidi_read(mvarInputHandle, buffer, buffer.Length);

				byte channelId = (byte)(((byte)(buffer[0] << 4)) >> 4);
				byte messageTypeId = (byte)((byte)(buffer[0] >> 4) << 4);
				OnMessageReceived(new MessageReceivedEventArgs(new Message((MessageType)messageTypeId, (byte)(channelId + 1), buffer[1], buffer[2]), IntPtr.Zero));

				System.Threading.Thread.Sleep(10);
			}
		}
		

		/// <summary>
		/// Starts listening for MIDI input.
		/// </summary>
		public void Start()
        {
            switch (Environment.OSVersion.Platform)
            {
                case PlatformID.MacOSX:
                case PlatformID.Unix:
                {
					tLinuxThread = new System.Threading.Thread(tLinuxThread_ThreadStart);
					tLinuxThread.Start();
                    break;
                }
                case PlatformID.Win32NT:
                case PlatformID.Win32S:
                case PlatformID.Win32Windows:
                case PlatformID.WinCE:
                {
                    Internal.Windows.Constants.MidiError error = Internal.Windows.Methods.midiInStart(mvarInputHandle);
                    Internal.Windows.Methods.midiErrorToException(error);
                    return;
                }
            }
        }

        /// <summary>
        /// Stops listening for MIDI input.
        /// </summary>
        public void Stop()
        {
            switch (Environment.OSVersion.Platform)
            {
                case PlatformID.MacOSX:
                case PlatformID.Unix:
                {
					tLinuxThread.Abort();
					tLinuxThread = null;
                    break;
                }
                case PlatformID.Win32NT:
                case PlatformID.Win32S:
                case PlatformID.Win32Windows:
                case PlatformID.WinCE:
                {
                    Internal.Windows.Constants.MidiError error = Internal.Windows.Methods.midiInStop(mvarInputHandle);
                    Internal.Windows.Methods.midiErrorToException(error);
                    return;
                }
            }
        }

		protected uint mvarManufacturerID = 0;
		public uint ManufacturerID { get { return mvarManufacturerID; } }
		protected uint mvarProductID = 0;
		public uint ProductID { get { return mvarProductID; } }
		protected ushort mvarMaximumVoices = 0;
		public ushort MaximumVoices { get { return mvarMaximumVoices; } }
		protected ushort mvarMaximumNotes = 0;
		public ushort MaximumNotes { get { return mvarMaximumNotes; } }
		protected ushort mvarSupportedChannels = 0;
		public ushort SupportedChannels { get { return mvarSupportedChannels; } }
		protected Version mvarDriverVersion = new Version(1, 0);
		public Version DriverVersion { get { return mvarDriverVersion; } }
		protected DeviceType mvarDeviceType = DeviceType.None;
		public DeviceType DeviceType { get { return mvarDeviceType; } }
		protected DeviceOptionalFunctionality mvarSupportedFunctionality = DeviceOptionalFunctionality.None;
		public DeviceOptionalFunctionality SupportedFunctionality { get { return mvarSupportedFunctionality; } }
	}
}
