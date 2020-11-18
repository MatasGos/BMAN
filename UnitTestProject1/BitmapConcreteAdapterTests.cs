using Microsoft.VisualStudio.TestTools.UnitTesting;
using Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Client.Tests
{
    [TestClass()]
    public class BitmapConcreteAdapterTests
    {
        [DataTestMethod]
        [DataRow(100, 100)]
        [DataRow(10, 10)]
        [DataRow(50, 50)]
        public void BitmapConcreteAdapterTest(int x, int y)
        {
            BitmapConcreteAdapter adapter = new BitmapConcreteAdapter(x,y);
        }

        [DataTestMethod]
        [DataRow(0, 100)]
        [DataRow(0, 0)]
        [DataRow(100, 0)]
        [DataRow(-1, 100)]
        [DataRow(100, -1)]
        [DataRow(-1, -1)]
        public void BitmapConcreteAdapterFailTest(int x, int y)
        {
            bool exceptionThrown = false;
            try
            {
                BitmapConcreteAdapter adapter = new BitmapConcreteAdapter(x, y);
            }
            catch
            {
                exceptionThrown = true;
            }
            Assert.IsTrue(exceptionThrown);
        }


        [TestMethod()]
        public void GetImageCopyTest()
        {
            BitmapConcreteAdapter adapter = new BitmapConcreteAdapter(100, 100);
            Bitmap copy = adapter.GetImageCopy();
            Assert.AreNotEqual(copy, adapter.GetImage());
        }

        [TestMethod()]
        public void GetImageTest()
        {
            BitmapConcreteAdapter adapter = new BitmapConcreteAdapter(100, 100);
            Bitmap picture = adapter.GetImage();
            Assert.IsNotNull(picture);
            Assert.AreEqual(100, picture.Width);
            Assert.AreEqual(100, picture.Height);
        }

        [TestMethod()]
        public void LockBitsTest()
        {
            BitmapConcreteAdapter adapter = new BitmapConcreteAdapter(10, 10);
            Bitmap picture1 = adapter.GetImage();
            adapter.LockBits();
            bool exceptionThrown = false;
            try
            {
                picture1.GetPixel(0, 0);
            }
            catch
            {
                exceptionThrown = true;
            }
            Assert.IsTrue(exceptionThrown);            
        }

        [TestMethod()]
        public void LockBits2Test()
        {
            BitmapConcreteAdapter adapter = new BitmapConcreteAdapter(10, 10);
            bool exceptionThrown = false;
            adapter.LockBits();
            try
            {
                adapter.LockBits();
            }
            catch
            {
                exceptionThrown = true;
            }
            Assert.IsTrue(exceptionThrown);
        }

        [TestMethod()]
        public void UnlockBitsTest()
        {
            BitmapConcreteAdapter adapter = new BitmapConcreteAdapter(10, 10);
            Bitmap picture1 = adapter.GetImage();
            adapter.LockBits();
            adapter.UnlockBits();
            bool exceptionThrown = false;
            try
            {
                picture1.GetPixel(0, 0);
            }
            catch
            {
                exceptionThrown = true;
            }
            Assert.IsFalse(exceptionThrown);
        }

        [TestMethod()]
        public void UnlockBits2Test()
        {
            BitmapConcreteAdapter adapter = new BitmapConcreteAdapter(10, 10);
            adapter.LockBits();
            adapter.UnlockBits();
            bool exceptionThrown = false;
            try
            {
                adapter.UnlockBits();
            }
            catch
            {
                exceptionThrown = true;
            }
            Assert.IsTrue(exceptionThrown);
        }

        [TestMethod()]
        public void GetColorArrayTest()
        {
            BitmapConcreteAdapter adapter = new BitmapConcreteAdapter(10, 10);
            Bitmap picture1 = adapter.GetImage();
            adapter.LockBits();
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    adapter.SetPixel(i, j, Color.Red);
                }
            }
            adapter.UnlockBits();
            Color[,] colorArray = adapter.GetColorArray();
            Bitmap picture = adapter.GetImage();
            for (int i = 0; i < 10; i++)
            {
                for (int j = 0; j < 10; j++)
                {
                    Assert.AreEqual(picture.GetPixel(i, j), colorArray[i, j]);
                }
            }
        }

        [TestMethod()]
        public void SetPixelTest()
        {
            BitmapConcreteAdapter adapter = new BitmapConcreteAdapter(10, 10);
            Bitmap picture1 = adapter.GetImage();
            adapter.LockBits();
            adapter.SetPixel(0, 0, Color.Red);
            adapter.UnlockBits();
            Assert.AreEqual(Color.Red.ToArgb(), picture1.GetPixel(0, 0).ToArgb());
        }

        [TestMethod()]
        public void GetWidthTest()
        {
            BitmapConcreteAdapter adapter = new BitmapConcreteAdapter(10, 11);
            Assert.AreEqual(10, adapter.GetWidth());
        }

        [TestMethod()]
        public void GetHeightTest()
        {
            BitmapConcreteAdapter adapter = new BitmapConcreteAdapter(10, 11);
            Assert.AreEqual(11, adapter.GetHeight());
        }

        [TestMethod()]
        public void SetImageTest()
        {
            BitmapConcreteAdapter adapter = new BitmapConcreteAdapter(10, 11);
            Bitmap picture = new Bitmap(5, 5);
            adapter.SetImage(picture);
            Assert.AreEqual(picture, adapter.GetImage());
        }
    }
}