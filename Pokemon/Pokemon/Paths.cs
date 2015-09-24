using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;

namespace Pokemon
{
    class Paths
    {
        public int X, Y, type;
        public Image image;

        public Paths(int _type, int x, int y)
        {
            X = x;
            Y = y;
            type = _type;
            image = Resources.path[type - 1];
        }
    }
}
