﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Tests
{
    [TestClass()]
    public class BoostTests
    {
        [DataTestMethod]
        [DataRow(0,0)]
        [DataRow(0,10)]
        [DataRow(50,50)]
        public void BoostTest(int x, int y)
        {
            Boost boost = new Boost(x, y);
            Assert.IsFalse(boost.isBreakable);
            Assert.IsFalse(boost.isSolid);
        }
    }
}