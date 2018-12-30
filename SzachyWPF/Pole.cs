using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SzachyWPF
{
   public class Pole
    {
        public Pole(string gracz)
        {
            this.gracz = gracz;
        }
        //pola
        protected string symbol = " ";
        protected string gracz;

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
        public string ZwrocGracza()
        {
            return gracz;
        }
    }
}
