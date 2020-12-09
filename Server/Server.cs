using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Server.Hubs;
using Model;

namespace Server
{
    public static class Server
    {
        public static MapFacade current;
        public static PlayerList playerList = new PlayerList();    
        public static IHubCallerClients context;
        public static GameServer game;
        public static List<Map> mapCache = new List<Map>();
        public static bool canStartRound = true;
        public static ScoreboardTemplate scoreboard = new ScoreboardMatch();
        public static object _locker = new object();

        public static ILogMediator mediator = new LogMediator();
        public static ConsoleLogger consoleLog = new ConsoleLogger(mediator);
        public static LogPlayer playerLog = new LogPlayer(mediator);
        public static FileLogger fileLog = new FileLogger(mediator);

        public static void InitializeLogger()
        {
            mediator.addLogReceiver(consoleLog);
            mediator.addLogSender(playerLog);
            mediator.addLogReceiver(fileLog);
            playerLog.sendMessage("Bandom");
        }

        public static bool AddPlayer(string id, string username)
        {
            bool success = false;
            lock (_locker)
            {
                if (playerList.getCount() < 4)
                {
                    Player p = new Player(id, username, playerList.getCount(), playerLog);
                    playerList.addPlayer(p);
                    scoreboard.AddPlayer(p);
                    success = true;
                }
            }
            return success;
        }

        public static List<Player> GetPlayers()
        {
            return playerList.getPlayers();
        }

        public static Player GetPlayerById(string id)
        {
            Player toReturn = null;
            lock (_locker)
            {                
                for (Iterator iter = playerList.getIterator(); iter.hasNext();)
                {
                    Player p = (Player)iter.next();
                    if (p.id == id)
                    {
                        toReturn = p;
                        break;
                    }
                }
            }
            return toReturn;
        }

        public static bool CheckUsernames(string username)
        {
            bool usernameExists = false;
            lock(_locker)
            {
                for (Iterator iter = playerList.getIterator(); iter.hasNext();)
                {
                    Player p = (Player)iter.next();
                    if (p.username == username)
                    {
                        usernameExists = true;
                        break;
                    }
                }
            }
            return usernameExists;
        }

        public static void UpdatePlayerSkin(string contextId, string skin)
        {
            for (Iterator iter = playerList.getIterator(); iter.hasNext();)
            {
                Player p = (Player)iter.next();
                if(p.id == contextId)
                {
                    p.UpdatePlayerStructure(skin);
                }
            }
        }

        public static void AddMap(Map map)
        {
            mapCache.Add(map.Clone(true));
        }

        //Returns null if didn't find
        public static Map GetMapByName(string name)
        {
            Map toReturn = null;
            foreach(var map in mapCache)
            {
                if (map.mapName.Equals(name))
                {
                    toReturn = map;
                    break;
                }
            }
            if (toReturn != null)
            {
                toReturn = toReturn.Clone(true);
            }
            return toReturn;
        }

        public static bool CheckRoundEnd()
        {
            int deadCount = 0;
            for (Iterator iter = playerList.getIterator(); iter.hasNext();)
            {
                Player p = (Player)iter.next();
                if (!p.IsAlive())
                {
                    deadCount++;
                }
            }
            if (playerList.getCount() == 1)
            {
                return deadCount == 1;
            }
            else
            {
                return deadCount == (playerList.getCount() - 1);
            }
        }

        public static void StartGame()
        {
            if (game == null)
            {                
                game = new GameServer(context);
                for (Iterator iter = playerList.getIterator(); iter.hasNext();)
                {
                    Player p = (Player)iter.next();
                    game.AddObserver(p);
                }
                Thread gameLoop = new Thread(new ThreadStart(game.GameLoop));                    
                gameLoop.Start();           
            }
        }
    }
}
