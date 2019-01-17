using Microsoft.VisualStudio.TestTools.UnitTesting;
using SzachyWPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SzachyWPF.Tests
{
    [TestClass()]
    public class PionekTests
    {
        [TestMethod()]
        public void PionekTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void PionekTest1()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void SprawdzRuchNaPustejPlanszyTest()
        {
            Pionek target = new Pionek(Gracz.CZARNE);
            int x1 = 5;
            int x2 = 5;
            int y1 = 2;
            int y2 = 3;
            bool expected = true;
            bool actual = target.SprawdzRuchNaPustejPlanszy(x1, y1, x2, y2);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void SprawdzRuchDoBiciaTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void cofnijPierwszyRuchTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void WykonalPierwszyRuchTest()
        {
            Assert.Fail();
        }
    }
}