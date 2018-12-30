using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SzachyWPF
{
    class Skoczek : Pole
    {
        public Skoczek(Gracz gracz) : base(gracz)
        {
            this.symbol = "G";
        }

        public override bool SprawdzRuchNaPustejPlanszy(int x1, int y1, int x2, int y2)
        {
            int x = x1 - x2;
            if (x < 0) x = -x;
            int y = y1 - y2;
            if (y < 0) y = -y;
            if(x == 1 && y == 2 || x == 2 && y == 1)
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
