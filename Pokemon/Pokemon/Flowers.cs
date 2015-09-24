using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Pokemon
{
    class Flowers
    {
        public Image image;
        private int imageNr;
        public int X, Y;
        public int sizeX, sizeY;

        public Flowers(int x, int y)
        {
            X = x;
            Y = y;

            sizeX = 40;
            sizeY = 40;

            imageNr = 0;

            ChangeSprite();
        }

        public void ChangeSprite()
        {
            imageNr = 1 - imageNr;
            image = Resources.flower[imageNr];
        }
    }
}
