using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Tests
{
    [TestClass()]
    public class BombTests
    {
        [DataTestMethod]
        [DataRow(0, 0, 2, 5)]
        [DataRow(0, 10, 2, 10)]
        [DataRow(50, 50, 3, 15)]
        public void BombTest(int x, int y, int explosionPower, double placeTime)
        {
            Bomb bomb = new Bomb(x, y, explosionPower, placeTime);
            Assert.AreEqual(x, bomb.x);
            Assert.AreEqual(y, bomb.y);
            Assert.AreEqual(explosionPower, bomb.explosionPower);
            Assert.AreEqual(placeTime + 2000, bomb.detonationTime);
        }
    }
}