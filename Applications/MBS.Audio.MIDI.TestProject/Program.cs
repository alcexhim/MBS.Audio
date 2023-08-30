using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MBS.Audio.MIDI.TestProject
{
	class Program
	{
		static void Main(string[] args)
		{
			ListAllSoundCards();
			// return;

			// Launchpad: Message 144, channel 1, param1=button ID, param2=off(0) or on(127)

			// TestOutput();
			// TestInput();

			TestIO();
		}

		private static void TestIO()
		{
			SoundCard card = SoundCard.GetSoundCardByID(1);
			card.Open();

			MidiDevice id = card.GetDefaultMidiInputDevice();
			id.Open();
			id.MessageReceived += id_MessageReceived;
			id.Start();

			Console.ReadKey(true);
		}

		private static Dictionary<int, bool> zz = new Dictionary<int, bool>();

		static void id_MessageReceived(object sender, MessageReceivedEventArgs e)
		{
			Console.WriteLine("MonoMidi: receiving channel " + e.Message.Channel.ToString() + " message " + e.Message.MessageType.ToString() + " (" + e.Message.Parameter1.ToString() + ", " + e.Message.Parameter2.ToString() + ")");

			if (e.Message.MessageType == MessageType.NoteOn)
			{
				if (e.Message.Channel == 9)
				{
					if (zz.ContainsKey(e.Message.Parameter1) && zz[e.Message.Parameter1])
					{
						((MidiDevice)sender).Send(new Message[] { new Message(MessageType.NoteOn, 9, e.Message.Parameter1, 0) });
						zz[e.Message.Parameter1] = false;
					}
					else
					{
						((MidiDevice)sender).Send(new Message[] { new Message(MessageType.NoteOn, 9, e.Message.Parameter1, 96) });
						zz[e.Message.Parameter1] = true;
					}
				}
			}
		}


		private static void TestOutput()
		{
			SoundCard card = SoundCard.GetSoundCardByID(1);
			card.Open();

			// OutputDevice od = card.GetDefaultMidiOutputDevice();
			MidiDevice[] ods = card.GetMidiOutputDevices();
			if (ods.Length == 0)
			{
				Console.WriteLine("No MIDI output devices found!");
				Pause();
				return;
			}

			MidiDevice od = null;
			if (ods.Length > 1)
			{
				Console.WriteLine("Select MIDI output device to test:");
				foreach (MidiDevice _od in ods)
				{
					int i = Array.IndexOf<MidiDevice>(ods, _od);
					Console.WriteLine("\t[" + i.ToString().PadLeft(2, ' ') + "]\t" + _od.Name);
				}

				int index = 0;
				while (true)
				{
					Console.WriteLine();
					Console.Write("Your selection [0]: ");
					string str = Console.ReadLine();
					if (!String.IsNullOrEmpty(str))
					{
						if (!Int32.TryParse(str, out index))
						{
							Console.WriteLine("Invalid selection.  Please enter a valid index.");
							index = 0;
							continue;
						}
						if (index < 0 || index >= ods.Length)
						{
							Console.WriteLine("Invalid selection.  Please enter a valid index.");
							index = 0;
							continue;
						}
						else
						{
							break;
						}
					}
					else
					{
						break;
					}
				}
				od = ods[index];
			}
			else
			{
				od = ods[0];
			}

			if (od == null)
			{
				Console.WriteLine("Invalid MIDI output device!");
				Pause();
				return;
			}
			Console.WriteLine();

			byte type = 0xB0;
			byte channel = 7;
			channel -= 1;
			byte status = (byte)(type | channel);

			Console.WriteLine("Opening device " + od.ID.ToString() + " (\"" + od.Name + "\") on soundcard " + od.Parent.ID.ToString());
			od.Open();

			Console.WriteLine("Writing bytes to the device");

			// off, red(dim), red(medium), red(bright)
			// 0, 1, 2, 3
			// 4, 5, 6, 7,
			// 8, 9, 10, 11
			// 12, 13, 14, 15

			// LaunchCControl
			int ctr = 0;
			byte[] ifz = new byte[] { 0, 16, 24, 32, 48, 127 };
			while (true)
			{
				for (byte j = 0; j < 127; j++)
				{
					Console.WriteLine(j);
					od.Send(new Message(MessageType.NoteOn, 9, 9, j));
					System.Threading.Thread.Sleep(100);
				}
			}

			/*
			for (byte i = 0; i < 127; i++)
			{
				od.Send(new Message(MessageType.NoteOn, 1, i, 0));
			}

			for (byte i = 0; i < 127; i++)
			{
				Console.WriteLine("note id: " + i.ToString());
				for (int j = 0; j < 3; j++)
				{
					od.Send(new Message(MessageType.NoteOn, 1, i, 127));
					System.Threading.Thread.Sleep(100);
					od.Send(new Message(MessageType.NoteOn, 1, i, 0));
					System.Threading.Thread.Sleep(100);
				}
				System.Threading.Thread.Sleep(300);
			}
			*/

			od.Flush();
			Console.WriteLine("flushed");
			Console.ReadKey(true);

			Console.WriteLine("Flushing and closing device");
			od.Close();

			Console.WriteLine("Closing sound card");
			card.Close();
		}
		private static void TestInput()
		{
			SoundCard[] cards = SoundCard.GetAllSoundCards();
			if (cards.Length == 0)
			{
			}

			SoundCard card = null;
			if (cards.Length == 1)
			{
				card = cards[0];
			}
			else
			{
				card = cards[1];
			}

			card.Open();

			// OutputDevice od = card.GetDefaultMidiOutputDevice();
			MidiDevice[] ods = card.GetMidiInputDevices();
			if (ods.Length == 0)
			{
				Console.WriteLine("No MIDI input devices found!");
				Pause();
				return;
			}

			MidiDevice id = null;
			if (ods.Length > 1)
			{
				Console.WriteLine("Select MIDI output device to test:");
				foreach (MidiDevice _od in ods)
				{
					int i = Array.IndexOf<MidiDevice>(ods, _od);
					Console.WriteLine("\t[" + i.ToString().PadLeft(2, ' ') + "]\t" + _od.Name);
				}

				int index = 0;
				while (true)
				{
					Console.WriteLine();
					Console.Write("Your selection [0]: ");
					string str = Console.ReadLine();
					if (!String.IsNullOrEmpty(str))
					{
						if (!Int32.TryParse(str, out index))
						{
							Console.WriteLine("Invalid selection.  Please enter a valid index.");
							index = 0;
							continue;
						}
						if (index < 0 || index >= ods.Length)
						{
							Console.WriteLine("Invalid selection.  Please enter a valid index.");
							index = 0;
							continue;
						}
						else
						{
							break;
						}
					}
					else
					{
						break;
					}
				}
				id = ods[index];
			}
			else
			{
				id = ods[0];
			}

			if (id == null)
			{
				Console.WriteLine("Invalid MIDI input device!");
				Pause();
				return;
			}
			Console.WriteLine();


			Console.WriteLine("Opening device " + id.ID.ToString() + " (\"" + id.Name + "\") on soundcard " + id.Parent.ID.ToString());
			id.Open();

			Console.WriteLine("Reading from the device");
			id.MessageReceived += id_MessageReceived;
			id.Start();

			Console.ReadKey(true);

			Console.WriteLine("Flushing and closing device");
			id.Close();

			Console.WriteLine("Closing sound card");
			card.Close();
		}

		public static void Pause()
		{
			Console.Write("Press any key to continue . . .");
			Console.ReadKey(true);
			Console.WriteLine();
		}

		public static void ListAllSoundCards()
		{
			SoundCard[] cards = SoundCard.GetAllSoundCards();
			foreach (SoundCard card in cards)
			{
				Console.WriteLine("Soundcard found: " + card.ID.ToString());

				card.Open();
				MidiDevice[] devices = card.GetMidiDevices(MidiDeviceFunctionality.Input | MidiDeviceFunctionality.Output);
				foreach (MidiDevice device in devices)
				{
					Console.WriteLine("--- Device found: " + device.ID.ToString());
				}
				card.Close();
			}
		}
	}
}
