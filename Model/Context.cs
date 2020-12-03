using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Model
{
    public class Context
    {
        IPlayerState currentState;
        private MovementControl movementControl;
        public Player player;

        public Context(MovementControl movement, Player player)
        {
            currentState = new InLobby();
            movementControl = movement;
            this.player = player;
        }

        public MovementControl GetMovement()
        {
            return movementControl;
        }

        public Player GetPlayer()
        {
            return player;
        }    

        public void SetState(IPlayerState state)
        {
            currentState = state;
        }

        public void Move()
        {
            currentState.Move(this);
        }
        public bool IsAlive()
        {
            return currentState.IsAlive(this);
        }
        public void Undo()
        {
            currentState.Undo(this);
        }
        public void ReduceHealth()
        {
            currentState.ReduceHealth(this);
        }
    }
}
