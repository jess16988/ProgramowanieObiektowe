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
using System.Windows.Shapes;

namespace SzachyWPF
{
    /// <summary>
    /// Interaction logic for WczytajWindow.xaml
    /// </summary>
    public partial class WczytajWindow : Window
    {
        public string nazwa;

        public WczytajWindow()
        {
            InitializeComponent();
            Button przycisk;
            string[] zapisy = Directory.GetFiles("zapisaneGry/");
            string nazwa;
            foreach (var zapis in zapisy)
            {
                nazwa = zapis.Substring(12);
                nazwa = nazwa.Remove(nazwa.Length - 4);
                przycisk = new Button();
                przycisk.Content = nazwa;
                przycisk.Click += wczytajPlansze;
                ZapisyPanel.Children.Add(przycisk);
            }        
        }
        public void wczytajPlansze(object sender, RoutedEventArgs e)
        {
            nazwa = (sender as Button).Content as string;
            this.Close();
        }
    }
}
