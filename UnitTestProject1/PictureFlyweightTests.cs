using Microsoft.VisualStudio.TestTools.UnitTesting;
using Client;
using System;
using System.Collections.Generic;
using System.Text;
using System.Drawing;

namespace Client.Tests
{
    [TestClass()]
    public class PictureFlyweightTests
    {
        [TestMethod()]
        //Tests the constructor
        public void PictureFlyweightTest()
        {
            PictureFlyweight<Bitmap, Color> pictures = new PictureFlyweight<Bitmap, Color>();
        }
        [TestMethod()]
        //Tests the constructor by creating it with unsupported types
        public void PictureFlyweightFailTest()
        {
            bool exceptionThrown = false;
            try
            {
                PictureFlyweight<String, String> pictures = new PictureFlyweight<String, String>();
            }
            catch
            {
                exceptionThrown = true;
            }
            Assert.IsTrue(exceptionThrown);
        }

        [TestMethod()]
        //Tests GetPictureArray method by checking if it can create all objects it should be able to create
        public void GetPictureArrayTest()
        {
            PictureFlyweight<Bitmap, Color> pictures = new PictureFlyweight<Bitmap, Color>();
            string[] pictureTypes = { "wall", "p1", "box", "bomb", "mine", "boost", "superbomb", "supermine", "explosion", "fedora", "shoes", "superexplosion", "teleporterin", "teleporterout" };
            Color[,] temp;
            for (int i = 0; i < 2; i++)
            {
                foreach (string type in pictureTypes)
                {
                    temp = pictures.GetPictureArray(type);
                }
            }
        }

        [TestMethod()]
        //Tests GetPictureArray method by checking if it throws and exception when it tries to create an object that should be not defined
        public void GetPictureArrayFailTest()
        {
            PictureFlyweight<Bitmap, Color> pictures = new PictureFlyweight<Bitmap, Color>();
            Color[,] temp;
            bool exceptionThrown = false;
            try
            {
                temp = pictures.GetPictureArray("neegzistuojanti_nuotrauka");
            }
            catch
            {
                exceptionThrown = true;
            }
            Assert.IsTrue(exceptionThrown);
        }
    }
}