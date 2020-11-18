using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Tests
{
    [TestClass()]
    public class PlayerTests
    {
        [DataTestMethod]
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(3)]
        [DataRow(0)]
        //Checks if doesn't throw exception
        public void PlayerTest(int x)
        {
            Player player = new Player("id", "username", x);
        }
        [DataTestMethod]
        [DataRow(-1)]
        [DataRow(4)]
        [DataRow(5)]
        [DataRow(6)]
        public void PlayerTestFail(int x)
        {
            bool exceptionThrown = false;
            try
            {
                Player player = new Player("id", "username", x);
            }
            catch
            {
                exceptionThrown = true;
            }
            Assert.IsTrue(exceptionThrown);            
        }

        [DataTestMethod]
        [DataRow(15,50)]
        public void getPosTest(int x, int y)
        {
            Player player = new Player("id", "username", 0);
            player.x = x;
            player.y = y;
            Assert.AreEqual(x, player.getPos()[0]);
            Assert.AreEqual(y, player.getPos()[1]);
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(3)]
        [DataRow(0)]
        public void UpdatePlayerStructureTest(int x)
        {
            string structure = "fs";
            Player player = new Player("id", "username", x);
            player.UpdatePlayerStructure(structure);
            string id = player.id;
            Assert.AreEqual(player.pictureStructure, x.ToString() + structure);
        }

        [DataTestMethod]
        [DataRow(15, 50)]
        public void SetPosTest(int x, int y)
        {
            Player player = new Player("id", "username", 0);
            player.SetPos(x, y);
            Assert.AreEqual(x, player.x);
            Assert.AreEqual(y, player.y);
        }

        [DataTestMethod]
        [DataRow("placeBomb")]
        [DataRow("placeMine")]
        public void SetActionTest(string action)
        {
            Player player = new Player("id", "username", 0);
            player.SetAction(action);
            Assert.AreEqual(action, player.action);
        }
        [DataTestMethod]
        [DataRow("undo")]
        public void SetActionSecondaryTest(string action)
        {
            Player player = new Player("id", "username", 0);
            player.SetAction(action);
            Assert.AreEqual(action, player.actionSecondary);
        }

        /*[TestMethod()]
        public void updateTest()
        {
            Assert.Fail();
        }*/

        [TestMethod()]
        public void MoveTest()
        {
            Player player = new Player("id", "username", 0);
            player.SetPos(50, 50);
            Map map = new Map(23 * 25, 19 * 25);
            MapFacade mapFacade = map.mapFacade;
            ICommand command = new MoveRight(player, mapFacade);
            player.SetCommand(command);
            player.Move();
            Assert.AreEqual(50 + player.speed, player.x);
            Assert.AreEqual(50, player.y);
        }


        [TestMethod()]
        public void UndoTest()
        {
            Player player = new Player("id", "username", 0);
            player.SetPos(50, 50);
            Map map = new Map(23 * 25, 19 * 25);
            MapFacade mapFacade = map.mapFacade;
            for (int i = 0; i < 6; i++)
            {
                ICommand command = new MoveRight(player, mapFacade);
                player.SetCommand(command);
                player.Move();
            }            
            Assert.AreEqual(50 + player.speed*6, player.x);
            Assert.AreEqual(50, player.y);
            player.Undo();
            Assert.AreEqual(50, player.x);
            Assert.AreEqual(50, player.y);
        }

        [TestMethod()]
        public void UndoLimitTest()
        {
            Player player = new Player("id", "username", 0);
            player.SetPos(50, 50);
            Map map = new Map(23 * 25, 19 * 25);
            MapFacade mapFacade = map.mapFacade;
            for (int i = 0; i < 25; i++)
            {
                ICommand command = new MoveRight(player, mapFacade);
                player.SetCommand(command);
                player.Move();
            }
            Assert.AreEqual(50 + player.speed * 25, player.x);
            Assert.AreEqual(50, player.y);
            player.Undo();
            Assert.AreEqual(50 + player.speed * 5, player.x);
            Assert.AreEqual(50, player.y);
        }

        [TestMethod()]
        public void SetCommandTest()
        {
            Player player = new Player("id", "username", 0);
            player.SetPos(50, 50);
            Map map = new Map(23 * 25, 19 * 25);
            MapFacade mapFacade = map.mapFacade;
            ICommand command = new MoveRight(player, mapFacade);
            player.SetCommand(command);
            player.Move();
            Assert.IsTrue(50 != player.x || 50 != player.y);            
        }

        [TestMethod()]
        public void ClearCommandHistoryTest()
        {
            Player player = new Player("id", "username", 0);
            player.SetPos(50, 50);
            Map map = new Map(23 * 25, 19 * 25);
            MapFacade mapFacade = map.mapFacade;
            for (int i = 0; i < 10; i++)
            {
                ICommand command = new MoveRight(player, mapFacade);
                player.SetCommand(command);
                player.Move();
            }
            int playerx = player.x;
            int playery = player.y;
            player.ClearCommandHistory();
            player.Undo();
            Assert.AreEqual(playerx, player.x);
            Assert.AreEqual(playery, player.y);
        }

        [TestMethod()]
        public void ReduceHealthTest()
        {
            Player player = new Player("id", "username", 0);
            int health = player.health;
            player.ReduceHealth();
            Assert.AreEqual(health - 1, player.health);
        }

        [TestMethod()]
        public void ReduceHealthLimitTest()
        {
            Player player = new Player("id", "username", 0);
            int health = player.health;
            for (int i = 0; i < health + 1; i++)
            {
                player.ReduceHealth();
            }            
            Assert.AreEqual(0, player.health);
        }

        [TestMethod()]
        public void BecomeInvincibleTest()
        {
            Player player = new Player("id", "username", 0);
            player.BecomeInvincible(6000);
            Assert.AreEqual(6000 + 1000.0, player.invincibleUntil);
        }

        [TestMethod()]
        public void IsAliveTest()
        {
            Player player = new Player("id", "username", 0);
            Assert.IsTrue(player.IsAlive());
            int health = player.health;            
            for (int i = 0; i < health - 1; i++)
            {
                player.ReduceHealth();
            }
            Assert.IsTrue(player.IsAlive());
        }

        [TestMethod()]
        public void IsAlive2Test()
        {
            Player player = new Player("id", "username", 0);
            Assert.IsTrue(player.IsAlive());
            int health = player.health;
            for (int i = 0; i < health; i++)
            {
                player.ReduceHealth();
            }
            Assert.IsFalse(player.IsAlive());
        }

        [TestMethod()]
        public void ResetPlayerTest()
        {
            Player player = new Player("id", "username", 0);
            int health = player.health;
            int bombCount = player.bombCount;
            int speed = player.speed;
            int explosionPower = player.explosionPower;
            player.health += 20;
            player.bombCount += 1;
            player.speed += 1;
            player.explosionPower += 1;
            player.ResetPlayer();
            Assert.AreEqual(health, player.health);
            Assert.AreEqual(bombCount, player.bombCount);
            Assert.AreEqual(speed, player.speed);
            Assert.AreEqual(explosionPower, player.explosionPower);

        }
    }
}