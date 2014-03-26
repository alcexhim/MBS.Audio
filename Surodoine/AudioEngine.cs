using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Surodoine
{
    public static class AudioEngine
    {
        public static void Initialize()
        {
            Internal.PortAudio.Constants.PaError result = Internal.PortAudio.Methods.Pa_Initialize();
            Internal.PortAudio.Methods.Pa_ResultToException(result);
        }
    }
}
