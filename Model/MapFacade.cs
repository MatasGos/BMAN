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

        public MapFacade(int xSize, int ySize, Unit[,] units, Explosive[,] explosions)
        {
            pcm = new PlayerControlManager(xSize, ySize, units, explosions);
            em = new ExplosionManager(xSize, ySize, units, explosions);
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
            pcm.ActivateBlock(player);
        }

        public void UpdateExplosives(double time)
        {
            em.UpdateExplosives(time);
        }
    }
}
