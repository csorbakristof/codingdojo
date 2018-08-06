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
            var s = new PolyStroke(new Point[] { p1, p2 });
            AssertPointSinglePresence(s, new Point[] { p1, p2 });
        }

        [TestMethod]
        public void Interpolate()
        {
            var p1 = new Point(0, 0);
            var pTest1 = new Point(6, 0);
            var p2 = new Point(10, 0);
            var pTest2 = new Point(10, 4);
            var p3 = new Point(10, 10);
            var stroke = new InterpolatingStroke(
                new PolyStroke(new Point[] { p1, p2, p3 }));
            Assert.AreEqual(21, stroke.Count());
            AssertPointSinglePresence(stroke, new Point[] { p1, pTest1, p2, pTest2, p3 });
        }

        [TestMethod]
        public void Rotate()
        {
            var p1 = new Point(0, 0);
            var p2 = new Point(10, 0);
            var p3 = new Point(20, 0);
            var s = new PolyStroke(new Point[] { p1, p2, p3 });
            var stroke = new RotatingStroke(s, 90.0, 5.0);
            Assert.AreEqual(3, stroke.Count());
            AssertPointSinglePresence(stroke, new Point[] {
                Shift(p1, 0, -5), Shift(p2, -5, 0), Shift(p3, 0, 5) });

        }

        [TestMethod]
        public void Dashed()
        {
            var p1 = new Point(0, 0);
            var p2 = new Point(10, 0);
            var p3 = new Point(20, 0);
            var p4 = new Point(30, 0);
            var s = new PolyStroke(new Point[] { p1, p2, p3, p4 });
            var dashed = new DashedStroke(s, 2);
            Assert.AreEqual(2, dashed.Count());
            AssertPointSinglePresence(dashed, new Point[] {p1, p3});

        }

        private Point Shift(Point basePoint, int xShift, int yShift)
        {
            return new Point(basePoint.X + xShift, basePoint.Y + yShift);
        }

        private void AssertPointSinglePresence(Stroke s, Point[] points)
        {
            foreach (var p in points)
                AssertPointSinglePresence(s, p);
        }

        private void AssertPointSinglePresence(Stroke s, Point p)
        {
            Assert.AreEqual(1, s.Count(i => i == p));
        }
    }
}
