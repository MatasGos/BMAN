using System;
using System.Drawing;
using Model;
using Client;
using System.Diagnostics;
using System.Collections.Generic;

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
            //IteratorDemo();
            //MediatorDemo();
            TemplateDemo();
            //FlyweightDemo();
        }

        public static void MediatorDemo()
        {
            ILogMediator mediator = new LogMediator();

            ConsoleLogger consoleLog = new ConsoleLogger(mediator);

            LogPlayer playerLog = new LogPlayer(mediator);

            mediator.addLogReceiver(consoleLog);
            mediator.addLogSender(playerLog);

            playerLog.sendMessage("Player killed Player 2");

            consoleLog.sendMessage("error: console error");

            
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

        public static void FlyweightDemo()
        {
            int countOfIterations = 1000;
            Stopwatch sw = new Stopwatch();
            string[] pictures = { "wall", "bomb", "box" };
            var graphics = new GraphicsRepository<Bitmap, Color>();
            var picturesFlyweight = new PictureFlyweight<Bitmap, Color>();
            sw.Start();
            Color[,] temp;
            for (int i = 0; i < countOfIterations; i++)
            {
                GraphicsAdapter<Bitmap, Color> pic = graphics.CreateGraphicsObject(25, 25);
                pic.SetImage(Images.wall);
                temp = pic.GetColorArray();
                pic.SetImage(Images.bomb);
                temp = pic.GetColorArray();
                pic.SetImage(Images.box);
                temp = pic.GetColorArray();
            }
            sw.Stop();
            Console.WriteLine("Time to read " + countOfIterations + " elements without flyweight " + sw.ElapsedMilliseconds + "ms");
            sw.Reset();
            sw.Start();
            for (int i = 0; i < countOfIterations; i++)
            {
                for (int j  = 0; j < pictures.Length; j++)
                {
                    temp = picturesFlyweight.GetPictureArray(pictures[j]);
                }
            }
            sw.Stop();
            Console.WriteLine("Time to read " + countOfIterations + " elements with flyweight " + sw.ElapsedMilliseconds + "ms");
        }

        public static void TemplateDemo()
        {
            ScoreboardTemplate templateMatch = new ScoreboardMatch();
            ScoreboardTemplate templateRound = new ScoreboardRound();

            Player player1 = new Player("aaaa1", "Player1", 0);
            Player player2 = new Player("aaaa2", "Player2", 1);
            Player player3 = new Player("aaaa3", "Player3", 2);

            templateMatch.AddPlayer(player1);
            templateMatch.AddPlayer(player2);
            templateMatch.AddPlayer(player3);

            templateRound.AddPlayer(player1);
            templateRound.AddPlayer(player2);
            templateRound.AddPlayer(player3);

            templateMatch.AddScore(player1, 1);
            templateMatch.AddScore(player1, 1);
            templateMatch.AddScore(player2, 3);

            templateRound.AddScore(player1, 1);
            templateRound.AddScore(player1, 1);
            templateRound.AddScore(player2, 1);

            Console.WriteLine("Match template:");
            foreach (var x in templateMatch.FormTable())
            {
                Console.WriteLine(x.Item2);
            }
            Console.WriteLine("Round template:");
            foreach (var x in templateRound.FormTable())
            {
                Console.WriteLine(x.Item2);
            }

        }
    }
}
