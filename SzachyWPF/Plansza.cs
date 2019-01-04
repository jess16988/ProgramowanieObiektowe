using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SzachyWPF
{
    class Plansza
    {
        
        public Plansza()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    this.plansza[i, j] = poleBezPionka;
                }
            }
            plansza[3, 0] = new Hetman(Gracz.CZARNE);
            plansza[4, 0] = new Krol(Gracz.CZARNE, 4, 0);
            plansza[5, 0] = new Goniec(Gracz.CZARNE);
            plansza[2, 0] = new Goniec(Gracz.CZARNE);
            plansza[6, 0] = new Skoczek(Gracz.CZARNE);
            plansza[1, 0] = new Skoczek(Gracz.CZARNE);
            plansza[0, 0] = new Wieza(Gracz.CZARNE);
            plansza[7, 0] = new Wieza(Gracz.CZARNE);
            plansza[3, 7] = new Hetman(Gracz.BIALE);
            plansza[4, 7] = new Krol(Gracz.BIALE, 3, 7);
            plansza[5, 7] = new Goniec(Gracz.BIALE);
            plansza[2, 7] = new Goniec(Gracz.BIALE);
            plansza[6, 7] = new Skoczek(Gracz.BIALE);
            plansza[1, 7] = new Skoczek(Gracz.BIALE);
            plansza[0, 7] = new Wieza(Gracz.BIALE);
            plansza[7, 7] = new Wieza(Gracz.BIALE);
            for (int i = 0; i < 8; i++)
            {
                plansza[i, 6] = new Pionek(Gracz.BIALE);
                plansza[i, 1] = new Pionek(Gracz.CZARNE);
            }
            kontrolki = new Kontrolki(this);
            ai = new AI();
        }
        //pola

        private AI ai;
        private Pole poleBezPionka = new PustePole();
        public Kontrolki kontrolki; 
        private Pole[,] plansza = new Pole[8, 8];
        private int licznikRuchow = 0;
        private ZapisywaczRuchow zapisywaczRuchow = new ZapisywaczRuchow();
        private Stack<Pole> pojemnikNaFigury1 = new Stack<Pole>();
        private Stack<Pole> pojemnikNaFigury2 = new Stack<Pole>();

        //metody
        public bool RuszGlowny(int x1, int y1, int x2, int y2, Gracz gracz)
        {
            if (SprawdzCalyRuch(x1, y1, x2, y2, gracz) == true)
            {
                wykonajRuch(x1, y1, x2, y2);
                RuchAI ruch = ai.ZwrocNajlepszyRuch(this);
                wykonajRuch(ruch.x1, ruch.y1, ruch.x2, ruch.y2);
                return true;
            }
            return false;
        }
        public bool SprawdzCalyRuch(int x1, int y1, int x2, int y2, Gracz? gracz)
        {
            if (sprawdzCalyRuchZaWyjatkiemKrola(x1, y1, x2, y2, gracz) == true)
            {
                 wykonajRuch(x1, y1, x2, y2);                
                 kontrolki.znajdzKroli(gracz);
                int x1Krola1 = kontrolki.x1Krola1;
                int y1Krola1 = kontrolki.y1Krola1;
                int x2Krola2 = kontrolki.x2Krola2;
                int y2Krola2 = kontrolki.y2Krola2;

                if (x2Krola2 == -1)
                {
                    // w przypadku zaznaczania cos bilo krola i stierdzalo ze ruch jest ok a krola nie bylo
                    CofnijRuch();
                    return true;
                }           
                if (czyRuchNieOdkrylKrola(x1Krola1, y1Krola1, plansza[x2Krola2, y2Krola2].ZwrocGracza()) == false)
                {
                    CofnijRuch();
                    return false;
                }
                else
                {
                    CofnijRuch();
                    return true;
                }
            }
            else return false;
        }
        
        private bool sprawdzCalyRuchZaWyjatkiemKrola(int x1, int y1, int x2, int y2, Gracz? gracz)
        {
            if (x1 < 0 || x1 > 7 || y1 < 0 || y1 > 7 || x2 < 0 || x2 > 7 || y2 < 0 || y2 > 7) return false;
            if (sprawdzKolizje(x1, y1, x2, y2) == true && sprawdzGracza(x1, y1, x2, y2, gracz) == true)
            {
                if (sprawdzBicie(x1, y1, x2, y2) == true)
                {
                    if (plansza[x1, y1].SprawdzRuchDoBicia(x1, y1, x2, y2) == true)
                    {
                        return true;
                    }
                }
                else
                {
                    if (plansza[x1, y1].SprawdzRuchNaPustejPlanszy(x1, y1, x2, y2) == true)
                    {
                        return true;
                    }
                }
            }
            return false;
        }
        private bool sprawdzBicie(int x1, int y1, int x2, int y2)
        {
            if ((plansza[x1, y1].ZwrocGracza() == Gracz.BIALE && plansza[x2, y2].ZwrocGracza() == Gracz.CZARNE) ||
                (plansza[x1, y1].ZwrocGracza() == Gracz.CZARNE && plansza[x2, y2].ZwrocGracza() == Gracz.BIALE)) return true;
            else return false;
        }
        private bool sprawdzKolizje(int x1, int y1, int x2, int y2)
        {
            if (plansza[x1, y1] is Skoczek || plansza[x1, y1] is Krol) return true;
            int[,] tablica = Prosta.ZwrocPunktyKolizji(x1, y1, x2, y2);
            int x = 0;
            while (tablica[0, x] != 100 && tablica[1, x] != 100)
            {
                if (plansza[tablica[0, x], tablica[1, x]].ZwrocSymbol() != " ") return false;
                x++;
            }
            return true;
        }
        private bool sprawdzGracza(int x1, int y1, int x2, int y2, Gracz? gracz)
        {
            if (plansza[x1, y1].ZwrocGracza() == plansza[x2, y2].ZwrocGracza() || plansza[x1, y1].ZwrocGracza() != gracz) return false;
            else return true;
        }

        private void zapiszRuch(int x1, int y1, int x2, int y2)
        {
            zapisywaczRuchow.DodajPozycje(x1, y1, plansza[x1, y1], x2, y2, plansza[x2, y2]);
        }
        public void CofnijRuch()
        {
            Ruch ruch = zapisywaczRuchow.WyciagnijPozycje();
            if (ruch != null)
            {
                figuraZPojemnika(ruch.poleDrugie);               
                plansza[ruch.x1, ruch.y1] = ruch.polePierwsze as Pole;
                plansza[ruch.x2, ruch.y2] = ruch.poleDrugie as Pole;
                licznikRuchow--;
                if (ruch.polePierwsze is Pionek && (ruch.y1 == 1 && (ruch.polePierwsze as Pionek).ZwrocGracza()== Gracz.CZARNE
                    || ruch.y1 == 6 && (ruch.polePierwsze as Pionek).ZwrocGracza() == Gracz.BIALE))
                {
                    (ruch.polePierwsze as Pionek).cofnijPierwszyRuch();
                }
            }
        }

        public void wykonajRuch(int x1, int y1, int x2, int y2)
        {
            zapiszRuch(x1, y1, x2, y2);
            zbitaFiguraDoPijemnika(plansza[x2, y2]);

            if (plansza[x1, y1] is Pionek)
            {
                (plansza[x1, y1] as Pionek).WykonalPierwszyRuch();
            }
            plansza[x2, y2] = plansza[x1, y1];
            plansza[x1, y1] = poleBezPionka;
            
            licznikRuchow++;
            kontrolki.Sprawdz();
        }
        private bool czyRuchNieOdkrylKrola(int x1Krola1, int y1Krola1, Gracz? gracz)
        {
            bool szach = false;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    szach = sprawdzCalyRuchZaWyjatkiemKrola(i, j, x1Krola1, y1Krola1, gracz);

                    if (szach == true)
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        
        private void zbitaFiguraDoPijemnika(Pole figura)
        {
            if (figura.ZwrocGracza() == Gracz.BIALE) pojemnikNaFigury1.Push(figura);
            else if (figura.ZwrocGracza() == Gracz.CZARNE) pojemnikNaFigury2.Push(figura);
        }
        private void figuraZPojemnika(Pole figura)
        {
            if (figura.ZwrocGracza() == Gracz.BIALE) pojemnikNaFigury1.Pop();
            else if (figura.ZwrocGracza() == Gracz.CZARNE) pojemnikNaFigury2.Pop();
        }
        private bool czyPojemnikSieWypelnil()
        {
            if (pojemnikNaFigury1.Count() == 12 || pojemnikNaFigury2.Count() == 12) return true;
            else return false;
        }

        public int IleRuchow()
        {
            return licznikRuchow;
        }
        public Pole[,] Zwroc()
        {
            return plansza;
        }
        public Stack<Pole> ZwrocPojemnik1()
        {
            return pojemnikNaFigury1;
        }
        public Stack<Pole> ZwrocPojemnik2()
        {
            return pojemnikNaFigury2;
        }


        public int OcenWartoscPlanszy()
        {
            int kontrolna = -1;
            if (licznikRuchow % 2 != 0) kontrolna = -kontrolna;

            int wartosc = 0;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    wartosc += OcenWartoscPola(plansza[i, j],kontrolna);
                }
            }
            return wartosc;
        }
        private int OcenWartoscPola(Pole pole, int kontrolna)
        {
            
            if (pole.ZwrocGracza() == Gracz.CZARNE) kontrolna = -kontrolna;
            
            
            if (pole is Pionek) return (kontrolna * 10);
            else if (pole is Goniec) return (kontrolna * 30);
            else if (pole is Skoczek) return (kontrolna * 30);
            else if (pole is Wieza) return (kontrolna * 50);
            else if (pole is Hetman) return (kontrolna * 90);
            else if (pole is Krol) return (kontrolna * 900);
            else return 0;
        }
        public List<RuchAI> ZwrocWszystkieMozliweRuchy(Gracz gracz)
        {
            List<RuchAI> ruchy = new List<RuchAI>();
     
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    for (int g = 0; g < 8; g++)
                    {
                        for (int h = 0; h < 8; h++)
                        {
                            if (SprawdzCalyRuch(i, j, g, h, gracz) == true)
                            {
                                wykonajRuch(i, j, g, h);
                                ruchy.Add(new RuchAI(i, j, g, h, this.OcenWartoscPlanszy()));
                                CofnijRuch();
                            }
                        }
                    }
                }
            }
            przelosujRuchy(ruchy);
            return ruchy;
        }
        private void przelosujRuchy(List<RuchAI> ruchy)
        {
            RuchAI wartosc;
            Random losowa = new Random();
            int k;
            int n = ruchy.Count;
            while (n > 1)
            {
                n--;
                k = losowa.Next(n + 1);
                wartosc = ruchy[k];
                ruchy[k] = ruchy[n];
                ruchy[n] = wartosc;
            }
        }


    }
}
