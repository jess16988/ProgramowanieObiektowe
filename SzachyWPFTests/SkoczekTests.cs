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
    public class SkoczekTests
    {
        [TestMethod()]
        public void SkoczekTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void SkoczekTest1()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void SprawdzRuchNaPustejPlanszyTest()
        {
            Skoczek target = new Skoczek(Gracz.CZARNE);
            int x1 = 3;
            int x2 = 5;
            int y1 = 3;
            int y2 = 1;
            bool expected = false;
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