using Microsoft.VisualStudio.TestTools.UnitTesting;
using Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Client.Tests
{
    [TestClass()]
    public class GraphicsRepositoryTests
    {
        [TestMethod()]
        public void GetBackgroundColorTest()
        {
            GraphicsRepository<Bitmap, Color> graphics = new GraphicsRepository<Bitmap, Color>();
            Color bgColor = graphics.GetBackgroundColor();
        } 

        [TestMethod()]
        public void CreateGraphicsObjectTest()
        {
            GraphicsRepository<Bitmap, Color> graphics = new GraphicsRepository<Bitmap, Color>();
            var gObject = graphics.CreateGraphicsObject(10, 10);
        }

        [TestMethod()]
        public void ColorFromArgbTest()
        {
            GraphicsRepository<Bitmap, Color> graphics = new GraphicsRepository<Bitmap, Color>();
            Color color = graphics.ColorFromArgb(0, 1, 2, 3);
            Assert.AreEqual(0, color.A);
            Assert.AreEqual(1, color.R);
            Assert.AreEqual(2, color.G);
            Assert.AreEqual(3, color.B);
        }

        [TestMethod()]
        public void ColorATest()
        {
            GraphicsRepository<Bitmap, Color> graphics = new GraphicsRepository<Bitmap, Color>();
            Color color = Color.Red;
            Assert.AreEqual(color.A, graphics.ColorA(color));
        }

        [TestMethod()]
        public void ColorRTest()
        {
            GraphicsRepository<Bitmap, Color> graphics = new GraphicsRepository<Bitmap, Color>();
            Color color = Color.Red;
            Assert.AreEqual(color.R, graphics.ColorR(color));
        }

        [TestMethod()]
        public void ColorGTest()
        {
            GraphicsRepository<Bitmap, Color> graphics = new GraphicsRepository<Bitmap, Color>();
            Color color = Color.Red;
            Assert.AreEqual(color.G, graphics.ColorG(color));
        }

        [TestMethod()]
        public void ColorBTest()
        {
            GraphicsRepository<Bitmap, Color> graphics = new GraphicsRepository<Bitmap, Color>();
            Color color = Color.Red;
            Assert.AreEqual(color.B, graphics.ColorB(color));
        }

        [TestMethod()]
        public void WrongTypesTest()
        {
            GraphicsRepository<Color, Bitmap> graphics = new GraphicsRepository<Color, Bitmap>();
            bool exceptionThrown = false;
            try
            {
                var bgColor = graphics.GetBackgroundColor();
            }
            catch
            {
                exceptionThrown = true;
            }
            Assert.IsTrue(exceptionThrown);
            exceptionThrown = false;
            try
            {
                var bgColor = graphics.CreateGraphicsObject(10, 10);
            }
            catch
            {
                exceptionThrown = true;
            }
            Assert.IsTrue(exceptionThrown);
            exceptionThrown = false;
            try
            {
                var bgColor = graphics.ColorFromArgb(10, 10, 10, 10);
            }
            catch
            {
                exceptionThrown = true;
            }
            Assert.IsTrue(exceptionThrown);
            exceptionThrown = false;
            try
            {
                var bgColor = graphics.ColorA(new Bitmap(10, 10));
            }
            catch
            {
                exceptionThrown = true;
            }
            Assert.IsTrue(exceptionThrown);
            exceptionThrown = false;
            try
            {
                var bgColor = graphics.ColorB(new Bitmap(10, 10));
            }
            catch
            {
                exceptionThrown = true;
            }
            Assert.IsTrue(exceptionThrown);
            exceptionThrown = false;
            try
            {
                var bgColor = graphics.ColorR(new Bitmap(10, 10));
            }
            catch
            {
                exceptionThrown = true;
            }
            Assert.IsTrue(exceptionThrown);
            exceptionThrown = false;
            try
            {
                var bgColor = graphics.ColorG(new Bitmap(10, 10));
            }
            catch
            {
                exceptionThrown = true;
            }
            Assert.IsTrue(exceptionThrown);
        }
    }
}