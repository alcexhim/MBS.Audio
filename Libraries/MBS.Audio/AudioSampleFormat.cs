using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MBS.Audio
{
    public enum AudioSampleFormat : uint
    {
        None = 0,
        Float32 = 1u,
        Int32 = 2u,
        Int24 = 4u,
        Int16 = 8u,
        Int8 = 16u,
        UInt8 = 32u,
        CustomFormat = 65536u,
        NonInterleaved = 2147483648u
    }
}
