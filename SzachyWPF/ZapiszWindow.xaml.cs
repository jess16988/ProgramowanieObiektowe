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
    /// Interaction logic for ZapiszWindow.xaml
    /// </summary>
    public partial class ZapiszWindow : Window
    {

        //pola
        private bool czyZapisac;

        public ZapiszWindow()
        {
            InitializeComponent();
        }

        private void zapiszGre(object sender, RoutedEventArgs e)
        {
            this.czyZapisac = true;
            this.Close();
        }
        public bool CzyZapisac()
        {
            if (this.czyZapisac == true)
            {
                this.czyZapisac = false;
                return true;
            }
            else return false;
        }
    }
}
