using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Tests
{
    [TestClass()]
    public class BoxTests
    {
        [TestMethod()]
        public void BoxTest()
        {
            Box box = new Box(0, 0);
            Assert.IsTrue(box.isBreakable);
            Assert.IsTrue(box.isSolid);
        }
    }
}