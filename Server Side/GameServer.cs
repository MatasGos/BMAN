using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Model;
using Newtonsoft.Json;
using Server.Hubs;

namespace Server
{
    public class GameServer
    {
        const int xSize = 23;       //Number of blocks left to right
        const int ySize = 19;       //Number of blocks top to bottom
        public Map map;
        public bool isRunning = false;

        private IHubCallerClients context;
        JsonSerializerSettings settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };

        public GameServer(IHubCallerClients _context)
        {
            context = _context;
            map = new Map(xSize, ySize);
            map.generateWalls();
        }

        public void GameLoop()
        {
            int tickRate = 30;
            double oneSecondInMs = 1000.0;
            double oneTickLength = oneSecondInMs / tickRate;

            isRunning = true;
            var sw = new Stopwatch();
            sw.Start();

            while (isRunning)
            {
                double startTime = sw.Elapsed.TotalMilliseconds;
                GameLogic();
                double elapsedTime = sw.Elapsed.TotalMilliseconds - startTime;
                if (elapsedTime < oneTickLength)
                {
                    //await Task.Delay((int)(oneTickLength - elapsedTime));
                    Thread.Sleep((int)(oneTickLength - elapsedTime));
                }
            }
        }
        public void GameLogic()
        {
            foreach(var player in Server.GetPlayers())
            {
                map.PlaceExplosive(player);
                map.Move(player);
            }

            string jsonMap = JsonConvert.SerializeObject(map, settings);
            string jsonPlayers = JsonConvert.SerializeObject(Server.GetPlayers(), settings);

            context.All.SendAsync("SendData", jsonPlayers, jsonMap);
        }

    }
}
