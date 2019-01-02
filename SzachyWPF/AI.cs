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
        long licznikruchow= 0;

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
                
                //aktualna = -plansza.OcenWartoscPlanszy();
                if (ruch.wartosc - drugi.wartosc > max)
                {
                    max = ruch.wartosc - drugi.wartosc;
                    najlepszyRuch = ruch;
                    //drugi = new RuchAI(0, 0, 0, 0, -1000);
                }
                plansza.CofnijRuch();
            }
            Console.Read();
            licznikruchow = 0;
            return najlepszyRuch;
        }
        public RuchAI ZwrocNajlepszyRuch(Plansza plansza)
        {
           return zwrocNajlepszyRuchZPierwszejPlanszy(plansza);
        }
        private RuchAI zwrocNajlepszyRuchZKolejnej(Plansza plansza)
        {
            List<RuchAI> ruchy = plansza.ZwrocWszystkieMozliweRuchy(Gracz.BIALE);
            int max = -1000;
            RuchAI najlepszyRuch = ruch0;
            //int aktualna;
            foreach (var ruch in ruchy)
            {              
                licznikruchow++;
                if (ruch.wartosc > max)
                {
                    max = ruch.wartosc;
                    najlepszyRuch = ruch;
                }                
            }
            return najlepszyRuch;
        }
        
    }
}
