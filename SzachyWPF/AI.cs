using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SzachyWPF
{
    class AI
    {
        //pola   
        RuchAI ruch0 = new RuchAI(0, 0, 0, 0, 0);

        private RuchAI zwrocNajlepszyRuchZPierwszejPlanszy(Plansza plansza)
        {
            List<RuchAI> ruchy = plansza.ZwrocWszystkieMozliweRuchy(Gracz.CZARNE);
            int max = -1000;           
            RuchAI najlepszyRuch = ruch0;
            RuchAI drugi;
            //int aktualna;           
            foreach (var ruch in ruchy)
            {
                plansza.wykonajRuch(ruch.x1, ruch.y1, ruch.x2, ruch.y2);
                drugi = zwrocNajlepszyRuchZKolejnej(plansza);

                int roznica = ruch.CompareTo(drugi); 
                if (roznica > max)
                {
                    najlepszyRuch = ruch;
                    max = roznica;
                }
                plansza.CofnijRuch();
            }
            Console.Read();
            return najlepszyRuch;
        }
        public RuchAI ZwrocNajlepszyRuch(Plansza plansza)
        {
           return zwrocNajlepszyRuchZPierwszejPlanszy(plansza);
        }
        private RuchAI zwrocNajlepszyRuchZKolejnej(Plansza plansza)
        {
            List<RuchAI> ruchy = plansza.ZwrocWszystkieMozliweRuchy(Gracz.BIALE);
            return ruchy.Max();
        }
        
    }
}
