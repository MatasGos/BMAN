using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class UnitBlockFactory
    {
        public Unit CreateBlock(string type, int x, int y)
        {
            Unit toReturn = null;
            switch(type)
            {
                case "Wall":
                    toReturn = new Wall(x, y);
                    break;
                case "Box":
                    toReturn = new Box(x, y);
                    break;
                case "Boost":
                    throw new NotImplementedException();
                    break;
                case "Teleporter":
                    throw new NotImplementedException();
                    break;
                default:
                    break;
            }
            return toReturn;
        }
    }
}
