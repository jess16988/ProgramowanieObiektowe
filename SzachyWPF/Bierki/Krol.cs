using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SzachyWPF
{
    public class Krol : Pole
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="gracz"></param>
        public Krol(Gracz gracz) : base(gracz,900)
        {
            this.symbol = "K";
        }
        public Krol()
        {

        }
        //pola
        public override bool SprawdzRuchNaPustejPlanszy(int x1, int y1, int x2, int y2)
        {
            int x = x1 - x2;
            if (x < 0) x = -x;
            int y = y1 - y2;
            if (y < 0) y = -y;
            if (x < 2 && y < 2) return true;
            else return false;
        }
        public override bool SprawdzRuchDoBicia(int x1, int y1, int x2, int y2)
        {
            return this.SprawdzRuchNaPustejPlanszy(x1, y1, x2, y2);
        }
        //pola
        public bool CzyWykonalPierwszyRuch = false;
    }
}
