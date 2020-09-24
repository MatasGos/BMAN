using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using prototype.Classes;
using Server.Hubs;

namespace Server
{
    public static class Server
    {
        static List<Player> playerList = new List<Player>();
        public static IHubCallerClients context;
        public static Map map;
        public static void AddPlayer(int speed, int[] xysize, string username, string id)
        {
            playerList.Add(new Player(playerList.Count+1, speed, xysize, username, id));
        }
        public static List<Player> GetPlayers()
        {
            return playerList;
        }
        public static void StartGame()
        {
            map = new Map(20, 20, new int[] { 98, 65, 8 }, 0);
            context.All.SendAsync("ReceiveMap", map);
            Game game = new Game(context);
            Thread gameLoop = new Thread(new ThreadStart(game.GameLoop));

            gameLoop.Start();
        }
        public static void MovePlayer(string id, int x, int y)
        {
            playerList = map.Move(id, x, y, playerList);
        }
    }
}
