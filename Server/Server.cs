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
        public static List<Player> playerList = new List<Player>();    
        public static IHubCallerClients context;
        public static GameServer game;
        public static List<Map> mapCache = new List<Map>();

        public static void AddPlayer(string id, string username)
        {
            Player p = new Player(id, username, playerList.Count);
            Console.WriteLine(p.pictureStructure);
            playerList.Add(p);
        }

        public static List<Player> GetPlayers()
        {
            return playerList;
        }

        public static Player GetPlayerById(string id)
        {
            Player toReturn = null;
            foreach (var player in playerList)
            {
                if (player.id == id)
                {
                    toReturn = player;
                    break;
                }
            }
            return toReturn;
        }

        public static void UpdatePlayerSkin(string contextId, string skin)
        {
            for (int i = 0; i < playerList.Count; i++)
            {
                if(playerList[i].id == contextId)
                {
                    playerList[i].UpdatePlayerStructure(skin);
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

        public static void StartGame()
        {
            if (game == null)
            {
                game = new GameServer(context);
                foreach(var player in playerList)
                {
                    game.AddObserver(player);
                }
                Thread gameLoop = new Thread(new ThreadStart(game.GameLoop));
                gameLoop.Start();
            }
        }
    }
}
