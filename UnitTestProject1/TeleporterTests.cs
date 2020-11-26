using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Tests
{
    [TestClass()]
    public class TeleporterTests
    {
        [DataTestMethod]
        [DataRow(0,0)]
        [DataRow(0,10)]
        [DataRow(50,50)]
        //Tests the constructor by checking if the correct values are assigned and if the default values are correct
        public void TeleporterTest(int x, int y)
        {
            Teleporter teleporter = new Teleporter(x, y);
            Assert.AreEqual(x, teleporter.x);
            Assert.AreEqual(y, teleporter.y);
            Assert.AreEqual(false, teleporter.isBreakable);
            Assert.AreEqual(false, teleporter.isSolid);
            Assert.AreEqual(null, teleporter.GetDestination());
            Assert.AreEqual(false, teleporter.HasDestination());
        }

        [TestMethod()]
        //Tests SetDestination method by checking if the destination was assigned
        public void SetDestinationTest()
        {
            Teleporter teleporterin = new Teleporter(0, 0);
            Teleporter teleporterout = new Teleporter(1, 1);
            teleporterin.SetDestination(teleporterout);
            Assert.IsNotNull(teleporterin.GetDestination());
        }

        [TestMethod()]
        //Tests GetDestination method by assigning a destination and then checking if the destination that was set is the same
        public void GetDestinationTest()
        {
            Teleporter teleporterin = new Teleporter(0, 0);
            Teleporter teleporterout = new Teleporter(1, 1);
            teleporterin.SetDestination(teleporterout);
            Assert.IsTrue(teleporterin.HasDestination());
            Assert.AreEqual(teleporterin.GetDestination(), teleporterout);
        }

        [TestMethod()]
        //Tests HasDestination method by setting a destination and checking if it returns true
        public void HasDestinationTest()
        {
            Teleporter teleporterin = new Teleporter(0, 0);
            Teleporter teleporterout = new Teleporter(1, 1);
            teleporterin.SetDestination(teleporterout);
            Assert.IsTrue(teleporterin.HasDestination());
        }
    }
}