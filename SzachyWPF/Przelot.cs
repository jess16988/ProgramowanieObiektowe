using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SzachyWPF
{
    class Przelot : Pole
    {
        public Przelot(Gracz gracz,int x, int y, int tura,int XPionka, int YPionka)
        {
            this.gracz = gracz;
            this.x = x;
            this.y = y;
            this.tura = tura;
            this.XPionka = XPionka;
            this.YPionka = YPionka;
        }

        //pola
        public int XPionka;
        public int YPionka;
        public int x;
        public int y;
        public int tura;
    }
}
