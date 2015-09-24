using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pokemon
{
    public partial class FormEvolution : Form
    {
        int start, finish, crt;

        public FormEvolution(int i, int j)
        {
            InitializeComponent();

            start = i;
            finish = j;

            crt = start;

            timerEvolution.Interval = 50;
            timerEvolution.Start();
        }

        private void pbCanvas_Paint(object sender, PaintEventArgs e)
        {
            Graphics canvas = e.Graphics;

            canvas.DrawImage(Resources.pokeFront[crt], 110, 110);
        }

        int timerEvolutionCount;

        private void timerEvolution_Tick(object sender, EventArgs e)
        {
            pbCanvas.Invalidate();

            if(++timerEvolutionCount >= 100)
            {
                crt = finish;
                if (timerEvolutionCount == 140)
                {
                    timerEvolution.Stop();
                    this.Close();
                }
                return;
            }

            if (++timerEvolutionCount <= 20) return;

            if (crt == start) crt = finish;
            else crt = start;
        }
    }
}
