using System;
using System.Drawing;
using Model;
using Client;

namespace Demo
{
    class Program
    {
        static void Main(string[] args)
        {

        }

        public static void FactoryDemo()
        {
            Factory factory = new BlockFactory();
            Block wall = factory.CreateBlock("wall",0, 0);
            Block box = factory.CreateBlock("wall", 0, 0);
            Block teleporter = factory.CreateBlock("teleporter", 0, 0);
        }

        public static void AbstractFactoryDemo()
        {
            bool super = false;
            ExplosiveAbstractFactory abstractFactory;
            switch (super)
            {
                case false:
                    abstractFactory = new RegularExplosiveConcreteFactory();
                    break;
                case true:
                    abstractFactory = new SuperExplosiveConcreteFactory();
                    break;
            }
            Explosive bomb = abstractFactory.CreateBomb(0, 0, 100, 0);
            Explosive mine = abstractFactory.CreateMine(0, 0);
            Explosive explosion = abstractFactory.CreateExplosion(0, 0, 0);
        }

        public static void PrototypeDemo()
        {
            Map mapOriginal = new Map(23, 19);
            Map mapDeepClone = mapOriginal.Clone(true);
            Map mapShallowClone = mapOriginal.Clone(false);
            Console.WriteLine("Deep kopijos palyginimas:");
            Console.WriteLine("mapOriginal.units == mapDeepClone.units : " + (mapOriginal.units == mapDeepClone.units));
            Console.WriteLine("mapOriginal.explosions == mapDeepClone.explosions : " + (mapOriginal.explosions == mapDeepClone.explosions));

            Console.WriteLine("Shallow kopijos palyginimas:");
            Console.WriteLine("mapOriginal.units == mapDeepClone.units : " + (mapOriginal.units == mapShallowClone.units));
            Console.WriteLine("mapOriginal.explosions == mapDeepClone.explosions : " + (mapOriginal.explosions == mapShallowClone.explosions));
        }

        public static void AdapterDemo()
        {
            GraphicsAdapter<Bitmap, Color> graphics = new BitmapConcreteAdapter(5, 5);
            graphics.LockBits();
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    graphics.SetPixel(i, j, Color.Blue);
                }
            }
            graphics.UnlockBits();
            Color[,] pixels = graphics.GetColorArray();
            for (int i = 0; i < 5; i++)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (j != 4)
                        Console.Write(pixels[i, j] + " | ");
                    else
                        Console.WriteLine(pixels[i, j]);
                }
            }
        }
    }
}
