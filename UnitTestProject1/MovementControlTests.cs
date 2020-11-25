using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Tests
{
    [TestClass()]
    public class MovementControlTests
    {
        [TestMethod()]
        public void MovementControlTest()
        {
            Player player = new Player("id", "username", 0);
            Map map = new Map(23 * 25, 19 * 25);
            MapFacade mapFacade = map.mapFacade;
            ICommand command = new MoveRight(player, mapFacade);
            player.SetCommand(command);
            player.Move();
        }

        [TestMethod()]
        public void AddCommandTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void MoveTest()
        {
            Player player = new Player("id", "username", 0);
            Map map = new Map(23 * 25, 19 * 25);
            MapFacade mapFacade = map.mapFacade;
            int[] oldPos = player.getPos();
            ICommand moveRight = new MoveRight(player, mapFacade);
            player.SetCommand(moveRight);
            player.Move();
            Assert.AreEqual(player.getPos()[0] , oldPos[0] + player.speed);

            ICommand moveRightUp = new MoveRightUp(player, mapFacade);
            player.SetCommand(moveRightUp);
            player.Move();

            ICommand moveRightDown = new MoveRightDown(player, mapFacade);
            player.SetCommand(moveRightDown);
            player.Move();

            ICommand moveLeft = new MoveLeft(player, mapFacade);
            player.SetCommand(moveLeft);
            player.Move();

            ICommand moveLeftUp = new MoveLeftUp(player, mapFacade);
            player.SetCommand(moveLeftUp);
            player.Move();

            ICommand moveLeftDown = new MoveLeftDown(player, mapFacade);
            player.SetCommand(moveLeftDown);
            player.Move();

            ICommand moveUp = new MoveUp(player, mapFacade);
            player.SetCommand(moveUp);
            player.Move();

            ICommand moveDown = new MoveDown(player, mapFacade);
            player.SetCommand(moveDown);
            player.Move();

        }

        [TestMethod()]
        public void UndoTest()
        {
            Player player = new Player("id", "username", 0);
            Map map = new Map(23 * 25, 19 * 25);
            MapFacade mapFacade = map.mapFacade;
            int[] pos = player.getPos();

            ICommand moveRight = new MoveRight(player, mapFacade);
            player.SetCommand(moveRight);
            player.Move();

            ICommand moveRightUp = new MoveRightUp(player, mapFacade);
            player.SetCommand(moveRightUp);
            player.Move();

            ICommand moveRightDown = new MoveRightDown(player, mapFacade);
            player.SetCommand(moveRightDown);
            player.Move();

            ICommand moveLeft = new MoveLeft(player, mapFacade);
            player.SetCommand(moveLeft);
            player.Move();

            ICommand moveLeftUp = new MoveLeftUp(player, mapFacade);
            player.SetCommand(moveLeftUp);
            player.Move();

            ICommand moveLeftDown = new MoveLeftDown(player, mapFacade);
            player.SetCommand(moveLeftDown);
            player.Move();

            ICommand moveUp = new MoveUp(player, mapFacade);
            player.SetCommand(moveUp);
            player.Move();

            ICommand moveDown = new MoveDown(player, mapFacade);
            player.SetCommand(moveDown);
            player.Move();

            player.Undo();

            Assert.AreEqual(pos[0], player.getPos()[0]);
            Assert.AreEqual(pos[1], player.getPos()[1]);
        }

        [TestMethod()]
        public void ClearTest()
        {
            Player player = new Player("id", "username", 0);
            Map map = new Map(23 * 25, 19 * 25);
            MapFacade mapFacade = map.mapFacade;
            ICommand moveRight = new MoveRight(player, mapFacade);
            player.SetCommand(moveRight);
            player.Move();

            ICommand moveRightUp = new MoveRightUp(player, mapFacade);
            player.SetCommand(moveRightUp);
            player.Move();

            ICommand moveRightDown = new MoveRightDown(player, mapFacade);
            player.SetCommand(moveRightDown);
            player.Move();

            ICommand moveLeft = new MoveLeft(player, mapFacade);
            player.SetCommand(moveLeft);
            player.Move();

            ICommand moveLeftUp = new MoveLeftUp(player, mapFacade);
            player.SetCommand(moveLeftUp);
            player.Move();

            player.Undo();
            int[] pos = player.getPos();
            player.Undo();

            Assert.AreEqual(pos, player.getPos());
        }
    }
}