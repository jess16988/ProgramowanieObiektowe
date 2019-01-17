using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SzachyWPF
{
    public class Goniec : Pole
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="gracz"></param>
        public Goniec(Gracz gracz) : base(gracz,30)
        {
            this.symbol = "L";
        }

        public override bool SprawdzRuchNaPustejPlanszy(int x1, int y1, int x2, int y2)
        {
            int x = x1 - x2;
            int y = y1 - y2;
            if (x < 0) x = -x;
            if (y < 0) y = -y;
            if (x == y) return true;
            else return false;
        }
        public override bool SprawdzRuchDoBicia(int x1, int y1, int x2, int y2)
        {
            return this.SprawdzRuchNaPustejPlanszy(x1, y1, x2, y2);
        }
    }
}
