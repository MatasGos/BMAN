using Microsoft.VisualStudio.TestTools.UnitTesting;
using Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Model.Tests
{
    [TestClass()]
    public class BlockFactoryTests
    {
        [TestMethod()]
        public void CreateBlockTest()
        {
            Factory fact = new BlockFactory();
            Block b = fact.CreateBlock("dsf", 1, 2);
            Assert.IsTrue(b is null);
        }
    }
}