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

        public void PerformPlayerActions(Player player, double time, ScoreboardTemplate scoreboard)
        {
            map.PlaceExplosive(player, time);
            player.Move();
            if (player.undoTimer < time)
            {
                player.canUndo = true;
            }
            if (player.actionSecondary == "undo")
            {
                if (player.canUndo)
                {
                    player.Undo();
                    player.undoTimer = time + 1000*60;
                    player.canUndo = false;
                }
                player.actionSecondary = "";
            }
            map.PickupBoost(player, time, scoreboard);
        }

        public void UpdateExplosives(double time)
        {
            map.UpdateExplosives(time);
        }

        public string GetJson(JsonSerializerSettings settings)
        {
            return map.GetJson(settings);
        }
        public MapFacade GetMapFacade()
        {
            return map.mapFacade;
        }

    }
}
