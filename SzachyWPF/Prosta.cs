using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;

namespace SzachyWPF
{
    class Prosta
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        public Prosta(int x1, int y1, int x2, int y2)
        {
            if ((x2 - x1) == 0)
            {
                x = x1;
                if (y2 > y1)
                {
                    this.y2 = y2;
                    this.y1 = y1;
                }
                else
                {
                    this.y1 = y2;
                    this.y2 = y1;
                }
            }
            else
            {
                a = ((y2 - y1) / (x2 - x1));
                b = y2 - ((y1 - y2) / (x1 - x2) * x2);
                if (x1 < x2)
                {
                    this.x1 = x1;
                    this.x2 = x2;
                }
                else
                {
                    this.x1 = x2;
                    this.x2 = x1;
                }
            }
        }

        //pola
        private double a = 0;
        private double b = 0;
        private int[,] liczby = new int[2,10];
        int licznik = 0;
        private int x = 100;
        private int y1;
        private int y2;
        private int x1;
        private int x2;

        //metody
        private int[,] wypiszWszystkiePunktyOproczPierwszegoIOstatniego()
        {
            
            if (x != 100)
            {
                y1++;
                while (y1 < y2)
                {
                    liczby[0, licznik] = x;
                    liczby[1, licznik] = y1;
                    y1++;
                    licznik++;
                }
                liczby[0, licznik] = 100;
                return liczby;

            }
            else
            {
                x1++;
                while (x1 < x2)
                {
                    liczby[0, licznik] = x1;
                    liczby[1, licznik] = (int)(a * x1 + b);
                    x1++;
                    licznik++;
                }
                liczby[0, licznik] = 100;
                return liczby;
            }
        }
        static public int[,] ZwrocPunktyKolizji(int x1,int y1,int x2,int y2)
        {
            Prosta prosta = new Prosta(x1, y1, x2, y2);
            return prosta.wypiszWszystkiePunktyOproczPierwszegoIOstatniego();
        }
    }
}
