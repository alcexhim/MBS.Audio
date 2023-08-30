using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MBS.Audio.MIDI
{
	public delegate void MessageReceivedEventHandler(object sender, MessageReceivedEventArgs e);
	public class MessageReceivedEventArgs : EventArgs
    {

        private Message mvarMessage = null;
        public Message Message { get { return mvarMessage; } }

		private IntPtr mvarInstanceData = IntPtr.Zero;
		public IntPtr InstanceData { get { return mvarInstanceData; } }

		public MessageReceivedEventArgs(Message message, IntPtr instanceData)
		{
			mvarMessage = message;
			mvarInstanceData = instanceData;
		}
	}
}
