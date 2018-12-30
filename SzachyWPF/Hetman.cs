using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SzachyWPF
{
    class Hetman : Pole
    {
        public Hetman(string gracz) : base(gracz)
        {
            this.symbol = "Q";
        }

        public override bool SprawdzRuchNaPustejPlanszy(int x1, int y1, int x2, int y2)
        {
            int x = x1 - x2;
            int y = y1 - y2;
            if (x < 0) x = -x;
            if (y < 0) y = -y;
            if (x == y) return true;
            else if ((x1 - x2) == 0 || (y1 - y2) == 0)
            {
                return true;
            }
            else return false;
        }
        public override bool SprawdzRuchDoBicia(int x1, int y1, int x2, int y2)
        {
            return this.SprawdzRuchNaPustejPlanszy(x1, y1, x2, y2);
        }
    }
}
