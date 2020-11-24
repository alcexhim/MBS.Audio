using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MBS.Audio
{
    public delegate void AudioPlayerStateChangedEventHandler(object sender, AudioPlayerStateChangedEventArgs e);
    public class AudioPlayerStateChangedEventArgs : EventArgs
    {
		public AudioPlayerState State { get; private set; } = AudioPlayerState.Stopped;
		public AudioPlayerStateChangedReason Reason { get; private set; } = AudioPlayerStateChangedReason.Unknown;

		public AudioPlayerStateChangedEventArgs(AudioPlayerState state, AudioPlayerStateChangedReason reason)
        {
            State = state;
			Reason = reason;
        }
    }
}
