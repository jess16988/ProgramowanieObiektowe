namespace SzachyWPF
{
    interface IZapisywaczRuchow
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="pole1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="pole2"></param>
        void DodajPozycje(int x1, int y1, Pole pole1, int x2, int y2, Pole pole2);
        Ruch WyciagnijPozycje();
        Ruch ZwrocjPozycje(int pozycja);
    }
}