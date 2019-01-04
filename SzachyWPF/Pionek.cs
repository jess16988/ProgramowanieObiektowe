using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SzachyWPF
{
    class Pionek : Pole
    {
        public Pionek(Gracz gracz) : base(gracz, 10)
        {
            this.symbol = "P";
        }
        private bool czyWykonalPierwszyRuch = false;

        public override bool SprawdzRuchNaPustejPlanszy(int x1, int y1, int x2, int y2)
        {
            int mnoznik = 1;
            if (gracz == Gracz.CZARNE) mnoznik = -1;
            if((y2 -y1) == (-2 * mnoznik) && x1 - x2== 0 && czyWykonalPierwszyRuch == false)
                {
                    return true;
                }
            else if((y2 - y1) == (-1 * mnoznik) && x1 - x2 == 0)
                {
                    return true;
                }
            return false;
        }
        public override bool SprawdzRuchDoBicia(int x1, int y1, int x2, int y2)
        {
            int x = x2 - x1;
            int y = y2 - y1;
            if (this.gracz == Gracz.BIALE)
            {
                if (y2 - y1 == -1 && (x2 - x1 == 1 || x2 - x1 == -1))
                {
                    return true;
                }
            }
            else if(this.gracz == Gracz.CZARNE)
            {
                if (y2 - y1 == 1 && (x2 - x1 == 1 || x2 - x1 == -1))
                {
                    return true;
                }
            }
            return false;
        }
        public void cofnijPierwszyRuch()
        {
            czyWykonalPierwszyRuch = false;
        }
        public void WykonalPierwszyRuch()
        {
            czyWykonalPierwszyRuch = true;
        }
    }
}
