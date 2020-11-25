using Microsoft.AspNetCore.SignalR;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using Server;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Server.Tests
{
    [TestClass()]
    public class GameServerTests
    {
        [TestMethod()]
        public void GameServerTest()
        {
            ResetServer();
            for (int i = 0; i < 4; i++)
            {
                Server.AddPlayer("id"+i, "username"+i);
            }
            IHubCallerClients context = new HubTest();
            GameServer game = new GameServer(context);
        }

        [TestMethod()]
        public void GameSetupTest()
        {
            ResetServer();
            for (int i = 0; i < 4; i++)
            {
                Server.AddPlayer("id" + i, "username" + i);
            }
            IHubCallerClients context = new HubTest();
            GameServer game = new GameServer(context);
            Assert.AreEqual(1, Server.mapCache.Count);
            Assert.IsNotNull(Server.current);
        }

        [TestMethod()]
        public void AddObserverTest()
        {
            ResetServer();
            IHubCallerClients context = new HubTest();
            HubTest temp = (HubTest)context;
            for (int i = 0; i < 4; i++)
            {
                Server.AddPlayer("id" + i, "username" + i);
                temp.AddClient(Server.GetPlayers()[i]);
            }
            
           
            GameServer game = new GameServer(context);
            for (int i = 0; i < 4; i++)
            {
                game.AddObserver(Server.GetPlayers()[i]);
            }
        }

        [TestMethod()]
        public void GameLogicTest()
        {
            ResetServer();
            IHubCallerClients context = new HubTest();
            HubTest temp = (HubTest)context;
            for (int i = 0; i < 4; i++)
            {
                Server.AddPlayer("id" + i, "username" + i);
                temp.AddClient(Server.GetPlayers()[i]);
            }
            GameServer game = new GameServer(context);
            for (int i = 0; i < 4; i++)
            {
                game.AddObserver(Server.GetPlayers()[i]);
            }
            game.sw = new Stopwatch();
            game.sw.Start();
            game.GameLogic();
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