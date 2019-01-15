using System;

namespace SzachyWPF
{
    public class RuchAI : IComparable<RuchAI>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="x1"></param>
        /// <param name="y1"></param>
        /// <param name="x2"></param>
        /// <param name="y2"></param>
        /// <param name="wartosc"></param>
        public RuchAI(int x1, int y1, int x2, int y2, int wartosc)
        {
            this.x1 = x1;
            this.y1 = y1;
            this.x2 = x2;
            this.y2 = y2;
            this.wartosc = wartosc;
        }
        public int x1;
        public int y1;
        public int x2;
        public int y2;
        public int wartosc;

        public int CompareTo(RuchAI other)
        {
            return this.wartosc - other.wartosc;
        }
    }
}
