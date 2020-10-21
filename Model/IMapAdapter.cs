using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public interface IMapAdapter
    {
        public void PerformPlayerActions(Player player, double time);

        public void UpdateExplosives(double time);

        public string GetJson(JsonSerializerSettings settings);
    }
}
