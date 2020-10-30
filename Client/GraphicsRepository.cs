using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Client
{
    //A - image (for example Bitmap)
    public class GraphicsRepository<A, B>
    {
        public A GetImage(string name)
        {
            if (typeof(A) != typeof(Bitmap))
            {
                throw new NotImplementedException();
            }
            A toReturn = default(A);
            switch (name)
            {
                case "wall":
                    toReturn = (A)(object)Images.wall;
                    break;
                case "p1":
                    toReturn = (A)(object)Images.p1;
                    break;
                case "box":
                    toReturn = (A)(object)Images.box;
                    break;
                case "bomb":
                    toReturn = (A)(object)Images.bomb;
                    break;
                case "mine":
                    toReturn = (A)(object)Images.mine;
                    break;
                case "boost":
                    toReturn = (A)(object)Images.boost;
                    break;
                case "superbomb":
                    toReturn = (A)(object)Images.superbomb;
                    break;
                case "supermine":
                    toReturn = (A)(object)Images.supermine;
                    break;
                case "explosion":
                    toReturn = (A)(object)Images.explosion;
                    break;
                case "fedora":
                    toReturn = (A)(object)Images.fedora;
                    break;
                case "shoes":
                    toReturn = (A)(object)Images.shoes;
                    break;
                case "superexplosion":
                    toReturn = (A)(object)Images.superexplosion;
                    break;
                case "teleporterin":
                    toReturn = (A)(object)Images.teleporterin;
                    break;
                case "teleporterout":
                    toReturn = (A)(object)Images.teleporterout;
                    break;
                default:
                    throw new NotImplementedException();
            }
            return toReturn;
        }

        public B GetBackgroundColor()
        {
            if (typeof(B) != typeof(Color))
            {
                throw new NotImplementedException();
            }
            return (B)(object)Color.BurlyWood;
        }

        public GraphicsAdapter<A, B> CreateGraphicsObject(int x, int y)
        {
            if (typeof(A) != typeof(Bitmap) || typeof(B) != typeof(Color))
            {
                throw new NotImplementedException();
            }
            return (GraphicsAdapter<A, B>)new BitmapConcreteAdapter(x, y);
        }

        public B ColorFromArgb(int a, int r, int g, int b)
        {
            if (typeof(A) != typeof(Bitmap) || typeof(B) != typeof(Color))
            {
                throw new NotImplementedException();
            }
            return (B)(object)Color.FromArgb(a, r, g, b);
        }

        public int ColorA(B color)
        {
            if (typeof(B) != typeof(Color))
            {
                throw new NotImplementedException();
            }
            return ((Color)(object)color).A;
        }

        public int ColorR(B color)
        {
            if (typeof(B) != typeof(Color))
            {
                throw new NotImplementedException();
            }
            return ((Color)(object)color).R;
        }

        public int ColorG(B color)
        {
            if (typeof(B) != typeof(Color))
            {
                throw new NotImplementedException();
            }
            return ((Color)(object)color).G;
        }

        public int ColorB(B color)
        {
            if (typeof(B) != typeof(Color))
            {
                throw new NotImplementedException();
            }
            return ((Color)(object)color).B;
        }

    }
}
