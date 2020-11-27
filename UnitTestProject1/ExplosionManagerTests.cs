using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;

namespace Model.Tests
{
    [TestClass()]
    public class ExplosionManagerTests
    {
        [TestMethod()]
        public void ExplosionManagerTest()
        {
            DefaultMapBuilder mb = new DefaultMapBuilder();
            mb.BuildWalls();
            mb.BuildBox();

            Map map = mb.GetMap();

            Unit[,] units = map.units;
            Explosive[,] explosions = map.explosions;

            ExplosionManager em = new ExplosionManager(23, 19, units, explosions);

            units[1, 1] = new Bomb(1, 1, 3, 10);
            units[1, 5] = new Bomb(1, 1, 3, 10);
            units[5, 1] = new Bomb(1, 1, 3, 10);
            units[5, 5] = new Bomb(1, 1, 3, 10);
            units[9, 1] = new Bomb(1, 1, 3, 10);
            units[1, 9] = new Bomb(1, 1, 3, 10);
            units[9, 9] = new Bomb(1, 1, 3, 10);
            units[15, 15] = new SuperBomb(15, 15, 3, 10);

            em.UpdateExplosives(10000);

            Assert.IsTrue(explosions[1, 1] is Explosion);
            Assert.IsTrue(explosions[15, 15] is SuperExplosion);

            Assert.IsTrue(units[1, 1] is null);
            Assert.IsTrue(units[15, 15] is null);

            em.UpdateExplosives(20000);

            Assert.IsTrue(explosions[1, 1] is null);
            Assert.IsTrue(explosions[15, 15] is null);
        }

        [TestMethod()]
        public void UpdateExplosivesTest()
        {
            Assert.Fail();
        }
    }
}