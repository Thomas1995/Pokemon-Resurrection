using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon
{
    public class ActionPoint
    {
        public int Xstart, Ystart, Xfin, Yfin;
        public int action;

        public ActionPoint(int x1, int y1, int x2, int y2, int actionNr)
        {
            Xstart = x1;
            Ystart = y1;
            Xfin = x2;
            Yfin = y2;

            action = actionNr;
        }
    }
}
