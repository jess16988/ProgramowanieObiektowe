using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SzachyWPF
{
   public class Pole
    {
        public Pole(Gracz? gracz)
        {
            this.gracz = gracz;
        }
        //pola
        protected string symbol = " ";
        protected Gracz? gracz;

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
    }
}
