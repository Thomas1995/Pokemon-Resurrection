using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon
{
    class BlockedZone
    {
        public int Xstart, Ystart, Xfin, Yfin;

        public BlockedZone(int x1, int y1, int x2, int y2)
        {
            Xstart = x1;
            Ystart = y1;
            Xfin = x2;
            Yfin = y2;
        }
    }
}
