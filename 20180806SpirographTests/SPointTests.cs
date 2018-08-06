using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using _20180806SpirographLib;
using OpenCvSharp;

namespace _20180806SpirographTests
{
    /// <summary>
    /// Summary description for SPointTests
    /// </summary>
    [TestClass]
    public class SPointTests
    {
        [TestMethod]
        public void BasicOperations()
        {
            var sp = new SPoint(10,20);
            sp.Color = new Vec3b(255, 255, 255);
            Assert.AreEqual(10, sp.X);
            Assert.AreEqual(20, sp.Y);

            var sp2 = new SPoint(sp);
            Assert.AreEqual(sp.X, sp2.X);
            Assert.AreEqual(sp.Y, sp2.Y);
            Assert.AreEqual(sp.Color, sp2.Color);
            Assert.AreEqual(sp, sp2);

            var color = new Vec3b(20, 30, 40);
            var sp3 = new SPoint(10,20,new Vec3b(20,30,40));
            Assert.AreEqual(color, sp3.Color);
        }
    }
}
