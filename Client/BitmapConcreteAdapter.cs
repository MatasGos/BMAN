using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Text;

namespace Client
{
    public class BitmapConcreteAdapter : GraphicsAdapter<Bitmap, Color>
    {
        Bitmap image;                   //Bitmap images used in showing the map on screen
        IntPtr Iptr = IntPtr.Zero;      //Pointer for bitmap data copying from memory
        BitmapData bitmapData;          //Map bitmap data construct with properties
        byte[] pixels;                  //Map pixel data array in bytes
        int depth;                      //How many bits per pixel are there in the map image
        public BitmapConcreteAdapter(int x, int y)
        {
            image = new Bitmap(x, y);
        }
        public Bitmap GetImageCopy()
        {
            RectangleF cloneRectangle = new RectangleF(0, 0, image.Width, image.Height);
            PixelFormat format = image.PixelFormat;
            return image.Clone(cloneRectangle, format);
        }
        public Bitmap GetImage()
        {
            return image;
        }
        public void LockBits()
        {
            try
            {
                //Total pixel count in map
                int pixelCount = image.Width * image.Height;
                //Rectangle for locking
                Rectangle rectangle = new Rectangle(0, 0, image.Width, image.Height);

                //Get source bitmap pixel format size
                depth = Bitmap.GetPixelFormatSize(image.PixelFormat);

                //Check if bpp (Bits Per Pixel) is 8, 24, or 32
                if (depth != 8 && depth != 24 && depth != 32)
                {
                    throw new Exception("Only 8, 24 and 32 bpp images are supported.");
                }

                //Lock bitmap and return bitmap data
                bitmapData = image.LockBits(rectangle, ImageLockMode.ReadWrite, image.PixelFormat);

                //Create byte array to copy pixel values
                int step = depth / 8;
                pixels = new byte[pixelCount * step];   //Galima iskelti kad kiekviena tick'a nekurtu naujo array/ jeigu lagins
                Iptr = bitmapData.Scan0;

                //Copy data from pointer to array
                Marshal.Copy(Iptr, pixels, 0, pixels.Length);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void UnlockBits()
        {
            try
            {
                //Copy data from byte array to a pointer
                Marshal.Copy(pixels, 0, Iptr, pixels.Length);

                //Unlock bitmap data
                image.UnlockBits(bitmapData);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public Color[,] GetColorArray()
        {
            Color[,] destination = new Color[image.Width, image.Height];
            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    destination[x, y] = image.GetPixel(x, y);
                }
            }
            return destination;
        }

        public void SetPixel(int x, int y, Color color)
        {
            //Get color components count
            int cCount = depth / 8;

            //Get start index of the specified pixel
            int i = ((y * image.Width) + x) * cCount;

            if (depth == 32) //For 32 bpp set Red, Green, Blue and Alpha
            {
                pixels[i] = color.B;
                pixels[i + 1] = color.G;
                pixels[i + 2] = color.R;
                pixels[i + 3] = color.A;
            }
            if (depth == 24) //For 24 bpp set Red, Green and Blue
            {
                pixels[i] = color.B;
                pixels[i + 1] = color.G;
                pixels[i + 2] = color.R;
            }
            if (depth == 8) //For 8 bpp set color value (Red, Green and Blue values are the same)
            {
                pixels[i] = color.B;
            }
        }
        public int GetWidth()
        {
            return image.Width;
        }
        public int GetHeight()
        {
            return image.Height;
        }
        public void SetImage(Bitmap image)
        {
            this.image = image;
        }
    }
}
