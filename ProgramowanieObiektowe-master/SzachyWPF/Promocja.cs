using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SzachyWPF
{
    public class Promocja
    {
        //pola
        public bool czyPromocja = false;
        public int x;
        public int y;

        //metody
        public void Sprawdz(int x, int y, Pole bierka)
        {
            czyPromocja = false;
            if (y == 0 && bierka.ZwrocGracza() == Gracz.BIALE && bierka is Pionek)
            {
                this.czyPromocja = true;
            }
            else if (y == 7 && bierka.ZwrocGracza() == Gracz.CZARNE && bierka is Pionek) czyPromocja = true;
            this.x = x;
            this.y = y;
        }
    }
}
