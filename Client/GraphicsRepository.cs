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
