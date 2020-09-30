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
    public class Game
    {
        const int xSize = 23;       //Number of blocks left to right
        const int ySize = 19;       //Number of blocks top to bottom
        bool isRunning = false;

        private IHubCallerClients context;
        JsonSerializerSettings settings = new JsonSerializerSettings
        {
            TypeNameHandling = TypeNameHandling.All
        };

        public Game(IHubCallerClients _context)
        {
            context = _context;
            Map map = new Map(xSize, ySize);
        }

        public void GameLoop()
        {
            int tickRate = 30;
            double oneSecondInMs = 1000.0;
            double oneTickLength = oneSecondInMs / tickRate;

            //await Clients.All.SendAsync("StartedMessage");
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
            string json = JsonConvert.SerializeObject(map, settings);
            Debug.WriteLine(json);
            Map map2 = JsonConvert.DeserializeObject<Map>(json, settings);
            context.All.SendAsync("SendData")
        }
    }
}
