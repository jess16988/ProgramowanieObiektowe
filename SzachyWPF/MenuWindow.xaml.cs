using System;
using System.Collections.Generic;
using System.ComponentModel;
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
    /// Logika interakcji dla klasy Window1.xaml
    /// </summary>
    public partial class MenuWindow : Window
    {
        public MenuWindow()
        {
            InitializeComponent();
        }

        private void nowaGraAIButton_Click_1(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow(true);
            mainWindow.Closing += ShowMenu;
            mainWindow.Show();
            this.Close();
        }

        public static void ShowMenu(object sender, CancelEventArgs e)
        {
            MenuWindow menu = new MenuWindow();
            menu.Show();
        }

        private void nowaGraCzlowiekButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow(false);
            mainWindow.Closing += ShowMenu;
            mainWindow.Show();
            this.Close();
        }

        private void wczytajGreButton_Click(object sender, RoutedEventArgs e)
        {
            WczytajWindow oknoWczytywania = new WczytajWindow();
            oknoWczytywania.ShowDialog();
            Plansza plansza;
            if (oknoWczytywania.nazwa != null)
            {
                plansza = Plansza.OdczytajXML(oknoWczytywania.nazwa);
                MainWindow mainWindow = new MainWindow(plansza, plansza.czyGraKomputer);
                mainWindow.Closing += ShowMenu;
                mainWindow.Show();
                this.Close();
            }
        }

        private void zapiszGreButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Nie zaimplementowano");
        }

        private void tablicaWynikowButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Nie zaimplementowano");
        }
    }
}
