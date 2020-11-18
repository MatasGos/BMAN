using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Tests
{
    [TestClass()]
    public class BoostTests
    {
        [TestMethod()]
        public void BoostTest()
        {
            Boost boost = new Boost(0, 0);
            Assert.IsFalse(boost.isBreakable);
            Assert.IsFalse(boost.isSolid);
        }
    }
}