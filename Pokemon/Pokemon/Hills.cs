using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Pokemon
{
    class Hills
    {
        public Image image;
        public int X, Y;
        public int sizeX, sizeY;

        public Hills(int x, int y, int length)
        {
            X = x;
            Y = y;

            sizeX = length;
            sizeY = 20;

            image = new Bitmap(Resources.hill, new Size(sizeX, sizeY));
        }
    }
}
