using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using Server;
using System;
using System.Collections.Generic;
using System.Text;

namespace Server.Tests
{
    [TestClass()]
    public class ServerTests
    {
        [TestMethod()]
        public void AddPlayerTest()
        {
            ResetServer();
            Server.AddPlayer("id", "username");
            Server.AddPlayer("id2", "username2");
        }

        [TestMethod()]
        public void GetPlayersTest()
        {
            ResetServer();
            Server.AddPlayer("id", "username");
            Server.AddPlayer("id2", "username2");
            Assert.IsTrue(Server.GetPlayers().Count > 0);
        }

        [TestMethod()]
        public void GetPlayerByIdTest()
        {
            ResetServer();
            Server.AddPlayer("id", "username");
            Server.AddPlayer("id2", "username2");
            Assert.IsNotNull(Server.GetPlayerById("id2"));
        }

        [TestMethod()]
        public void UpdatePlayerSkinTest()
        {
            ResetServer();
            Server.AddPlayer("id", "username");
            Server.AddPlayer("id2", "username2");
            Server.UpdatePlayerSkin("id", "fs");
        }

        [TestMethod()]
        public void AddMapTest()
        {
            ResetServer();
            MapDirector mapDirector = new MapDirector(new ConcreteMapBuilder());
            mapDirector.constructMap();
            Map temp = mapDirector.getMap();
            Server.AddMap(temp);
        }

        [TestMethod()]
        public void GetMapByNameTest()
        {
            ResetServer();
            MapDirector mapDirector = new MapDirector(new ConcreteMapBuilder());
            mapDirector.constructMap();
            Map temp = mapDirector.getMap();
            Server.AddMap(temp);
            Assert.IsNotNull(Server.GetMapByName(mapDirector.getMap().mapName));
        }

        [TestMethod()]
        public void GetMapByName2Test()
        {
            ResetServer();
            MapDirector mapDirector = new MapDirector(new ConcreteMapBuilder());
            mapDirector.constructMap();
            Map temp = mapDirector.getMap();
            Server.AddMap(temp);
            Assert.IsNull(Server.GetMapByName("neegzistuojantis"));
        }

        [TestMethod()]
        public void CheckRoundEndTest()
        {
            ResetServer();
            ResetServer();
            Server.AddPlayer("id", "username");
            Server.AddPlayer("id2", "username2");
            Server.AddPlayer("id3", "username3");
            Server.AddPlayer("id4", "username4");
            Assert.IsFalse(Server.CheckRoundEnd());
        }
        [TestMethod()]
        public void CheckRoundEnd2Test()
        {
            ResetServer();
            ResetServer();
            Server.AddPlayer("id", "username");
            Server.AddPlayer("id2", "username2");
            Server.AddPlayer("id3", "username3");
            Server.AddPlayer("id4", "username4");
            for (Iterator iter = Server.playerList.getIterator(); iter.hasNext();)
            {
                Player p = (Player)iter.next();
                p.health = 0;
            }
            Assert.IsTrue(Server.CheckRoundEnd());
        }

        public void ResetServer()
        {
            Server.current = null;
            Server.playerList = new PlayerList();
            Server.context = null;
            Server.game = null;
            Server.mapCache = new List<Map>();
            Server.canStartRound = true;
        }
    }
}