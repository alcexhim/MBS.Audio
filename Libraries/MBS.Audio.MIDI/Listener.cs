using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MBS.Audio.MIDI
{
    public class Listener
    {
		public MidiDevice InputDevice { get; private set; } = null;

        public SoundCard SoundCard { get; set; } = null;

		private static MessageReceivedEventHandler eventHandler = null;

        public void Start()
        {
			if (SoundCard == null)
			{
				SoundCard = SoundCard.GetDefaultSoundCard();
			}
			if (SoundCard != null)
			{
				SoundCard.Open();
				if (InputDevice == null)
				{
					InputDevice = SoundCard.GetDefaultMidiInputDevice();
				}
				// output = sc.GetMidiOutputDevices()[1];
			}

			if (InputDevice != null)
			{
				InputDevice.Open();
				InputDevice.MessageReceived += Listener_MessageReceived;
				InputDevice.Start();
			}
        }
        public void Stop()
        {
            if (InputDevice != null)
            {
				InputDevice.Stop();
				InputDevice.Close();
            }
			SoundCard.Close();
        }

        public event MessageReceivedEventHandler MessageReceived;
		protected virtual void OnMessageReceived(MessageReceivedEventArgs e)
		{
			MessageReceived?.Invoke(this, e);
		}

		private void Listener_MessageReceived(object sender, MBS.Audio.MIDI.MessageReceivedEventArgs e)
        {
			OnMessageReceived(e);
        }
    }
}
