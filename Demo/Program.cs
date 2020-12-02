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
            //FactoryDemo();
            //AbstractFactoryDemo();
            //AdapterDemo();
            //PrototypeDemo();
            //CommanderDemo();
            //BuilderDemo();
            //SingletonDemo();
            //StrategyDemo();
            IteratorDemo();
        }

        public static void IteratorDemo()
        {
            PlayerList playerList = new PlayerList();
            playerList.addPlayer(new Player());
            playerList.addPlayer(new Player());
            playerList.addPlayer(new Player());

            Iterator iterator = playerList.getIterator();

            while(iterator.hasNext())
            {
                Player p = (Player)iterator.next();
                Console.WriteLine(p.ToString());
                
            }
            Console.ReadLine();
        }

        public static void StrategyDemo()
        {
            Boost boost = new Boost(0, 0);
            boost.algorithm = new SpeedBoostAlgorithm();
            Player player = new Player();
            Console.WriteLine("Zaidejo greitis: " + player.speed);

            boost.algorithm.UseBoost(player, null);
            Console.WriteLine("Zaidejo greitis po Boost panaudojimo: " + player.speed);
        }

        public static void SingletonDemo()
        {
            Factory factory1 = BlockFactorySingleton.GetInstance();
            Factory factory2 = BlockFactorySingleton.GetInstance();

            //Console.WriteLine(factory1.CreateBlock("box", 0, 0).isSolid);

            if (Object.ReferenceEquals(factory1, factory2))
            {
                Console.WriteLine("Factory1 ir Factory2 yra tas pats objektas");
            }
        }

        public static void FactoryDemo()
        {
            BlockFactory factory = BlockFactorySingleton.GetInstance();
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
            //Explosive bomb = abstractFactory.CreateBomb(0, 0, 100, 0);
           // Explosive mine = abstractFactory.CreateMine(0, 0);
            //Explosive explosion = abstractFactory.CreateExplosion(0, 0, 0);
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
            Console.WriteLine("mapOriginal.units == mapShallowClone.units : " + (mapOriginal.units == mapShallowClone.units));
            Console.WriteLine("mapOriginal.explosions == mapShallowClone.explosions : " + (mapOriginal.explosions == mapShallowClone.explosions));
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

        public static void CommanderDemo()
        {
            Unit[,] units = new Unit[50,50];
            Explosive[,] explosion = new Explosive[0, 0];
            MapFacade mapOriginal = new MapFacade(10, 10, units, explosion);


            Player player = new Player("aaaa", "Player", 0);
            Console.WriteLine("Pradine pozicija " + player.getPos()[0] + "-"+player.getPos()[1]);

            ICommand command = new MoveDown(player, mapOriginal);
            player.SetCommand(command);
            player.Move();

            Console.WriteLine("Pajudejimas i apacia " + player.getPos()[0] + "-" + player.getPos()[1]);

            player.Undo();

            Console.WriteLine("Pozicija po undo " + player.getPos()[0] + "-" + player.getPos()[1]);
        }

        public static void BuilderDemo()
        {
            Random rand = new Random();
            int r = rand.Next(100);
            MapDirector mapDirector;
            if (r < 30)
            {
                mapDirector = new MapDirector(new ConcreteMapBuilder());
            }
            else if (r < 60)
            {
                mapDirector = new MapDirector(new DefaultMapBuilder());
            }
            else
            {
                mapDirector = new MapDirector(new TeleporterMapBuilder());
            }
            Console.WriteLine(r);
            mapDirector.constructMap();
            Map map = mapDirector.getMap();

            Unit[,] units = map.getUnits();
            for (int i = 0; i < map.xSize; i++)
            {
                for (int j = 0; j < map.ySize; j++)
                {
                    if (units[i, j]!=null)
                    {
                        Console.Write(units[i, j] + "-");
                    }
                    else
                    {
                        Console.Write("blank-");
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
