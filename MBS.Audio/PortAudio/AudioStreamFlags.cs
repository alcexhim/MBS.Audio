using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MBS.Audio.PortAudio
{
    [Flags()]
    public enum AudioStreamFlags : uint
    {
        None,
        ClipOff,
        DitherOff,
        NeverDropInput = 4u,
        PrimeOutputBuffersUsingStreamCallback = 8u,
        PlatformSpecificFlags = 4294901760u
    }
}
