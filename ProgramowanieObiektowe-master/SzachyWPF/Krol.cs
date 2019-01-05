using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SzachyWPF
{
    class Krol : Pole
    {
        public Krol(Gracz gracz, int x, int y) : base(gracz,900)
        {
            this.symbol = "K";
            this.x = x;
            this.y = y;
        }
        //pola
        int x;
        int y;
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
        public void ZmienLokalizacje(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public int[] ZwrocLokaliacje()
        {
            int[] tablica = { this.x, this.y };
            return tablica;
        }
    }
}
