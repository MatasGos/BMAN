using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace prototype.Classes
{
    class Bomb
    {
        private Player player;
        private int strength;
        private int x, y;
        private int timeleft;
        const int explosionTime = 200;
        bool delete;
        public Bomb(Player player,int[] xy)
        {
            this.player = player;
            this.timeleft = player.bombTime();
            bool delete = false;
            this.x = xy[0];
            this.y = xy[1];
            this.strength = player.getPower();
        }
        public Bomb(int x, int y)
        {
            this.x = x;
            this.y = y;
        }
        public int tick()
        {  
            if (timeleft <= 0) {
                bool delete = true;
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
        public override bool Equals(object obj)
        {
            var item = obj as Bomb;

            if (item == null)
            {
                return false;
            }

            return this.x.Equals(item.x) && this.y.Equals(item.y);
        }
    }
}
