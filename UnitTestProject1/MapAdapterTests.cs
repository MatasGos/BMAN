using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Tests
{
    [TestClass()]
    public class MapAdapterTests
    {
        [TestMethod()]
        public void PerformPlayerActionsTest()
        {
            Map m = new Map(23, 19);
            MapAdapter ma = new MapAdapter(m);
            Player p = new Player();
            p.x = 15 * 25 + 12;
            p.y = 15 * 25 + 12;
            p.actionSecondary = "undo";
            ma.PerformPlayerActions(p, 10);
        }
    }
}