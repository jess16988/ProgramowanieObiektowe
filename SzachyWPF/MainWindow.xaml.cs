using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Serialization;

namespace SzachyWPF
{
    /// <summary>
    /// Widok Main Window (planszy do gry)
    /// </summary>
  
    public partial class MainWindow : Window
    {

        //pola
        private Image[] obrazkiZbitych1 = new Image[16];
        private Image[] obrazkiZbitych2 = new Image[16];
        private Button[,] pola = new Button[8, 8];
        private Image[,] obrazkiDlaPol = new Image[8, 8];
        private int x1 = -10;
        private int y1;
        private int x2;
        private int y2;
        BitmapImage[] obrazki = new BitmapImage[16];
        private Plansza plansza;
        private bool czyGraKomputer = false;     
        private int xSzachowanegoKrola;
        private int ySzachowanegoKrola;
        AI ai;

        /// <summary>
        /// Wygląd planszy w przypadku gry z komputerem
        /// </summary>

        public MainWindow(Plansza plansza, bool czyGraKomputer)
        {
            this.plansza = plansza;
            this.czyGraKomputer = czyGraKomputer;
            if(czyGraKomputer) ai = new AI();
            InitializeComponent();
            utworzPola();
            wczytajObrazkizPlikow();
            rysujPlansze();
        }
        public MainWindow(bool czyGraKomputer)
        {
            plansza = new Plansza(czyGraKomputer);
            this.czyGraKomputer = czyGraKomputer;
            if (czyGraKomputer) ai = new AI();
            InitializeComponent();
            utworzPola();
            wczytajObrazkizPlikow();
            rysujPlansze();            
        }
        //metody        
        /// <summary>
        /// Tworzenie pól na planszy. Ustawianie je jako przyciski, by móc wybierać pole na które figura ma się przesunąć.
        /// Nadawanie kolorów planszy.
        /// </summary>
        private void utworzPola()
        {
            int[] xy = new int[2];
            for (int i = 0; i < 8; i++)
            {            
                for (int j = 0; j < 8; j++)
                {
                    pola[i, j] = new Button
                    {
                        Tag = new int[] { i, j },
                        BorderBrush = Brushes.Black,
                        Content = null
                    };
                    Grid.SetRow(pola[i,j], j);
                    Grid.SetColumn(pola[i, j], i);
                    szachownica.Children.Add(pola[i, j]);

                    if (i % 2 != 0 && j % 2 == 0 || i % 2 == 0 && j % 2 != 0)
                    {
                        pola[i, j].Background = Brushes.Gray;
                    }
                    else pola[i, j].Background = Brushes.White;

                    pola[i, j].Click += przeslijPozycje;
                }              
            }
        }

