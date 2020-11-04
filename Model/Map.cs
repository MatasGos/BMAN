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
        public Explosive[,] explosions { get; set; }
        public MapFacade mapFacade { get; set; }

        public Map(int xSize, int ySize)
        {
            this.xSize = xSize;
            this.ySize = ySize;
            units = new Unit[xSize, ySize];
            explosions = new Explosive[xSize, ySize];
            mapFacade = new MapFacade(xSize, ySize, units, explosions);
        }

        public Unit[,] getUnits()
        {
            return units;
        }

        public Explosive[,] getExplosions()
        {
            return explosions;
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
            clone.explosions = new Explosive[xSize, ySize];
            for (int i = 0; i < xSize; i++)
            {
                for (int j = 0; j < ySize; j++)
                {
                    clone.units[i, j] = units[i, j];
                    clone.explosions[i, j] = explosions[i, j];
                }
            }
            mapFacade = new MapFacade(xSize, ySize, units, explosions);
            return clone;
        }
    }
}

