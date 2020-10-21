using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class MapAdapter : IMapAdapter
    {
        private Map map; //adaptee
        public MapAdapter(Map map)
        {
            this.map = map;
        }

        public void PerformPlayerActions(Player player, double time)
        {
            map.PlaceExplosive(player, time);
            map.Move(player);
            map.PickupBoost(player);
        }

        public void UpdateExplosives(double time)
        {
            map.UpdateExplosives(time);
        }

        public string GetJson(JsonSerializerSettings settings)
        {
            return map.GetJson(settings);
        }

    }
}
