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
            List<RuchAI> czarneRuchy = plansza.ZwrocWszystkieMozliweRuchy(Gracz.CZARNE);
            int min = 1000;           
            RuchAI najlepszyCzarnyRuch = ruch0;
                 
            foreach (var czarnyRuch in czarneRuchy)
            {
                plansza.WykonajRuch(czarnyRuch.x1, czarnyRuch.y1, czarnyRuch.x2, czarnyRuch.y2);
                List<RuchAI> bialeRuchy = plansza.ZwrocWszystkieMozliweRuchy(Gracz.BIALE);
                RuchAI najlepszyBialyRuch = bialeRuchy.Max();

                if (najlepszyBialyRuch.wartosc < min)
                {
                    najlepszyCzarnyRuch = czarnyRuch;
                    min = najlepszyBialyRuch.wartosc;
                }
                plansza.CofnijRuch();
            }
            Console.Read();
            return najlepszyCzarnyRuch;
        }
        public RuchAI ZwrocNajlepszyRuch(Plansza plansza)
        {
           return zwrocNajlepszyRuchZPierwszejPlanszy(plansza);
        }
        
    }
}
