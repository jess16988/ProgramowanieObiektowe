using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SzachyWPF
{
   public abstract class Pole
    {
        protected Pole(Gracz? gracz, int wartosc)
        {
            this.gracz = gracz;
            this.wartosc = wartosc;
        }
        //pola
        protected string symbol = " ";
        protected Gracz? gracz;
        private int wartosc;

        //metody
        public virtual bool SprawdzRuchNaPustejPlanszy(int x1, int y1, int x2, int y2)
        {
            Console.WriteLine("Pole");
            //Console.ReadKey();
            return false;
        }
        public virtual bool SprawdzRuchDoBicia(int x1, int y1, int x2, int y2)
        {
            return false;
        }
        public string ZwrocSymbol()
        {
            return symbol;
        }
        public Gracz? ZwrocGracza()
        {
            return gracz;
        }

        public int podajWartosc(Gracz aktywnyGracz)
        {
            return kontrolna(aktywnyGracz) * this.wartosc;
        }

        protected int kontrolna(Gracz aktywnyGracz)
        {
            return this.gracz == aktywnyGracz ? 1 : -1;
        }
    }
}
