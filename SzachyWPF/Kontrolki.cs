using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SzachyWPF
{
    class Kontrolki
    {
        public Kontrolki(Plansza plansza)
        {
            this.plansza = plansza;
            this.pola = plansza.Zwroc();
        }
        //pola
        public int x1Krola1 = 0;
        public int y1Krola1 = 0;
        public int x2Krola2 = -1;
        public int y2Krola2 = -1;
        private Gracz? gracz = null;
        public bool czySzach = false;
        public bool czyMat = false;
        public bool czyPat = false;
        public bool czyKoniec = false;
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
            czySzach = sprawdzCzySzach();
            //czyPat = sprawdzCzyPat();
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
                        if (sprawdzCzyMat(i, j, x2Krola2, y2Krola2) == true) czyMat = true;
                        else czyMat = false;                       
                        return true;
                    }                  
                }                
            }
            return false;
        }
        private bool sprawdzCzyMat(int xAtakujacego, int yAtakujacego, int xKrola, int yKrola)
        {
            //sprawdzam czy nie da sie zbic atakujacego
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    if (plansza.SprawdzCalyRuch(i, j, xAtakujacego, yAtakujacego, pola[i, j].ZwrocGracza()) == true)
                    {
                        return false;
                    }
                }
            }
            //sprawdzam czy nie da sie uciec krolem
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (plansza.SprawdzCalyRuch(xKrola, yKrola, xKrola + i - 1, yKrola + j - 1, pola[xKrola, yKrola].ZwrocGracza()) == true)
                    {
                        return false;
                    }
                }
                
            }
            //sprawdzam czy nie da sie zagrodzic atakujacego
            if (pola[yAtakujacego, yAtakujacego] is Goniec || pola[yAtakujacego, yAtakujacego] is Wieza || pola[yAtakujacego, yAtakujacego] is Hetman)
            {
                int[,] tablica = Prosta.ZwrocPunktyKolizji(xAtakujacego, yAtakujacego, xKrola, yKrola);
                int x = 0;
                while (tablica[0, x] != 100)
                {
                    for (int i = 0; i < 8; i++)
                    {
                        for (int j = 0; j < 8; j++)
                        {
                            if (plansza.SprawdzCalyRuch(i, j, tablica[0, x], tablica[1, x], pola[i, j].ZwrocGracza()) == true)
                            {
                                return false;
                            }
                        }
                    }
                    x++;
                }
            }
            return true;
        }        
    }
}
