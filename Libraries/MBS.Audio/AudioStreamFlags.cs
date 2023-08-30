using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MBS.Audio
{
    [Flags()]
    public enum AudioStreamFlags : uint
    {
        None,
        ClipOff,
        DitherOff,
        NeverDropInput,
        PrimeOutputBuffersUsingStreamCallback,
        PlatformSpecificFlags
    }
}
