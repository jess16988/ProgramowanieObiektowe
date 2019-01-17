using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SzachyWPF
{
    public class Ruch
    {
        /// <summary>
        /// 
        /// </summary>

        public Ruch()
        {

        }
        public Pole polePierwsze { get; } = new PustePole();
        public int x1 { get; }
        public int y1 { get; }
        public Pole poleDrugie { get; } = new PustePole();
        public int x2 { get; }
        public int y2 { get; }

        public Ruch(int x1, int y1, Pole polePierwsze, int x2, int y2, Pole poleDrugie)
        {
            this.polePierwsze = polePierwsze;
            this.x1 = x1;
            this.y1 = y1;
            this.poleDrugie = poleDrugie;
            this.x2 = x2;
            this.y2 = y2;
        }
    }
}
