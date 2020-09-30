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

        public static void AddPlayer(string id, string username)
        {
            playerList.Add(new Player(id, username));
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

        public static void StartGame()
        {
            game = new GameServer(context);
            Thread gameLoop = new Thread(new ThreadStart(game.GameLoop));
            gameLoop.Start();
        }
    }
}
