using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Pokemon
{
    class WildGrass
    {
        public Image image;
        public int X, Y;
        public bool isVisited = false;

        public WildGrass(int x, int y)
        {
            X = x;
            Y = y;
            image = new Bitmap(Resources.biggrass, new Size(40,40));
        }
    }
}
