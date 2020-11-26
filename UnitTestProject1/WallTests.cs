using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Tests
{
    [TestClass()]
    public class WallTests
    {
        [TestMethod()]
        //Tests constructor by checking if the correct default values are assigned
        public void WallTest()
        {
            Wall wall = new Wall(0, 0);
            Assert.IsFalse(wall.isBreakable);
            Assert.IsTrue(wall.isSolid);
        }
    }
}