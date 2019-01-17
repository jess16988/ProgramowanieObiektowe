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
    public class WiezaTests
    {
        [TestMethod()]
        public void WiezaTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void WiezaTest1()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void SprawdzRuchNaPustejPlanszyTest()
        {
            Wieza target = new Wieza(Gracz.CZARNE);
            int x1 = 3;
            int x2 = 3;
            int y1 = 3;
            int y2 = 2;
            bool expected = true;
            bool actual = target.SprawdzRuchNaPustejPlanszy(x1, y1, x2, y2);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod()]
        public void SprawdzRuchDoBiciaTest()
        {
            Assert.Fail();
        }
    }
}