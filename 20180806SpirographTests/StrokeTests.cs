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

        [TestMethod]
        public void Interpolate()
        {
            var p1 = new Point(0, 0);
            var pTest1 = new Point(6, 0);
            var p2 = new Point(10, 0);
            var pTest2 = new Point(10, 4);
            var p3 = new Point(10, 10);
            var s = new Stroke();
            s.Add(p1);
            s.Add(p2);
            s.Add(p3);
            s.Interpolate();
            Assert.IsTrue(s.Any(i => i == p1));
            Assert.IsTrue(s.Any(i => i == pTest1));
            Assert.AreEqual(1, s.Count(i => i == p2));
            Assert.IsTrue(s.Any(i => i == pTest2));
            Assert.IsTrue(s.Any(i => i == p3));
            Assert.AreEqual(21, s.Count());
        }
    }
}
