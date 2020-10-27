using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;
using System.Threading.Tasks;
using System.Threading;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using Newtonsoft.Json;

namespace Model
{
    public class Map : ICloneable<Map>
    {
        //Size of the map in blocks e.g 15x15
        public string mapName { get; set; }
        public int xSize { get; set; }
        public int ySize { get; set; }
        public Unit[,] units { get; set; }
        public Boost[,] boosts { get; set; }
        public MapFacade mapFacade { get; set; }

        public Map(int xSize, int ySize)
        {
            this.xSize = xSize;
            this.ySize = ySize;
            units = new Unit[xSize, ySize];
            boosts = new Boost[xSize, ySize];
            mapFacade = new MapFacade(xSize, ySize, units, boosts);
        }

        public Unit[,] getUnits()
        {
            return units;
        }

        public Boost[,] getBoosts()
        {
            return boosts;
        }

        public void Move(Player movingPlayer)
        {
            mapFacade.Move(movingPlayer);
        }

        public void PlaceExplosive(Player player, double placeTime)
        {
            mapFacade.PlaceExplosive(player, placeTime);
        }

        public void PickupBoost(Player player)
        {
            mapFacade.PickupBoost(player);
        }

        public void UpdateExplosives(double time)
        {
            mapFacade.UpdateExplosives(time);
        }

        public string GetJson(JsonSerializerSettings settings)
        {
            return JsonConvert.SerializeObject(this, settings);
        }

        public Map Clone(bool deepCopy)
        {
            Map clone = (Map)this.MemberwiseClone();
            if (deepCopy == false)
            {
                return clone;
            }
            clone.units = new Unit[xSize, ySize];
            clone.boosts = new Boost[xSize, ySize];
            for (int i = 0; i < xSize; i++)
            {
                for (int j = 0; j < ySize; j++)
                {
                    clone.units[i, j] = units[i, j];
                    clone.boosts[i, j] = boosts[i, j];
                }
            }
            return clone;
        }
    }
}

