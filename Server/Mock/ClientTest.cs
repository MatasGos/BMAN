using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Model;
using Newtonsoft.Json;

namespace Server
{
    //Mock class for testing
    public class ClientTest : IClientProxy
    {
        public Player client { get; }
        JsonSerializerSettings settings = new JsonSerializerSettings { TypeNameHandling = TypeNameHandling.All };
        public ClientTest(Player player)
        {
            client = player;
        }
        public Task SendCoreAsync(string method, object[] args, CancellationToken cancellationToken = default)
        {
            return Task.Run(() =>
            {
                if (args.Length != 3)
                {
                    throw new NotImplementedException();
                }
                if (!(args[0] is string) || !(args[1] is string) || !(args[2] is int) )
                {
                    throw new NotImplementedException();
                }
                List<Player> players = JsonConvert.DeserializeObject<List<Player>>((string)args[0], settings);
                Map map = JsonConvert.DeserializeObject<Map>((string)args[1], settings);
            });            
        }

    }
}
