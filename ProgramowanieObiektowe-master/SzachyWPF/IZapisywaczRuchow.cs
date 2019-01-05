namespace SzachyWPF
{
    interface IZapisywaczRuchow
    {
        void DodajPozycje(int x1, int y1, Pole pole1, int x2, int y2, Pole pole2);
        Ruch WyciagnijPozycje();
        Ruch ZwrocjPozycje(int pozycja);
    }
}