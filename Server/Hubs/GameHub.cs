using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Model;
using Newtonsoft.Json;

namespace Server.Hubs
{
    public class GameHub : Hub
    {
        JsonSerializerSettings settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
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
            await Task.Run(() =>
            {
                Server.context = this.Clients;  //Sets the context to send messages to clients not only from the hub
                Server.StartGame();
            });
        }

        //Sends a move message
        /*public async Task SendMoveMessage(int x, int y)
        {
            await Task.Run(() =>
            {
                Server.GetPlayerById(Context.ConnectionId).directionx = x;
                Server.GetPlayerById(Context.ConnectionId).directiony = y;
            });
        }*/
        public async Task SendMoveMessage(string moveCommand)
        {
            await Task.Run(() =>
            {
                ICommand command = null;
                Player p = Server.GetPlayerById(Context.ConnectionId);
                switch (moveCommand)
                {
                    case "moveleft":
                        command = new MoveLeft(p, Server.current);
                        break;
                    case "moveright":
                        command = new MoveRight(p, Server.current);
                        break;
                    case "moveup":
                        command = new MoveUp(p, Server.current);
                        break;
                    case "movedown":
                        command = new MoveDown(p, Server.current);
                        break;
                    case "moveleftup":
                        command = new MoveLeftUp(p, Server.current);
                        break;
                    case "moveleftdown":
                        command = new MoveLeftDown(p, Server.current);
                        break;
                    case "moverightup":
                        command = new MoveRightUp(p, Server.current);
                        break;
                    case "moverightdown":
                        command = new MoveRightDown(p, Server.current);
                        break;
                }
                p.SetCommand(command);
                //Server.GetPlayerById(Context.ConnectionId).movementControl = new MovementControl(command);
                //Server.GetPlayerById(Context.ConnectionId).movementControl.Move();
            });
        }

        //Sends a place bomb message
        public async Task SendActionMessage(string action)
        {
            await Task.Run(() =>
            {
                Server.GetPlayerById(Context.ConnectionId).SetAction(action);
            });
        }
    }
}