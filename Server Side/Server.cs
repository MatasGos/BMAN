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
        public static List<Player> playerList = new List<Player>();    
        public static IHubCallerClients context;
        public static GameServer game;
        public static List<Map> mapCache = new List<Map>();

        public static void AddPlayer(string id, string username)
        {
            playerList.Add(new Player(id, username, playerList.Count));
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

        public static void AddMap(Map map)
        {
            mapCache.Add(map);
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
            return toReturn;
        }

        public static void StartGame()
        {
            if (game == null)
            {
                game = new GameServer(context);
                foreach(var player in playerList)
                {
                    game.AddPlayer(player);
                }
                Thread gameLoop = new Thread(new ThreadStart(game.GameLoop));
                gameLoop.Start();
            }
        }
    }
}
