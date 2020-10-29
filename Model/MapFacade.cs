using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Model
{
    public class MapFacade
    {
        PlayerControlManager pcm;
        ExplosionManager em;

        public MapFacade(int xSize, int ySize, Unit[,] units, Boost[,] boosts)
        {
            pcm = new PlayerControlManager(xSize, ySize, units, boosts);
            em = new ExplosionManager(xSize, ySize, units, boosts);
        }

        public void Move(Player movingPlayer, int x, int y, int speed)
        {
            pcm.Move(movingPlayer, x, y, speed);
        }

        public void PlaceExplosive(Player player, double placeTime)
        {
            pcm.PlaceExplosive(player, placeTime);
        }

        public void PickupBoost(Player player)
        {
            pcm.PickupBoost(player);
        }

        public void UpdateExplosives(double time)
        {
            em.UpdateExplosives(time);
        }
    }
}
