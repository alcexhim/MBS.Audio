using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace MBS.Audio.Internal.PortAudio
{
    public static class Delegates
    {
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate Constants.PaStreamCallbackResult PaStreamCallbackDelegate(IntPtr input, IntPtr output, uint frameCount, ref Structures.PaStreamCallbackTimeInfo timeInfo, Constants.PaStreamCallbackFlags statusFlags, IntPtr userData);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        public delegate void PaStreamFinishedCallbackDelegate(IntPtr userData);
    }
}
