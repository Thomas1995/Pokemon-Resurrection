using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon
{
    class ExitPoint
    {
        public int Xstart, Ystart, Xfin, Yfin;
        public int mapNr, Xmap, Ymap;

        public ExitPoint(int x1, int y1, int x2, int y2, int map, int x, int y)
        {
            Xstart = x1;
            Ystart = y1;
            Xfin = x2;
            Yfin = y2;
            mapNr = map;
            Xmap = x;
            Ymap = y;
        }
    }
}
