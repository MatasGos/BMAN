using Microsoft.AspNetCore.SignalR;
using Newtonsoft.Json;
using System;
using System.Collections;
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

        public double invincibleUntil { get; set; }
        public int bombCount { get; set; }      //Number of bombs that the player can place at once
        //public List<Boost> boosts { get; set; } //List of collected boosts

        public bool hasSuperbombs;

        //Variables to check where the player is moving to and what action he might be doing
        public int directionx { get; set; }
        public int directiony { get; set; }
        public string action { get; set; }
        public string actionSecondary { get; set; }

        private MovementControl movementControl;

        public enum PlayerNum : int
        {
            P1 = 0,
            P2 = 1,
            P3 = 2,
            P4 = 3
        }

        public Player()
        {
            InitializeValues();
        }

        public Player(string id, string username, int num) : this()
        {
            this.id = id;
            this.username = username;
            this.num = (PlayerNum)num;

            //DEFAULT VALUES
            //TODO: check which player it is and where to spawn him

            InitializePlayerStructure();
        }

        public int[] getPos()
        {
            return new int[] { this.x, this.y };
        }

        public void UpdatePlayerStructure(string structure)
        {
            int num = (int)this.num;
            IPlayerStructure playerStructure = null;

            switch (this.num)
            {
                case PlayerNum.P1:
                    playerStructure = new PlayerRed();
                    break;
                case PlayerNum.P2:
                    playerStructure = new PlayerBlue();
                    break;
                case PlayerNum.P3:
                    playerStructure = new PlayerGreen();
                    break;
                case PlayerNum.P4:
                    playerStructure = new PlayerYellow();
                    break;
            }

            if (structure.Contains("f"))
            {
                playerStructure = new PlayerFedoraDecorator(playerStructure);
            }

            if (structure.Contains("s"))
            {
                playerStructure = new PlayerShoesDecorator(playerStructure);
            }
            this.pictureStructure = playerStructure.GetPlayerStructure();
        }

        public void SetPos(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public void SetAction(string action)
        {
            if(action == "undo")
            {
                this.actionSecondary = action;
            }
            else 
            {
                this.action = action;
            }          
        }

        public void update(IHubCallerClients context, string jsonMap, string jsonPlayers, string jsonScoreboard, int roundEnded)
        {
            context.Client(id).SendCoreAsync("SendData", new object[] { jsonPlayers, jsonMap, health, jsonScoreboard, roundEnded });
            //context.Client(id).SendAsync("SendData", jsonPlayers, jsonMap, health);
        }
        public void Move()
        {
            this.movementControl.Move();
        }
        public void Undo()
        {
            this.movementControl.Undo();
        }
        public void SetCommand(ICommand command)
        {
            this.movementControl.AddCommand(command);
        }

        public void ClearCommandHistory()
        {
            this.movementControl.Clear();
        }

        public void ReduceHealth()
        {
            if (health > 0)
            {
                this.health -= 1;
            }    
            else
            {
                x = -25;
                y = -25;
            }
        }

        public void BecomeInvincible(double time)
        {
            this.invincibleUntil = time + 1000.0;
        }

        public bool IsAlive()
        {
            return health > 0;
        }
        public void ResetPlayer()
        {
            InitializeValues();
            InitializePlayerStructure();
        }

        private void InitializeValues()
        {
            this.health = 5;
            this.bombCount = 1;
            this.action = "";
            this.speed = 3;
            this.explosionPower = 2;
            this.pictureStructure = "";
            this.movementControl = new MovementControl();
            this.hasSuperbombs = true;
            this.invincibleUntil = 0;
        }
        private void InitializePlayerStructure()
        {
            IPlayerStructure playerStructure = null;
            switch (this.num)
            {
                case PlayerNum.P1:
                    playerStructure = new PlayerRed();
                    this.x = 26;
                    this.y = 26;
                    break;
                case PlayerNum.P2:
                    playerStructure = new PlayerBlue();
                    this.x = 534;
                    this.y = 26;
                    break;
                case PlayerNum.P3:
                    playerStructure = new PlayerGreen();
                    this.x = 26;
                    this.y = 434;
                    break;
                case PlayerNum.P4:
                    playerStructure = new PlayerYellow();
                    this.x = 534;
                    this.y = 434;
                    break;
            }
            this.pictureStructure = playerStructure.GetPlayerStructure();
        }
    }
}
