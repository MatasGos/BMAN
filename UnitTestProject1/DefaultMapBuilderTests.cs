using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Tests
{
    [TestClass()]
    public class DefaultMapBuilderTests
    {
        [TestMethod()]
        public void DefaultMapBuilderTest()
        {
            MapDirector mapDirector = new MapDirector(new DefaultMapBuilder());
            mapDirector.constructMap();

            Assert.IsTrue(mapDirector.getMap().units[5, 1] is Box);
        }
    }
}