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

        [TestMethod]
        public void EnumeratesPointsBetweenEndpoints()
        {
            var p1 = new Point(0, 0);
            var p2 = new Point(10, 0);
            var pTest = new Point(6, 0);
            var s = new Stroke();
            s.AddPointsBetween(p1, p2);
            Assert.IsTrue(s.Any(i => i == p1));
            Assert.IsTrue(s.Any(i => i == pTest));
            Assert.IsTrue(s.Any(i => i == p2));
            Assert.AreEqual(11, s.Count());
        }
    }
}
