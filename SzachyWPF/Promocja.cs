using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SzachyWPF
{
    /// <summary>
    /// Gdy pionek dojdzie do swojej liny przemiany gracz wybiera w jak¹ figurê chce go przemienic
    /// </summary>
    public class Promocja
    {

        //pola
        public bool czyPromocja = false;
        public int x;
        public int y;

        //metody        
        /// <summary>
        /// Sprawdza czy pionek jest na swojej lini przemiany (dla bia³ych y=0, dla czarnych y=7)
        /// </summary>
        /// <param name="x">The x.</param>
        /// <param name="y">The y.</param>
        /// <param name="bierka">The bierka.</param>
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
