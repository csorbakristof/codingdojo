using System;
using System.Linq;
using _20180806SpirographLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenCvSharp;

namespace _20180806SpirographTests
{
    [TestClass]
    public class StrokeTests
    {
        [TestMethod]
        public void PointStorage()
        {
            var p1 = new Point(0, 0);
            var p2 = new Point(1, 1);
            var s = new Stroke();
            s.Add(p1);
            s.Add(p2);
            Assert.IsTrue(s.Any(i => i == p1));
            Assert.IsTrue(s.Any(i => i == p2));
        }
    }
}
