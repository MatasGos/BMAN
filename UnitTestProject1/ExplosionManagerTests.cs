using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Tests
{
    [TestClass()]
    public class ExplosionManagerTests
    {
        [TestMethod()]
        public void ExplosionManagerTest()
        {
            Unit[,]  units = new Unit[23, 19];
            Explosive[,] explosions = new Explosive[23, 19];

            explosions[1, 1] = new SuperExplosion(1, 1, 1);
            explosions[1, 2] = new Explosion(1, 2, 1);
            explosions[1, 3] = new Bomb(1, 3, 1, 1);
            explosions[1, 3] = new SuperBomb(1, 3, 1, 1);
            MapFacade mapFacade = new MapFacade(23, 19, units, explosions);
            Player player = new Player("id", "username", 0);


            mapFacade.PlaceExplosive(player, 0);
            mapFacade.UpdateExplosives(1);
        }

 

        [TestMethod()]
        public void UpdateExplosivesTest()
        {
            Assert.Fail();
        }
    }
}