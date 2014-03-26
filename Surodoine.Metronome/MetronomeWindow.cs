using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace Surodoine.Metronome
{
    public partial class MetronomeWindow : Form
    {
        public MetronomeWindow()
        {
            InitializeComponent();

            tickPanel = new Panel[] { panel1, panel2, panel3, panel4 };
            metronome.Tick += metronome_Tick;
        }

        private void metronome_Tick(object sender, EventArgs e)
        {
            Invoke(new Action(Tick));
        }

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            metronome.Stop();
        }

        private int tickTime = 0;
        private Panel[] tickPanel = null;

        private void Tick()
        {
            if (tickTime >= tickPanel.Length)
            {
                tickTime = 0;
            }

            foreach (Panel panel in tickPanel)
            {
                panel.BackColor = Color.Black;
            }
            tickPanel[tickTime].BackColor = Color.Red;
            tickTime++;
        }

        private Metronome metronome = new Metronome(String.Join(System.IO.Path.DirectorySeparatorChar.ToString(), new string[]
        {
            "Sounds",
            "Metronome"
        }), 120);

        private void cmdStart_Click(object sender, EventArgs e)
        {
            if (metronome.IsPlaying)
            {
                metronome.Stop();
                cmdStart.Text = "&Start";
                tickTime = 0;
                foreach (Panel panel in tickPanel)
                {
                    panel.BackColor = Color.Black;
                }
            }
            else
            {
                metronome.Tempo = (double)(txtTempo.Value);
                metronome.Start();
                cmdStart.Text = "&Stop";
            }
        }

    }
}
