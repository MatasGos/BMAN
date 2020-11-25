using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Tests
{
    [TestClass()]
    public class SpeedBoostAlgorithmTests
    {
        [DataTestMethod]
        [DataRow("palyer", "id")]
        public void UseBoostTest(string username, string id)
        {
            Player player = new Player(id, username, 0);
            BoostAlgorithm boost = new SpeedBoostAlgorithm();
            int oldSpeed = player.speed;
            boost.UseBoost(player);
            Assert.AreEqual(oldSpeed + 1, player.speed);
        }
    }
}