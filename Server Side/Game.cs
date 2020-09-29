using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Server.Hubs;

namespace Server
{
    public class Game
    {
        bool isRunning = false;
        Random r = new Random();
        private readonly IHubCallerClients context;

        public Game(IHubCallerClients _context)
        {
            context = _context;
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

            int tick = 1;
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
                //Console.WriteLine(tick + "zzzzz " + (int)sw.Elapsed.TotalMilliseconds);
                tick++;
            }
        }
        public void GameLogic()
        {
            //FORM PLAYER DATA TO BE SENT
            List<string> playerInfo = new List<string>();
            foreach (var player in Server.GetPlayers())
            {
                string str = player.getString();
                playerInfo.Add(str);
            }

            //SEND A LIST OF STRINGS THAT CONTAIN PLAYER INITIAL INFORMATION
            context.All.SendAsync("InitializePlayers", playerInfo);

            //Console.WriteLine();

        }
    }
}
