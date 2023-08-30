using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MBS.Audio.MIDI
{
    [System.Diagnostics.DebuggerNonUserCode()]
    public class Message
    {
        private MessageType mvarMessageType = MessageType.ControlChange;
        public MessageType MessageType { get { return mvarMessageType; } set { mvarMessageType = value; } }

        private byte mvarChannel = 0;
        public byte Channel
        {
            get { return mvarChannel; }
            set
            {
                if (value < 1 || value > 16)
                {
                    throw new ArgumentOutOfRangeException("Channel", "value for channel must be between 1 and 16, inclusive");
                }
                mvarChannel = value;
            }
        }

        private byte mvarParameter1 = 0;
        public byte Parameter1
        {
            get { return mvarParameter1; }
            set
            {
                if (value < 0 || value > 127)
                {
                    throw new ArgumentOutOfRangeException("Parameter1", "value for parameter 1 must be between 0 and 127, inclusive");
                }
                mvarParameter1 = value;
            }
        }

        private byte mvarParameter2 = 0;
        public byte Parameter2
        {
            get { return mvarParameter2; }
            set
            {
                if (value < 0 || value > 127)
                {
                    throw new ArgumentOutOfRangeException("Parameter2", "value for parameter 2 must be between 0 and 127, inclusive");
                }
                mvarParameter2 = value;
            }
        }

        public Message(MessageType messageType, byte channel, byte parameter1, byte parameter2)
        {
            mvarMessageType = messageType;
            Channel = channel;
            Parameter1 = parameter1;
            Parameter2 = parameter2;
        }

    }
}
