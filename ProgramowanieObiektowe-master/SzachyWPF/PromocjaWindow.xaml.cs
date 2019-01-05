using System;
using System.Collections.Generic;
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
using System.Windows.Shapes;

namespace SzachyWPF
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class PromocjaWindow : Window
    {
        public PromocjaWindow(BitmapImage[] obrazki, int ktoraTura)
        {
            InitializeComponent();
            Image obrazekHetman = new Image();
            Image obrazekGoniec = new Image();
            Image obrazekSkoczek = new Image();
            Image obrazekWieza = new Image();
            
            if (ktoraTura % 2 == 0)
            {
                obrazekHetman.Source = obrazki[11];
                obrazekSkoczek.Source = obrazki[7];
                obrazekGoniec.Source = obrazki[8];
                obrazekWieza.Source = obrazki[9];
            }
            else
            {
                obrazekHetman.Source = obrazki[5];
                obrazekSkoczek.Source = obrazki[1];
                obrazekGoniec.Source = obrazki[2];
                obrazekWieza.Source = obrazki[3];
            }
            Hetman.Content = obrazekHetman;
            Wieza.Content = obrazekWieza;
            Goniec.Content = obrazekGoniec;
            Skoczek.Content = obrazekSkoczek;
            
        }
        
        public enum Figura
        {
            Hetman,
            Goniec,
            Skoczek,
            Wieza
        }

        //pola
        public Figura DoWypromowania;
          
        //metody
        private void Hetman_Click(object sender, RoutedEventArgs e)
        {
            DoWypromowania = Figura.Hetman;
            this.Close();

        }
        private void Goniec_Click(object sender, RoutedEventArgs e)
        {
            DoWypromowania = Figura.Goniec;
            this.Close();            
        }
        private void Wieza_Click(object sender, RoutedEventArgs e)
        {
            DoWypromowania = Figura.Wieza;
            this.Close();            
        }
        private void Skoczek_Click(object sender, RoutedEventArgs e)
        {
            DoWypromowania = Figura.Skoczek;
            this.Close();
        }

        

    }
}
