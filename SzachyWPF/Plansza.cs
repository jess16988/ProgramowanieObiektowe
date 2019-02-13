using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SzachyWPF
{
    /// <summary>
    /// Plansza do szachow
    /// </summary>
    public class Plansza
    {
        public Plansza(bool czyGraKomputer): this()
        {
            this.czyGraKomputer = czyGraKomputer;
        }
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
            plansza[4, 0] = new Krol(Gracz.CZARNE);
            plansza[5, 0] = new Goniec(Gracz.CZARNE);
            plansza[2, 0] = new Goniec(Gracz.CZARNE);
            plansza[6, 0] = new Skoczek(Gracz.CZARNE);
            plansza[1, 0] = new Skoczek(Gracz.CZARNE);
            plansza[0, 0] = new Wieza(Gracz.CZARNE);
            plansza[7, 0] = new Wieza(Gracz.CZARNE);
            plansza[3, 7] = new Hetman(Gracz.BIALE);
            plansza[4, 7] = new Krol(Gracz.BIALE);
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
        }
        //pola        
        private Pole poleBezPionka = new PustePole();
        internal Kontrolki kontrolki; 
        private Pole[,] plansza = new Pole[8, 8];
        public int licznikRuchow = 0;
        public ZapisywaczRuchow zapisywaczRuchow = new ZapisywaczRuchow();
        private Stack<Pole> pojemnikNaFigury1 = new Stack<Pole>();
        private Stack<Pole> pojemnikNaFigury2 = new Stack<Pole>();
        internal Promocja promocja = new Promocja();
        public bool czyGraKomputer;
        private List<Przelot> przeloty = new List<Przelot>();

        //wlasciwosci
        public Pole[][] WlasciwoscPlanszy
        {
            get
            {
                return zwrocTabliceTablic(plansza);
            }
            set
            {
                this.plansza = zwrocTabliceDwuwymiarowa(value);
            }
        }

        public List<Pole> PojemnikNaFigury1
        {
            get
            {
                return pojemnikNaFigury1.ToList<Pole>();
            }
            set
            {
                this.pojemnikNaFigury1 = new Stack<Pole>(value);
            }
        }
        public List<Pole> PojemnikNaFigury2
        {
            get
            {
                return pojemnikNaFigury2.ToList<Pole>();
            }
            set
            {
                this.pojemnikNaFigury2 = new Stack<Pole>(value);
            }
        }

        //metody
        /// <summary>
        /// Wykonuje ruch na planszy z x1,y1 do x2,y2 dla gracza gracz
        /// </summary>
        /// <returns>Prawda jesli ruch sie powiodl, falsz jesli nie</returns>
        public bool RuszGlowny(int x1, int y1, int x2, int y2, Gracz gracz)
        {
            
            if (SprawdzCalyRuch(x1, y1, x2, y2, gracz) == true)
            {
                WykonajRuch(x1, y1, x2, y2);
                WyczyscPrzeloty();
                kontrolki.Sprawdz();
                promocja.Sprawdz(x2, y2, plansza[x2, y2]);
                if ((y2 - y1 == 2 || y2 - y1 == -2) && plansza[x2,y2] is Pionek)
                {                                     
                    Przelot przelot = new Przelot(gracz, x2, (y2 + y1) / 2, licznikRuchow,x2,y2);
                    plansza[x1, ((y2 + y1) / 2)] = przelot;
                    przeloty.Add(przelot);
                }
                return true;
            }
            return false;
        }
        /// <summary>
        /// Sprawdza czy mozliwy jest ruch na planszy z x1,y1 do x2,y2 wykonywany przez gracza gracz
        /// </summary>
        /// <returns>Prawda jesli tak, falsz jesli nie</returns>
        public bool SprawdzCalyRuch(int x1, int y1, int x2, int y2, Gracz? gracz)
        {
            
            if (sprawdzCalyRuchZaWyjatkiemKrola(x1, y1, x2, y2, gracz) == true)
            {
                 WykonajRuch(x1, y1, x2, y2);                
                 kontrolki.znajdzKroli(gracz);
                int x1Krola1 = kontrolki.x1Krola1;
                int y1Krola1 = kontrolki.y1Krola1;
                int x2Krola2 = kontrolki.x2Krola2;
                int y2Krola2 = kontrolki.y2Krola2;

                if (x2Krola2 == -1)
                {
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
            if (plansza[x1, y1] is Skoczek || plansza[x1, y1] is Krol|| plansza[x2,y2] is Przelot) return true;
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
            if (plansza[x1, y1].ZwrocGracza() != gracz) return false;
            if (plansza[x2, y2] is Przelot) return true;
            if (plansza[x1, y1].ZwrocGracza() == plansza[x2, y2].ZwrocGracza()) return false;            
            else return true;
        }

        private void zapiszRuch(int x1, int y1, int x2, int y2)
        {
            zapisywaczRuchow.DodajPozycje(x1, y1, plansza[x1, y1], x2, y2, plansza[x2, y2]);
        }
        /// <summary>
        /// Cofa ostatnio wykonany ruch
        /// </summary>
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
        /// <summary>
        /// Wykonuje ruch z x1,y1 do x2,y2 bez sprawdzania
        /// </summary>
        public void WykonajRuch(int x1, int y1, int x2, int y2)
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
        /// <summary>
        /// Zwraca ile zostalo wykonanych ruchow liczac od zera
        /// </summary>
        public int IleRuchow()
        {
            return licznikRuchow;
        }
        public Pole[,] Zwroc()
        {
            return plansza;
        }
        /// <summary>
        /// Zwraca pojemnik an figury gracza 1
        /// </summary>
        public Stack<Pole> ZwrocPojemnik1()
        {
            return pojemnikNaFigury1;
        }
        /// <summary>
        /// zwraca pojemnik na figury gracza 2
        /// </summary>
        public Stack<Pole> ZwrocPojemnik2()
        {
            return pojemnikNaFigury2;
        }

        /// <summary>
        /// ocenia wartosc planszy gracza ktory ma wykonac teraz ruch
        /// </summary>
        /// <returns>suma wartosci pionkow</returns>
        public int OcenWartoscPlanszy()
        {
            Gracz aktywnyGracz = licznikRuchow % 2 != 0 ? Gracz.BIALE : Gracz.CZARNE;

            int wartosc = 0;
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    wartosc += plansza[i, j].podajWartosc(aktywnyGracz);
                }
            }
            return wartosc;
        }
        /// <summary>
        /// zwraca wszystkie mozliwe ruchy dla gracza gracz
        /// </summary>
        /// <param name="gracz"> gracz wykonujacy ruchy</param>
        /// <returns>tablica obiektow RuchAI</returns>
        public List<RuchAI> ZwrocWszystkieMozliweRuchy(Gracz? gracz)
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
                                WykonajRuch(i, j, g, h);
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
        /// <summary>
        /// Metoda promocji pionka
        /// </summary>
        /// <param name="DoWypromowania">Figura do ktorej pionek zostanie wypromowany</param>
        public void Wypromuj(PromocjaWindow.Figura DoWypromowania)
        {
            int x = promocja.x;
            int y = promocja.y;
            Gracz gracz;
            if (licznikRuchow % 2 == 0) gracz = Gracz.CZARNE;
            else gracz = Gracz.BIALE;

            if (DoWypromowania == PromocjaWindow.Figura.Goniec)
            {
                plansza[x, y] = new Goniec(gracz);
            }
            else if (DoWypromowania == PromocjaWindow.Figura.Hetman)
            {
                plansza[x, y] = new Hetman(gracz);
            }
            else if (DoWypromowania == PromocjaWindow.Figura.Wieza)
            {
                plansza[x, y] = new Wieza(gracz);
            }
            else if (DoWypromowania == PromocjaWindow.Figura.Skoczek)
            {
                plansza[x, y] = new Skoczek(gracz);
            }
        }
        private void WyczyscPrzeloty()
        {
            if (przeloty.Count() != 0)
            {
                Przelot przelot = przeloty.Last();
                if (plansza[przelot.x, przelot.y] is Pionek) plansza[przelot.XPionka, przelot.YPionka] = poleBezPionka;
                else plansza[przelot.x, przelot.y] = poleBezPionka;
            }
            
        }

        private Pole[][] zwrocTabliceTablic(Pole[,] plansza)
        {
            Pole[][] nowaPlansza = new Pole[8][];
            for (int i = 0; i < 8; i++)
            {
                nowaPlansza[i] = new Pole[8];
            }

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    nowaPlansza[i][j] = plansza[i, j];
                }
            }
            return nowaPlansza;
        }
        private Pole[,] zwrocTabliceDwuwymiarowa(Pole[][] plansza)
        {
            Pole[,] nowaPlansza = new Pole[8,8];

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    nowaPlansza[i,j] = plansza[i][j];
                }
            }
            return nowaPlansza;
        }
        /// <summary>
        /// Serializuje plansze do XML
        /// </summary>
        /// <param name="nazwa">nazwa pliku</param>
        public void ZapiszXML(string nazwa)
        {
            Type[] tablicaTypow = new Type[]{ typeof(Hetman), typeof(Wieza),typeof(Pionek), typeof(Krol), typeof(Skoczek), typeof(Goniec), typeof(PustePole) };
            XmlSerializer xs = new XmlSerializer(typeof(Plansza),tablicaTypow);
            FileStream fs = new FileStream("zapisaneGry/" + nazwa+ ".xml", FileMode.Create);
            xs.Serialize(fs, this);
            fs.Close();
        }
        /// <summary>
        /// Zwraca obiekt planszy odczytany z pliku XML
        /// </summary>
        /// <param name="nazwa">nazwa pliku </param>
        /// <returns></returns>
        public static Plansza OdczytajXML(string nazwa)
        {
            Type[] tablicaTypow = new Type[] { typeof(Hetman), typeof(Wieza), typeof(Pionek), typeof(Krol), typeof(Skoczek), typeof(Goniec), typeof(PustePole) };
            XmlSerializer xs = new XmlSerializer(typeof(Plansza), tablicaTypow);
            using (FileStream fs = new FileStream("zapisaneGry/" + nazwa + ".xml", FileMode.Open))
            {
                return xs.Deserialize(fs) as Plansza;
            }            
        }
    }
}
