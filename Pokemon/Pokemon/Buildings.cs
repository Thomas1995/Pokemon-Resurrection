using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Pokemon
{
    class Buildings
    {
        public Image image;
        public int type;
        public int X, Y;
        public int sizeX, sizeY;
        public bool hasInside;

        public Buildings(int t, int x, int y, bool inside)
        {
            type = t;
            X = x;
            Y = y;
            hasInside = inside;

            sizeX = 180;
            sizeY = 135;
            
            if(type == -1)
            {
                image = Resources.cave;
            }
            if(type == 0)
            {
                image = Resources.gym;
            }
            if(type == 1)
            {
                image = Resources.healingcenter;
            }
            if(type == 2)
            {
                image = Resources.shop;
            }
            if(type >= 3)
            {
                image = Resources.house[type - 3];
            }
        }
    }
}
