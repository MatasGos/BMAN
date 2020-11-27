using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Tests
{
    [TestClass()]
    public class MapTests
    {
        [TestMethod()]
        public void CloneTest()
        {
            Map m = new Map(23, 19);
            Map NewM = m.Clone(false);
            Assert.IsTrue(m.explosions == NewM.explosions && m.units == NewM.units);
        }
    }
}