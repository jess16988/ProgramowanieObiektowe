﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SzachyWPF
{
    class RuchAI
    {
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
    }
}
