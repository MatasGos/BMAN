using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;

namespace Client
{
    public class PictureFlyweight<A, B>
    {
        private IDictionary<string, B[,]> pictures;
        private GraphicsRepository<A, B> graphics;
        private int sizex;
        private int sizey;
        public PictureFlyweight()
        {
            if (typeof(A) != typeof(Bitmap) || typeof(B) != typeof(Color))
            {
                throw new NotImplementedException();
            }
            pictures = new Dictionary<string, B[,]>();
            graphics = new GraphicsRepository<A, B>();
        }

        public B[,] GetPictureArray(string type)
        {
            B[,] picToReturn = null;
            if (pictures.ContainsKey(type))
            {
                picToReturn = pictures[type];
            }
            else            
            {
                A picObject = GetImage(type);
                Console.WriteLine(sizex + " " + sizey);
                GraphicsAdapter<A, B> pic = graphics.CreateGraphicsObject(sizex, sizey);
                pic.SetImage(picObject);
                picToReturn = pic.GetColorArray();
                pictures.Add(type, picToReturn);
            }
            return picToReturn;
        }

        private A GetImage(string name)
        {
            A toReturn = default(A);
            switch (name)
            {
                case "wall":
                    toReturn = (A)(object)Images.wall;
                    sizex = 25;
                    sizey = 25;
                    break;
                case "p1":
                    toReturn = (A)(object)Images.p1;
                    sizex = 15;
                    sizey = 15;
                    break;
                case "box":
                    toReturn = (A)(object)Images.box;
                    sizex = 25;
                    sizey = 25;
                    break;
                case "bomb":
                    toReturn = (A)(object)Images.bomb;
                    sizex = 25;
                    sizey = 25;
                    break;
                case "mine":
                    toReturn = (A)(object)Images.mine;
                    sizex = 25;
                    sizey = 25;
                    break;
                case "boost":
                    toReturn = (A)(object)Images.boost;
                    sizex = 25;
                    sizey = 25;
                    break;
                case "superbomb":
                    toReturn = (A)(object)Images.superbomb;
                    sizex = 25;
                    sizey = 25;
                    break;
                case "supermine":
                    toReturn = (A)(object)Images.supermine;
                    sizex = 25;
                    sizey = 25;
                    break;
                case "explosion":
                    toReturn = (A)(object)Images.explosion;
                    sizex = 25;
                    sizey = 25;
                    break;
                case "fedora":
                    toReturn = (A)(object)Images.fedora;
                    sizex = 15;
                    sizey = 15;
                    break;
                case "shoes":
                    toReturn = (A)(object)Images.shoes;
                    sizex = 15;
                    sizey = 15;
                    break;
                case "superexplosion":
                    toReturn = (A)(object)Images.superexplosion;
                    sizex = 25;
                    sizey = 25;
                    break;
                case "teleporterin":
                    toReturn = (A)(object)Images.teleporterin;
                    sizex = 25;
                    sizey = 25;
                    break;
                case "teleporterout":
                    toReturn = (A)(object)Images.teleporterout;
                    sizex = 25;
                    sizey = 25;
                    break;
                default:
                    throw new NotImplementedException();
            }
            return toReturn;
        }
    }
}
