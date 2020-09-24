using Microsoft.AspNetCore.SignalR;
using prototype.Classes;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Server.Hubs
{
    public class GameHub : Hub
    {

        int startSpeed = 5;
        int[] xysize = { 20, 20 };
        //Sends a message to everyone
        public async Task SendMessage(string username, string message)
        {
            Console.WriteLine(username + ": " + message);
            await Clients.All.SendAsync("ReceiveMessage", username, message);
        }
        public async Task Move(int x, int y)
        {
            Console.WriteLine(x + " " + y);
            Server.MovePlayer(Context.ConnectionId, x , y);
        }

        //Logs the player into the game, saves his information and sends a login message to everyone else
        public async Task LoginMessage(string username)
        {
            //TODO: Check if the game has already started and dont allow to connect
            //TODO: Check if more than 4 players are trying to connect
            Server.AddPlayer(startSpeed, xysize, username, Context.ConnectionId);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(username + " has logged in.");
            Console.ResetColor();

            await Clients.Others.SendAsync("LoggedinMessage", username, Server.map);
        }

        //Sends a message to start the game to everyone after someone presses the start button
        public async Task StartMessage()
        {
            Server.context = this.Clients;
            Server.StartGame();
        }

        public async Task SendInfo()
        {
            
        }

    }
}
