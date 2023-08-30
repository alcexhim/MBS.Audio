using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using MBS.Audio.PortAudio;
using UniversalEditor.ObjectModels.Multimedia.Audio.Waveform;

namespace MBS.Audio.Metronome
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
			AudioEngine ae = new PortAudioEngine();
			ae.Initialize();

			Application.Run(new MetronomeWindow());
        }
    }
}
