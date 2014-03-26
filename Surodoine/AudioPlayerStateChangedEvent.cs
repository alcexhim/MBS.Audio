using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Surodoine
{
    public delegate void AudioPlayerStateChangedEventHandler(object sender, AudioPlayerStateChangedEventArgs e);
    public class AudioPlayerStateChangedEventArgs : EventArgs
    {
        private AudioPlayerState mvarState = AudioPlayerState.Stopped;
        public AudioPlayerState State { get { return mvarState; } }


        public AudioPlayerStateChangedEventArgs(AudioPlayerState state)
        {
            mvarState = state;
        }
    }
}
