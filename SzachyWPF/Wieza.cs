using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SzachyWPF
{
    class Wieza : Pole
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="gracz"></param>
        public Wieza(Gracz gracz) : base(gracz,50)
        {
            this.symbol = "W";
        }

        public override bool SprawdzRuchNaPustejPlanszy(int x1, int y1, int x2, int y2)
        {
            if ((x1 - x2) == 0 || (y1 - y2) == 0)
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
