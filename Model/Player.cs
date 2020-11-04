﻿using Microsoft.AspNetCore.SignalR;
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
        //public List<Boost> boosts { get; set; } //List of collected boosts

        public bool hasSuperbombs;

        //Variables to check where the player is moving to and what action he might be doing
        public int directionx { get; set; }
        public int directiony { get; set; }
        public string action { get; set; }
        public string actiontwo { get; set; }

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
            this.health = 2;
            this.bombCount = 1;
            this.action = "";
            this.speed = 3;
            this.explosionPower = 2;
            this.pictureStructure = "";
            this.movementControl = new MovementControl();
            this.hasSuperbombs = true;
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
                    playerStructure = new PlayerRed();
                    this.x = 26;
                    this.y = 26;
                    break;
                case PlayerNum.P2:
                    playerStructure = new PlayerShoesDecorator(new PlayerFedoraDecorator(new PlayerBlue()));
                    this.x = 534;
                    this.y = 26;
                    break;
                case PlayerNum.P3:
                    playerStructure = new PlayerGreen();
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

        public void SetPos(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public void SetAction(string action)
        {
            if(action == "undo")
            {
                this.actiontwo = action;
            }
            else 
            {
                this.action = action;
            }          
        }

        public void update(IHubCallerClients context, string jsonMap, string jsonPlayers)
        {
            context.Client(id).SendAsync("SendData", jsonPlayers, jsonMap);
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
    }
}
