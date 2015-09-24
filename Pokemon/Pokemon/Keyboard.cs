using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pokemon
{
    class Keyboard
    {
        static List<int> stackArrows = new List<int>();
        // 0 none 1 up 2 down 3 right 4 left

        static public int GetDir()
        {
            if (stackArrows.Count == 0) return 0;
            return stackArrows[stackArrows.Count - 1];
        }

        static public void ChangeDir(int x, bool add)
        {
            if (add)
            {
                if(!stackArrows.Contains(x))
                    stackArrows.Add(x);
            }
            else stackArrows.Remove(x);
        }

        static public void ClearStack()
        {
            for(int i=1;i<=4;++i)
                if(stackArrows.Contains(i))
                    stackArrows.Remove(i);
        }
    }
}
