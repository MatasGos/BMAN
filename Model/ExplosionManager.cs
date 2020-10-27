using System;
using System.Collections.Generic;
using System.Text;

namespace Model
{
    public class ExplosionManager
    {
        int xSize;
        int ySize;
        Unit[,] units;
        Boost[,] boosts;

        public ExplosionManager(int xSize, int ySize, Unit[,] units, Boost[,] boosts)
        {
            this.xSize = xSize;
            this.ySize = ySize;
            this.units = units;
            this.boosts = boosts;
        }

        public void UpdateExplosives(double time)
        {
            for (int x = 0; x < xSize; x++)
            {
                for (int y = 0; y < ySize; y++)
                {
                    ExplosiveAbstractFactory factory;
                    if (units[x, y] is Explosion)
                    {
                        Explosion explosion = (Explosion)units[x, y];
                        if (explosion.removalTime < time)
                        {
                            units[x, y] = null;
                        }
                    }
                    else if (units[x, y] is Bomb)
                    {
                        factory = new RegularExplosiveConcreteFactory();
                        Bomb bomb = (Bomb)units[x, y];

                        if (bomb.detonationTime < time)
                        {
                            PlaceRegularExplosion(x, y, bomb.explosionPower, time, factory);
                        }
                    }
                    else if (units[x, y] is SuperBomb)
                    {
                        factory = new SuperExplosiveConcreteFactory();
                        SuperBomb bomb = (SuperBomb)units[x, y];

                        if (bomb.detonationTime < time)
                        {
                            PlaceSuperExplosion(x, y, bomb.explosionPower, time, factory);
                        }
                    }
                }
            }
        }

        private void PlaceRegularExplosion(int xTile, int yTile, int explosionPower, double placeTime, ExplosiveAbstractFactory factory)
        {
            bool topFinished = false;
            bool bottomFinished = false;
            bool leftFinished = false;
            bool rightFinished = false;

            units[xTile, yTile] = factory.CreateExplosion(xTile, yTile, placeTime);

            for (int i = 1; i < explosionPower; i++)
            {
                if (!topFinished)
                {
                    topFinished = RegularExplosionCreation(xTile, yTile - i, placeTime, topFinished, factory);
                }
                if (!bottomFinished)
                {
                    bottomFinished = RegularExplosionCreation(xTile, yTile + i, placeTime, bottomFinished, factory);
                }
                if (!leftFinished)
                {
                    leftFinished = RegularExplosionCreation(xTile - i, yTile, placeTime, leftFinished, factory);
                }
                if (!rightFinished)
                {
                    rightFinished = RegularExplosionCreation(xTile + i, yTile, placeTime, rightFinished, factory);
                }
            }
        }

        private bool RegularExplosionCreation(int x, int y, double placeTime, bool isFinished, ExplosiveAbstractFactory factory)
        {
            if (units[x, y] == null || units[x, y] is Explosion)
            {
                units[x, y] = factory.CreateExplosion(x, y, placeTime);
            }
            else
            {
                if (units[x, y] is Box)
                {
                    Random rand = new Random();
                    int n = rand.Next(100);
                    if (n < 30)
                    {
                        boosts[x, y] = PickBoostStrategy(x, y);
                    }
                    units[x, y] = factory.CreateExplosion(x, y, placeTime);

                }
                isFinished = true;
            }
            return isFinished;
        }

        private Boost PickBoostStrategy(int x, int y)
        {
            Boost boost = new Boost(x, y);
            Random rand = new Random();
            int n = rand.Next(100);
            if (n < 50)
            {
                boost.boostType = "speed";
                boost.algorithm = new SpeedBoostAlgorithm();
            }
            else if (n < 100)
            {
                boost.boostType = "explosion";
                boost.algorithm = new ExplosionRangeBoostAlgorithm();
            }
            return boost;
        }

        private void PlaceSuperExplosion(int xTile, int yTile, int explosionPower, double placeTime, ExplosiveAbstractFactory factory)
        {
            units[xTile, yTile] = factory.CreateExplosion(xTile, yTile, placeTime);

            for (int x = Math.Max(1, xTile - explosionPower); x <= Math.Min(xTile + explosionPower, xSize - 2); x++)
            {
                for (int y = Math.Max(1, yTile - explosionPower); y <= Math.Min(yTile + explosionPower, ySize - 2); y++)
                {
                    if (units[x, y] == null || units[x, y] is Box || units[x, y] is Explosion)
                    {
                        units[x, y] = factory.CreateExplosion(x, y, placeTime);
                    }
                }
            }
        }
    }
}
