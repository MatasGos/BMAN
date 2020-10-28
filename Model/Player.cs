using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Model
{
    public class Player : IPlayerObserver
    {
        public string pictureStructure { get; set; }
        public PlayerNum num { get; set; }      //Which player it is(changes spawn position and player image)
        public string id { get; set; }          //Player's id (equals to connection id)
        public string username { get; set; }
        public int x { get; set; }
        public int y { get; set; }
        public int speed { get; set; }          //Player's walking speed
        public int explosionPower { get; set; } //Player's bomb explosion power/radius
        public int health { get; set; }         //Player's health
        public int bombCount { get; set; }      //Number of bombs that the player can place at once
        public List<Boost> boosts { get; set; } //List of collected boosts

        //Variables to check where the player is moving to and what action he might be doing
        public int directionx { get; set; }
        public int directiony { get; set; }
        public string action { get; set; }

        public enum PlayerNum : int
        {
            P1 = 0,
            P2 = 1,
            P3 = 2,
            P4 = 3
        }

        public Player()
        {
            this.health = 2;
            this.bombCount = 1;
            this.action = "";
            this.speed = 3;
            this.explosionPower = 2;
            this.boosts = new List<Boost>();
            this.pictureStructure = "";      
        }

        public Player(string id, string username, int num) : this()
        {
            this.id = id;
            this.username = username;
            this.num = (PlayerNum)num;

            //DEFAULT VALUES
            //TODO: check which player it is and where to spawn him
            
            IPlayerStructure playerStructure = null;
            switch (this.num)
            {
                case PlayerNum.P1:
                    playerStructure = new PlayerShoesDecorator(new PlayerFedoraDecorator(new PlayerRed()));
                    this.x = 26;
                    this.y = 26;
                    break;
                case PlayerNum.P2:
                    playerStructure = new PlayerShoesDecorator(new PlayerFedoraDecorator(new PlayerBlue()));
                    this.x = 534;
                    this.y = 26;
                    break;
                case PlayerNum.P3:
                    playerStructure = new PlayerShoesDecorator(new PlayerFedoraDecorator(new PlayerGreen()));
                    this.x = 26;
                    this.y = 434;
                    break;
                case PlayerNum.P4:
                    playerStructure = new PlayerShoesDecorator(new PlayerFedoraDecorator(new PlayerYellow()));
                    this.x = 534;
                    this.y = 434;
                    break;
            }
            this.pictureStructure = playerStructure.GetPlayerStructure();
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

        public void update(IHubCallerClients context, string jsonMap, string jsonPlayers)
        {
            context.Client(id).SendAsync("SendData", jsonPlayers, jsonMap);
        }
    }
}
