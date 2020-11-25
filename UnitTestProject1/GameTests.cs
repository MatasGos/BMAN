using Microsoft.VisualStudio.TestTools.UnitTesting;
using Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using Model;

namespace Client.Tests
{
    [TestClass()]
    public class GameTests
    {
        [TestMethod()]
        public void GameTest()
        {
            Game<Bitmap, Color> game = new Game<Bitmap, Color>();
            Assert.AreEqual(23, game.xSize);
            Assert.AreEqual(19, game.ySize);
            Assert.AreEqual(15, game.playerSize);
            Assert.AreEqual(25, game.unitSize);
        }

        [TestMethod()]
        public void FormPlayerImagesTest()
        {
            Game<Bitmap, Color> game = new Game<Bitmap, Color>();
            game.players = new List<Player>();
            for (int i = 0; i < 4; i++)
            {
                Player x = new Player("id", "username", i);
                x.x = i * 25;
                x.y = 0;
                game.players.Add(x);
            }
            game.FormPlayerImages();
            game.drawBackground();
            game.drawMap();
            Bitmap picture = game.GetField().GetImage();
            bool playerDrawn;
            for (int i = 0; i < 4; i++)
            {
                playerDrawn = false;
                for (int x = i*25; x < game.playerSize + i*25; x++)
                {
                    for (int y = 0; y < game.playerSize; y++)
                    {
                        if (Color.BurlyWood.ToArgb() != picture.GetPixel(x,y).ToArgb())
                        {
                            playerDrawn = true;
                        }                        
                    }
                }
                Assert.IsTrue(playerDrawn);
            }
        }

        [TestMethod()]
        public void drawBackgroundTest()
        {
            Game<Bitmap, Color> game = new Game<Bitmap, Color>();
            game.map = new Map(game.xSize, game.ySize);
            game.map.units[1, 0] = (Unit)new Wall(0, 1);
            game.drawBackground();
            game.drawMap();
            Color[,] picture = game.GetField().GetColorArray();
            PictureFlyweight<Bitmap, Color> pictures = new PictureFlyweight<Bitmap, Color>();
            Color[,] wallPicture = pictures.GetPictureArray("wall");
            for (int x = 0; x < game.unitSize; x++)
            {
                for (int y = 0; y < game.unitSize; y++)
                {
                    Assert.AreEqual(picture[x+game.unitSize, y].ToArgb(), wallPicture[x, y].ToArgb());
                }
            }            
        }

        [TestMethod()]
        public void drawMapTest()
        {
            Game<Bitmap, Color> game = new Game<Bitmap, Color>();
            game.map = new Map(game.xSize, game.ySize);
            game.map.units[0, 0] = (Unit)new Bomb(0, 0, 0, 0.0);
            game.map.units[1, 0] = (Unit)new Mine(1, 0);
            game.drawBackground();
            game.drawMap();
            Color[,] picture = game.GetField().GetColorArray();
            PictureFlyweight<Bitmap, Color> pictures = new PictureFlyweight<Bitmap, Color>();
            Color[,] bombPicture = pictures.GetPictureArray("bomb");
            Color[,] minePicture = pictures.GetPictureArray("mine");
            for (int x = 0; x < game.unitSize; x++)
            {
                for (int y = 0; y < game.unitSize; y++)
                {
                    Assert.AreEqual(picture[x, y].ToArgb(), bombPicture[x, y].ToArgb());
                    Assert.AreEqual(picture[x + game.unitSize, y].ToArgb(), minePicture[x, y].ToArgb());
                }
            }
        }

        [TestMethod()]
        //Patikrina ar nėra exception su visais įmanomais elementų tipais.
        public void drawMap2Test()
        {
            Game<Bitmap, Color> game = new Game<Bitmap, Color>();
            game.map = new Map(game.xSize, game.ySize);
            game.map.units[0, 0] = (Unit)new Bomb(0, 0, 0, 0.0);
            game.map.units[1, 0] = (Unit)new Mine(1, 0);
            game.map.units[2, 0] = (Unit)new Box(2, 0);
            game.map.units[3, 0] = (Unit)new SuperBomb(3, 0, 0, 0.0);
            game.map.units[4, 0] = (Unit)new SuperMine(4, 0);
            game.map.units[5, 0] = (Unit)new Boost(5, 0);
            Teleporter teleporterin = new Teleporter(6, 0);
            Teleporter teleporterout = new Teleporter(7, 0);
            teleporterin.SetDestination(teleporterout);
            game.map.units[6, 0] = teleporterin;
            game.map.units[7, 0] = teleporterout;
            game.map.explosions[8, 0] = new Explosion(8, 0, 0.0);
            game.map.explosions[9, 0] = new SuperExplosion(9, 0, 0.0);
            for (int i = 0; i < 4; i++)
            {
                Player x = new Player("id", "username", i);
                x.UpdatePlayerStructure("fs");
                x.x = i * 25;
                x.y = 0;
                game.players.Add(x);
            }
            game.drawBackground();
            game.drawMap();
        }

        [TestMethod()]
        public void drawMap3Test()
        {
            Game<Bitmap, Color> game = new Game<Bitmap, Color>();
            game.map = new Map(game.xSize, game.ySize);
            game.map.units[0, 0] = new Explosion(8, 0, 0.0);
            //game.map.explosions[0, 1] = new SuperExplosion(9, 0, 0.0);
            for (int i = 0; i < 4; i++)
            {
                Player x = new Player("id", "username", i);
                x.UpdatePlayerStructure("fs");
                x.x = i * 25;
                x.y = 0;
                game.players.Add(x);
            }
            game.drawBackground();
            bool exception_Thrown = false;
            try
            {
                game.drawMap();
            }
            catch
            {
                exception_Thrown = true;
            }
            Assert.IsTrue(exception_Thrown);
        }

        [TestMethod()]
        public void drawMap4Test()
        {
            Game<Bitmap, Color> game = new Game<Bitmap, Color>();
            game.map = new Map(game.xSize, game.ySize);
            game.map.explosions[0, 0] = new Bomb(0, 0, 0, 0);
            for (int i = 0; i < 4; i++)
            {
                Player x = new Player("id", "username", i);
                x.UpdatePlayerStructure("fs");
                x.x = i * 25;
                x.y = 0;
                game.players.Add(x);
            }
            game.drawBackground();
            bool exception_Thrown = false;
            try
            {
                game.drawMap();
            }
            catch
            {
                exception_Thrown = true;
            }
            Assert.IsTrue(exception_Thrown);
        }



        [TestMethod()]
        public void GetFieldTest()
        {
            Game<Bitmap, Color> game = new Game<Bitmap, Color>();
            game.map = new Map(game.xSize, game.ySize);
            game.drawBackground();
            game.drawMap();
            Assert.IsNotNull(game.GetField());
        }
    }
}