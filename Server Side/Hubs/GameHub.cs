using Microsoft.AspNetCore.SignalR;
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
        //Logs the player into the game, saves his information and sends a login message to everyone else
        public async Task SendLoginMessage(string username)
        {
            //TODO: Check if the game has already started and dont allow to connect
            //TODO: Check if more than 4 players are trying to connect
            Server.AddPlayer(Context.ConnectionId, username);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(username + " has logged in.");
            Console.ResetColor();

            await Clients.Others.SendAsync("ReceiveLoginMessage", username);
        }

        //Sends a message to everyone
        public async Task SendMessage(string username, string message)
        {
            Console.WriteLine(username + ": " + message);
            await Clients.All.SendAsync("ReceiveMessage", username, message);
        }

        //Sends a message to start the game to everyone after someone presses the start button
        public async Task StartMessage()
        {
            Server.context = this.Clients;  //Sets the context to send messages to clients not only from the hub
            Server.StartGame();
        }

        //Sends a move message
        public async Task SendMoveMessage(int x, int y)
        {
            await Task.Run(() =>
            {
                Server.GetPlayerById(Context.ConnectionId).directionx = x;
                Server.GetPlayerById(Context.ConnectionId).directiony = y;
            });
        }

        //Sends a place bomb message
        public async Task SendPlaceBombMessage()
        {
            await Task.Run(() =>
            {
                Server.GetPlayerById(Context.ConnectionId).placeBomb = true;
            });
        }
    }
}