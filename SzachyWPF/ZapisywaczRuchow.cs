using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SzachyWPF
{
    class ZapisywaczRuchow
    {
        private Stack<Ruch> stosRuchow = new Stack<Ruch>();
        private List<Ruch> ListaRuchow = new List<Ruch>();

        //metody
        public void DodajPozycje(int x1, int y1, Pole pole1, int x2, int y2, Pole pole2)
        {
            this.stosRuchow.Push(new Ruch(x1, y1, pole1, x2, y2, pole2));
            this.ListaRuchow.Add(new Ruch(x1, y1, pole1, x2, y2, pole2));
        }
        public Ruch WyciagnijPozycje()
        {   
            if (stosRuchow.Count == 0)
            {
                //Console.WriteLine("Nie wykonano zadnego ruchu");
                //Console.ReadKey();
                return null;
            }
            else
            {
                return stosRuchow.Pop();
            }
        }
        public Ruch ZwrocjPozycje(int pozycja)
        {
            return this.ListaRuchow[pozycja];
        }
        
    }
}
