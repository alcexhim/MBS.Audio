using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using UniversalEditor.ObjectModels.Multimedia.Audio.Waveform;

namespace Surodoine.Metronome
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            AudioEngine.Initialize();
            Application.Run(new MetronomeWindow());
        }
    }
}
