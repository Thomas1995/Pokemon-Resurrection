using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Windows.Forms;

namespace Pokemon
{
    class Paint_BlackScreen
    {
        Graphics canvas;
        int W, H, R = 400;
        Timer timer = new Timer();

        FormGame parent;

        public Paint_BlackScreen(FormGame form, Graphics _canvas, int _W, int _H)
        {
            parent = form;
            canvas = _canvas;
            W = _W;
            H = _H;

            parent.pbMenuMaximize.Hide();

            timer.Interval = FormGame.refreshRate;
            timer.Tick += timer_Tick;

            timer.Start();
        }

        void timer_Tick(object sender, EventArgs e)
        {
            R -= 10;

            if(R < 0)
            {
                parent.pbMenuMaximize.Show();
                timer.Stop();
                timer.Dispose();
                return;
            }

            Paint();
        }

        private void Paint()
        {
            Rectangle[] blackRectangles = new Rectangle[4];
            blackRectangles[0] = new Rectangle(0, 0, W, R*3/4);
            blackRectangles[1] = new Rectangle(0, H - R*3/4, W, H);
            blackRectangles[2] = new Rectangle(0, 0, R, H);
            blackRectangles[3] = new Rectangle(W - R, 0, W, H);

            canvas.FillRectangles(Brushes.Black, blackRectangles);
        }
    }
}
