using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Pokemon
{
    class Trees
    {
        public Image image;
        public int X, Y;
        public int sizeX, sizeY;
        public int type;

        public Trees(int _type, int x, int y)
        {
            X = x;
            Y = y;
            type = _type;

            if (type == 1)
            {
                sizeX = 80;
                sizeY = 90;
                image = Resources.tree;
            }
            if(type == 2)
            {
                sizeX = 48;
                sizeY = 62;
                image = Resources.smalltree;
            }
            if(type == 3)
            {
                sizeX = 80;
                sizeY = 90;
                image = Resources.treesnow;
            }
        }
    }
}
