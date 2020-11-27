using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Tests
{
    [TestClass()]
    public class PlayerControlManagerTests
    {
        [TestMethod()]
        public void PlayerControlManagerTest()
        {
            DefaultMapBuilder mb = new DefaultMapBuilder();
            mb.BuildWalls();
            mb.BuildBox();

            Map map = mb.GetMap();

            Unit[,] units = map.units;
            Explosive[,] explosions = map.explosions;

            PlayerControlManager pcm = new PlayerControlManager(23, 19, units, explosions);

            Player p = new Player();
            p.x = 27;
            p.y = 27;

            p.action = "placeBomb";
            pcm.PlaceExplosive(p, 10);
            Assert.IsTrue(units[1, 1] is Bomb);
            units[1, 1] = null;

            p.action = "placeBombS";
            pcm.PlaceExplosive(p, 10);
            Assert.IsTrue(units[1, 1] is SuperBomb);
            units[1, 1] = null;

            p.action = "placeMine";
            pcm.PlaceExplosive(p, 10);
            Assert.IsTrue(units[1, 1] is Mine);
            units[1, 1] = null;

            p.action = "placeMineS";
            pcm.PlaceExplosive(p, 10);
            Assert.IsTrue(units[1, 1] is SuperMine);
            units[1, 1] = null;

            Boost b = new Boost(1, 1);
            b.algorithm = new SpeedBoostAlgorithm();
            units[1, 1] = b;
            pcm.ActivateBlock(p, 10);
            Assert.IsTrue(units[1, 1] is null);

            Teleporter t = new Teleporter(1, 3);
            Teleporter t2 = new Teleporter(1, 1);
            t2.SetDestination(t);
            units[1, 1] = t2;
            pcm.ActivateBlock(p, 10);

            units[1, 1] = null;
            p.x = 27;
            p.y = 27;

            explosions[1, 1] = new Explosion(1, 1, 10);
            var health = p.health;
            pcm.ActivateBlock(p, 10);
            Assert.IsTrue(health == p.health + 1);

            explosions[1, 1] = new SuperExplosion(1, 1, 10);
            health = p.health;
            pcm.ActivateBlock(p, 5000);
            Assert.IsTrue(health == p.health + 1);
        }
    }
}