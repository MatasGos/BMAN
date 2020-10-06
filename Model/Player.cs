using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Model
{
    public class Player
    {
        public string id { get; set; }
        public string username { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public int speed { get; set; }
        public int explosionPower { get; set; }
        public int directionx { get; set; }
        public int directiony { get; set; }
        public string action { get; set; }
        public List<Boost> boosts { get; set; }

        public Player(string id, string username)
        {
            this.action = "";
            this.id = id;
            this.username = username;
            //DEFAULT VALUES
            this.x = 26;
            this.y = 26;
            this.speed = 3;
            this.explosionPower = 3;
            boosts = new List<Boost>();
        }

        public int[] getPos()
        {
            return new int[] { this.x, this.y };
        }

        public void SetDirection(int px, int py)
        {
            directionx = px;
            directiony = py;
        }

        public void SetAction(string action)
        {
            this.action = action;
        }

        public void AddBoost(Boost boost)
        {
            bool canAdd = true;
            foreach (var x in boosts)
            {
                if (x.boostType.Equals(boost.boostType))
                {
                    canAdd = false;
                    break;
                }
            }
            boosts.Add(boost);
        }
        public void RemoveBoost(Boost boost)
        {
            boosts.Remove(boost);
        }
        public bool HasBoost(string boostType)
        {
            foreach (var x in boosts)
            {
                if (x.boostType.Equals(boostType))
                {
                    return true;
                }
            }
            return false;
        }

        public void RemoveBoostDebug(string boostType)
        {
            int index = 0;
            foreach (var x in boosts)
            {
                if (x.boostType.Equals(boostType))
                {
                    break;
                }
                index++;
            }
            if (index < boosts.Count)
            {
                boosts.RemoveAt(index);
            }
        }
    }
}
