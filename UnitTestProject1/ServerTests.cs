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
        //Tests AddPlayer method by adding a few players
        public void AddPlayerTest()
        {
            ResetServer();
            Server.AddPlayer("id", "username");
            Server.AddPlayer("id2", "username2");
        }

        [TestMethod()]
        //Tests GetPlayers method by adding a few players and making sure that they were added
        public void GetPlayersTest()
        {
            ResetServer();
            Server.AddPlayer("id", "username");
            Server.AddPlayer("id2", "username2");
            Assert.IsTrue(Server.GetPlayers().Count > 0);
        }

        [TestMethod()]
        //Tests GetPlayerById method by getting a player who was previously added
        public void GetPlayerByIdTest()
        {
            ResetServer();
            Server.AddPlayer("id", "username");
            Server.AddPlayer("id2", "username2");
            Assert.IsNotNull(Server.GetPlayerById("id2"));
        }

        [TestMethod()]
        //Tests UpdatePlayerSkin method
        public void UpdatePlayerSkinTest()
        {
            ResetServer();
            Server.AddPlayer("id", "username");
            Server.AddPlayer("id2", "username2");
            Server.UpdatePlayerSkin("id", "fs");
        }

        [TestMethod()]
        //Tests AddMap method
        public void AddMapTest()
        {
            ResetServer();
            MapDirector mapDirector = new MapDirector(new ConcreteMapBuilder());
            mapDirector.constructMap();
            Map temp = mapDirector.getMap();
            Server.AddMap(temp);
        }

        [TestMethod()]
        //Tests GetMapByName method by adding a map and trying to find it using this method
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
        //Tests GetMapByName method by trying to find a map that doesn't exist (should return null)
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
        //Tests CheckRoundEnd method by adding players who are still alive (if they are all alive the round should not have ended)
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
        //Tests CheckRoundEnd method by adding players who are still alive and then making their health 0 (if they are all dead the round should have ended)
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
        
        //Helper method
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