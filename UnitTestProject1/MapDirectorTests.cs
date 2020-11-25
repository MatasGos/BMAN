using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Tests
{
    [TestClass()]
    public class MapDirectorTests
    {
        [TestMethod()]
        public void MapDirectorTest()
        {
            MapDirector mapDirector = new MapDirector(new ConcreteMapBuilder());

        }

        [TestMethod()]
        public void getMapTest()
        {
            MapDirector mapDirector = new MapDirector(new ConcreteMapBuilder());
            Assert.IsNull(mapDirector.getMap().units[0, 0]);
        }

        [TestMethod()]
        public void constructMapTest()
        {
            MapDirector mapDirector = new MapDirector(new ConcreteMapBuilder());
            mapDirector.constructMap();
            Assert.IsTrue(mapDirector.getMap().units[0, 0] is Wall);
        }
    }
}