        /// <summary>
        /// Wybór figury(dokładniej pozycji na której się znajdują) i przesunięcie jej na inne pole.
        /// Jeśli kliknięte jest dwa razy to samo pole - ruch się nie wykonuję, cofa się kliknięcie
        /// i znowu można wybrać figure.
        /// </summary>
        private void przeslijPozycje(object sender, RoutedEventArgs e)
        {

            Button przycisk = sender as Button;
            int[] pozycja = (przycisk).Tag as int[];
            if (x1 == -10)
            {
                zaznacz(przycisk);
                x1 = pozycja[0];
                y1 = pozycja[1];
                if (plansza.IleRuchow() % 2 == 0) zaznaczPola(x1, y1, Gracz.BIALE);
                else zaznaczPola(x1, y1, Gracz.CZARNE);
            }
            else
            {
                bool czyWykonalRuch = false;
                odznaczWszystko();
                x2 = pozycja[0];
                y2 = pozycja[1];
                if (plansza.IleRuchow() % 2 == 0)
                {
                    czyWykonalRuch = plansza.RuszGlowny(x1, y1, x2, y2, Gracz.BIALE);

                }
                else
                {
                    czyWykonalRuch = plansza.RuszGlowny(x1, y1, x2, y2, Gracz.CZARNE);
                }

                x1 = -10;


                if (czyWykonalRuch == true)
                {
                    pola[xSzachowanegoKrola, ySzachowanegoKrola].BorderBrush = Brushes.Black;
                    pola[xSzachowanegoKrola, ySzachowanegoKrola].BorderThickness = new Thickness(1);
                    if (czyGraKomputer == true)
                    {
                        //AI
                        rysujPlansze(); 
                        rysujPojemniki();
                        WykonajRuchDlaAI();
                        //AI
                    }
                    rysujPlansze();
                    rysujPojemniki();
                }


            }
        }
        private void zaznacz(Button przycisk)
        {
            przycisk.BorderBrush = Brushes.Green;
            przycisk.BorderThickness = new Thickness(4);
        }
        private void odznaczWszystko()
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    pola[i, j].BorderBrush = Brushes.Black;
                    pola[i, j].BorderThickness = new Thickness(1);
                }
            }
            if (plansza.kontrolki.czySzach == true)
            {
                pola[xSzachowanegoKrola, ySzachowanegoKrola].BorderBrush = Brushes.Red;
                pola[xSzachowanegoKrola, ySzachowanegoKrola].BorderThickness = new Thickness(4);
            }
        }
        private void wczytajObrazkizPlikow()
        {
            for (int i = 0; i < 12; i++)
            {
                obrazki[i] = new BitmapImage(new Uri("images\\" + (i + 1) + ".png", UriKind.Relative));
                obrazki[i].Freeze();
            }
            
        }
        private void rysujPlansze()
        {
            zareagujNaPromocje();
            Pole[,] tablicaPol = plansza.Zwroc();
            BitmapImage b = new BitmapImage();            
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    obrazkiDlaPol[j, i] = new Image();
                    b = zwrocPasjacyObrazek(tablicaPol[j, i]);
                    if(b != null)
                    {
                        obrazkiDlaPol[j, i].Source = b;
                        pola[j, i].Content = obrazkiDlaPol[j, i];
                    }
                    else
                    {
                        pola[j, i].Content = null;
                    }
                    
                }
            }
            zareagujNaSzachMatPat();
        }
        private BitmapImage zwrocPasjacyObrazek(Pole pole)
        {
            if(pole.ZwrocSymbol() != " ")
            {
                if (pole is Przelot) return obrazki[0];
                if (pole is Pionek && pole.ZwrocGracza() == Gracz.BIALE)
                    return obrazki[0];
                else if (pole is Pionek && pole.ZwrocGracza() == Gracz.CZARNE)
                    return obrazki[6];
                else if (pole is Skoczek && pole.ZwrocGracza() == Gracz.BIALE)
                    return obrazki[1];
                else if (pole is Skoczek && pole.ZwrocGracza() == Gracz.CZARNE)
                    return obrazki[7];
                else if (pole is Goniec && pole.ZwrocGracza() == Gracz.BIALE)
                    return obrazki[2];
                else if (pole is Goniec && pole.ZwrocGracza() == Gracz.CZARNE)
                    return obrazki[8];
                else if (pole is Wieza && pole.ZwrocGracza() == Gracz.BIALE)
                    return obrazki[3];
                else if (pole is Wieza && pole.ZwrocGracza() == Gracz.CZARNE)
                    return obrazki[9];
                else if (pole is Krol && pole.ZwrocGracza() == Gracz.BIALE)
                    return obrazki[4];
                else if (pole is Krol && pole.ZwrocGracza() == Gracz.CZARNE)
                    return obrazki[10];
                else if (pole is Hetman && pole.ZwrocGracza() == Gracz.BIALE)
                    return obrazki[5];
                else if (pole is Hetman && pole.ZwrocGracza() == Gracz.CZARNE)
                    return obrazki[11];           
            }
            else
            {
                return null;
            }
            return null;
        }
        private void zaznaczPola(int x1, int y1, Gracz gracz)
        {
            Pole[,] pionki = plansza.Zwroc();
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {  
                    if(plansza.SprawdzCalyRuch(x1,y1,j,i, gracz) == true)
                    {                        
                            pola[j, i].BorderThickness = new Thickness(4);
                            pola[j, i].BorderBrush = Brushes.Blue;
                        
                    }
                }
            }
        }

        private void odswiez_Click(object sender, RoutedEventArgs e)
        {
            (sender as Button).Content = plansza.OcenWartoscPlanszy();
            rysujPlansze();
        }
        private void rysujPojemnik(Stack<Pole> pojemnik, Image[] obrazkiZbitych, StackPanel stackPanel1, StackPanel stackPanel2) 
        {
            int i = 0;
            BitmapImage b = new BitmapImage();
            stackPanel1.Children.Clear();
            stackPanel2.Children.Clear();
            double wysokosc = pola[0, 0].ActualHeight;
            double szerokosc = pola[0, 0].ActualWidth;
            foreach (var figura in pojemnik)
            {               
                obrazkiZbitych[i] = new Image();
                obrazkiZbitych[i].Height = wysokosc;
                obrazkiZbitych[i].Width = szerokosc;
                if(figura is Przelot) obrazkiZbitych[i].Source = zwrocPasjacyObrazek(new Pionek(figura.gracz));
                else obrazkiZbitych[i].Source = zwrocPasjacyObrazek(figura);         
                if(i<8) stackPanel1.Children.Add(obrazkiZbitych[i]);
                else stackPanel2.Children.Add(obrazkiZbitych[i]);
                i++;
            }
            
        }
        private void rysujPojemniki()
        {
            rysujPojemnik(plansza.ZwrocPojemnik1(), obrazkiZbitych1, pojemnik1, pojemnik2);
            rysujPojemnik(plansza.ZwrocPojemnik2(), obrazkiZbitych2, pojemnik3, pojemnik4);
        }
        private void zareagujNaSzachMatPat()
        {
            string wiadomosc = "";
            if (plansza.kontrolki.czyMat == true) wiadomosc = "MAT";
            else if (plansza.kontrolki.czyPat == true) wiadomosc = "PAT";
            else if (plansza.kontrolki.czySzach == true)
            {
                xSzachowanegoKrola = plansza.kontrolki.x2Krola2;
                ySzachowanegoKrola = plansza.kontrolki.y2Krola2;
                pola[plansza.kontrolki.x2Krola2, plansza.kontrolki.y2Krola2].BorderBrush = Brushes.Red;
                pola[plansza.kontrolki.x2Krola2, plansza.kontrolki.y2Krola2].BorderThickness = new Thickness(4);
            }
            else
            {
                pola[plansza.kontrolki.x2Krola2, plansza.kontrolki.y2Krola2].BorderBrush = Brushes.Black;
                pola[plansza.kontrolki.x2Krola2, plansza.kontrolki.y2Krola2].BorderThickness = new Thickness(1);
            }
            if (wiadomosc != "")
                MessageBox.Show(wiadomosc);

        }
        private void zareagujNaPromocje()
        {
            if (plansza.promocja.czyPromocja == true)
            {
                if (plansza.IleRuchow() % 2 == 0 && czyGraKomputer)
                {
                    plansza.Wypromuj(PromocjaWindow.Figura.Hetman);
                }
                else
                {
                    PromocjaWindow okno = new PromocjaWindow(obrazki, plansza.IleRuchow());
                    okno.ShowDialog();
                    plansza.Wypromuj(okno.DoWypromowania);
                }

            }
        }

        /// <summary>
        /// Wykonanie ruchu komputera.
        /// </summary>
        public void WykonajRuchDlaAI()
        {
            RuchAI ruch = ai.ZwrocNajlepszyRuch(plansza);
            plansza.RuszGlowny(ruch.x1, ruch.y1, ruch.x2, ruch.y2,Gracz.CZARNE);
        }

        private void zapiszGre(object sender, RoutedEventArgs e)
        {
            ZapiszWindow oknoZapisu = new ZapiszWindow();
            oknoZapisu.ShowDialog();
            if (oknoZapisu.CzyZapisac()) plansza.ZapiszXML(oknoZapisu.sciezka);
        }


    }
}
