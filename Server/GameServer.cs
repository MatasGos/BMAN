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

        public IMapAdapter map;
        public bool isRunning = false;
        Stopwatch sw;
        public List<IPlayerObserver> playerList = new List<IPlayerObserver>();
        private IHubCallerClients context;       

        JsonSerializerSettings settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
        
        public GameServer(IHubCallerClients _context)
        {
            context = _context;

            Random rand = new Random();
            int r = rand.Next(100);
            MapDirector mapDirector;
            if (r < 30)
            {
                mapDirector = new MapDirector(new ConcreteMapBuilder());
            }
            else if(r < 60)
            {
                mapDirector = new MapDirector(new DefaultMapBuilder());
            }
            else
            {
                mapDirector = new MapDirector(new DefaultMapBuilder());                
            }

            Map mapFromCache = Server.GetMapByName(mapDirector.getMap().mapName);
            //If the map was already generated, clone it
            //If the map was not generated before, generate it and put a copy of it in cache
            if (mapFromCache != null)
            {
                map = new MapAdapter(mapFromCache);
            }
            else
            {
                mapDirector.constructMap();
                Map temp = mapDirector.getMap();
                map = new MapAdapter(temp);
                Server.AddMap(temp);
            }
            Server.current = map.GetMapFacade(); 
        }

        public void AddObserver(IPlayerObserver player)
        {
            playerList.Add(player);
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
            foreach(Player player in playerList)
            {
                map.PerformPlayerActions(player, sw.Elapsed.TotalMilliseconds);
                
            }
            map.UpdateExplosives(sw.Elapsed.TotalMilliseconds);

            //TODO: Make copies of map and players as it sometimes crashes
            string jsonMap = map.GetJson(settings);
            string jsonPlayers = JsonConvert.SerializeObject(Server.GetPlayers(), settings);

            foreach(var player in playerList)
            {
                player.update(context, jsonMap, jsonPlayers);
            }
            //context.All.SendAsync("SendData", jsonPlayers, jsonMap);
        }
    }
}
