using System;
using System.Collections.Generic;
using System.Text;

namespace prototype.Classes
{
    public class Bomb
    {
        private Player player;
        private int strength;
        private int x, y;
        private int timeleft;
        const int explosionTime = 200;
        private bool walkThrough;
        private int ticks;
        private int objectAfter = 50;
        public Bomb(Player player,int[] xy)
        {
            this.player = player;
            this.timeleft = player.bombTime();
            this.x = xy[0];
            this.y = xy[1];
            this.strength = player.getPower();
            ticks = 0;
            walkThrough = true;
        }
        public Bomb(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public int tick()
        {  
            if (timeleft <= 0) {
            }
            if (ticks++ > objectAfter)
            {
                walkThrough = false;
            }
            this.timeleft--;
            return timeleft+1;
        }
        public int[] getPos()
        {
            return new int[] { this.x, this.y };
        }
        public int getPower()
        {
            return strength;
        }
        public bool getWT()
        {
            return walkThrough;
        }
    }
}
