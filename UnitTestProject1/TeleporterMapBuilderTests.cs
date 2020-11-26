using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Tests
{
    [TestClass()]
    public class TeleporterMapBuilderTests
    {
        //Tests map construction and wether it wat built correctly
        [TestMethod()]
        public void TeleporterMapBuilderTest()
        {
            MapDirector mapDirector = new MapDirector(new TeleporterMapBuilder());
            mapDirector.constructMap();

            Assert.IsTrue(mapDirector.getMap().units[1, 4] is Teleporter);
        }
    }
}