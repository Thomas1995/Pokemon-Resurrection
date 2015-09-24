using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Pokemon
{
    class Structures
    {
        public int X, Y;
        public int sizeX, sizeY;
        public int type, subtype, subtype2;
        public Image image;

        public Structures(int _type, int _subtype, int x, int y)
        {
            X = x;
            Y = y;
            type = _type;
            subtype = _subtype;

            if(type == 1) // Fence
            {
                image = Resources.fence[subtype];
                sizeY = 40;

                if (subtype == 0) sizeX = 80;
                else sizeX = 15;
            }

            if(type == 2) // Lantern
            {
                image = Resources.lantern;
                sizeX = 40;
                sizeY = 80;
            }

            if(type == 3) // Stone
            {
                image = Resources.stone;
                sizeX = 60;
                sizeY = 60;
            }
        }

        public Structures(int _type, int _subtype, int _subtype2, int x, int y)
        {
            X = x;
            Y = y;
            type = _type;
            subtype = _subtype;
            subtype2 = _subtype2;

            if (type == 1) // Water
            {
                image = Resources.water[subtype - 1, subtype2 - 1];
                sizeX = 40;
                sizeY = 40;
            }
        }
    }
}
