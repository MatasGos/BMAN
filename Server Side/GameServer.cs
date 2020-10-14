using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
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
        Stopwatch sw;

        private IHubCallerClients context;
        JsonSerializerSettings settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };


        /*PlayerBuilder builder = new ConcretePlayerBuilder();
        builder.BuildId(id);
            builder.BuildUsername(username);
            builder.BuildNum(playerList.Count);
            Player player = builder.GetPlayer();
        playerList.Add(player);*/
        public GameServer(IHubCallerClients _context)
        {
            context = _context;

            Random rand = new Random();
            int r = rand.Next(100);
            if (r < 0)
            {
                MapBuilder concreteBuilder = new ConcreteMapBuilder();
                MapDirector mapDirector = new MapDirector(concreteBuilder);

                mapDirector.constructMap();
                map = mapDirector.getMap();
            }
            else
            {
                MapBuilder concreteBuilder = new DefaultMapBuilder();
                MapDirector mapDirector = new MapDirector(concreteBuilder);

                mapDirector.constructMap();
                map = mapDirector.getMap();
            }
        }

        public void GameLoop()
        {
            int tickRate = 30;
            double oneSecondInMs = 1000.0;
            double oneTickLength = oneSecondInMs / tickRate;

            isRunning = true;
            sw = new Stopwatch();
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
                map.PlaceExplosive(player, sw.Elapsed.TotalMilliseconds);
                map.Move(player);
                map.PickupBoost(player);
            }
            map.UpdateExplosives(sw.Elapsed.TotalMilliseconds);

            //TODO: Make copies of map and players as it sometimes crashes
            string jsonMap = JsonConvert.SerializeObject(map, settings);
            string jsonPlayers = JsonConvert.SerializeObject(Server.GetPlayers(), settings);

            context.All.SendAsync("SendData", jsonPlayers, jsonMap);
        }

    }
}
