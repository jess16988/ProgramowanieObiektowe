using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SzachyWPF
{
    public class Kontrolki
    {
        public Kontrolki(Plansza plansza)
        {
            this.plansza = plansza;
            this.pola = plansza.Zwroc();
        }
        //pola
        public int x1Krola1 = 4;
        public int y1Krola1 = 7;
        public int x2Krola2 = 4;
        public int y2Krola2 = 0;
        private Gracz? gracz = null;
        public bool czySzach = false;
        public bool czyMat = false;
        public bool czyPat = false;
        private Plansza plansza;
        private Pole[,] pola;

        //metody
        public void znajdzKroli(Gracz? gracz)
        {
            this.gracz = gracz;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (pola[i, j] is Krol)
                    {
                        if (pola[i, j].ZwrocGracza() == gracz)
                        {
                            x1Krola1 = i;
                            y1Krola1 = j;
                        }
                        else
                        {
                            x2Krola2 = i;
                            y2Krola2 = j;
                        }
                    }
                }
            }
            //kontrolki.PrzekazPozycjeKroli(x1Krola1, y1Krola1, x2Krola2, y2Krola2);
        }
        public void Sprawdz()
        {
            czySzach = false;
            czyMat = false;
            czyPat = false;

            czySzach = sprawdzCzySzach();
            czyPat = sprawdzCzyPat();
        }

        private bool sprawdzCzySzach() //gracz ktory ostatnio sie poruszyl
        {

            bool szach = false;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    szach = plansza.SprawdzCalyRuch(i, j, x2Krola2, y2Krola2, pola[x1Krola1, y1Krola1].ZwrocGracza());
                    if (szach == true)
                    {
                        czyMat = sprawdzCzyMat(i, j, x2Krola2, y2Krola2);                    
                        return true;
                    }                  
                }                
            }
            return false;
        }
        private bool sprawdzCzyMat(int xAtakujacego, int yAtakujacego, int xKrola, int yKrola)
        {
            if (plansza.ZwrocWszystkieMozliweRuchy(pola[xKrola, yKrola].ZwrocGracza()).Count == 0) return true;
            else return false;
        }
        private bool sprawdzCzyPat()//gracz ktory ostatnio sie poruszyl
        {
            Gracz gracz2;
            if (gracz == Gracz.BIALE) gracz2 = Gracz.CZARNE;
            else gracz2 = Gracz.BIALE;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    for (int k = 0; k < 8; k++)
                    {
                        for (int l = 0; l < 8; l++)
                        {
                            if (plansza.SprawdzCalyRuch(i, j, k, l, gracz2) == true)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }
    }
}
