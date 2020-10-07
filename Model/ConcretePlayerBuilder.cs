using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class ConcretePlayerBuilder : PlayerBuilder
    {
        private Player _player = new Player();

        public override void BuildId(string id)
        {
            _player.id = id;
        }

        public override void BuildUsername(string username)
        {
            _player.username = username;
        }

        public override void BuildNum(int num)
        {
            _player.num = (Player.PlayerNum)num;
            //TODO XY
            _player.x = 26;
            _player.y = 26;
        }

        public override Player GetPlayer()
        {
            return _player;
        }
    }
}
