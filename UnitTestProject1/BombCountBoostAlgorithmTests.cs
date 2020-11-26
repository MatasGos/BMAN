using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Tests
{
    [TestClass()]
    //bomb count boost unit test
    public class BombCountBoostAlgorithmTests
    {
        [DataTestMethod]
        [DataRow("palyer", "id")]
        public void UseBoostTest(string username, string id)
        {
            Player player = new Player(id, username, 0);
            BoostAlgorithm boost = new BombCountBoostAlgorithm();
            int old = player.bombCount;
            boost.UseBoost(player);
            Assert.AreEqual(old + 1, player.bombCount);
        }
    }
}