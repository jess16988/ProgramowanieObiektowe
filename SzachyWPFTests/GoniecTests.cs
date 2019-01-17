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
    public class GoniecTests
    {
        [TestMethod()]
        public void GoniecTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void SprawdzRuchNaPustejPlanszyTest()
        {
            Goniec target = new Goniec(Gracz.CZARNE);
            int x1 = 3;
            int x2 = 5;
            int y1 = 3;
            int y2 = 2;
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