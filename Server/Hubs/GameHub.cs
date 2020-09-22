using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Server.Hubs
{
    public class GameHub : Hub
    {
        public async Task Move(int id, int x, int y)
        {
            
        }
        public async Task SendMessage(int user, string message)
        {
            Console.WriteLine(user + ": " + message);
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public override Task OnConnectedAsync()
        {
            Console.WriteLine("user connected");
            return base.OnConnectedAsync();
        }
    }
}
