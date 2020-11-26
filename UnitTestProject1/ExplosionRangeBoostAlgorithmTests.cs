using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Tests
{
    //Explosion range boost algorith test creates new boost and tests its functionality
    [TestClass()]
    public class ExplosionRangeBoostAlgorithmTests
    {
        [DataTestMethod]
        [DataRow("palyer", "id")]
        public void UseBoostTest(string username, string id)
        {
            Player player = new Player(id, username, 0);
            BoostAlgorithm boost = new ExplosionRangeBoostAlgorithm();
            int oldPower = player.explosionPower;
            boost.UseBoost(player);
            Assert.AreEqual(oldPower + 1, player.explosionPower);
        }
    }
}