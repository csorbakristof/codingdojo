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
            var p1 = new SPoint(0, 0);
            var p2 = new SPoint(1, 1);
            var s = new PolyStroke(new SPoint[] { p1, p2 });
            AssertPointPresence(s, new SPoint[] { p1, p2 });
        }

        [TestMethod]
        public void Interpolate()
        {
            var p1 = new SPoint(0, 0);
            var pTest1 = new SPoint(6, 0);
            var p2 = new SPoint(10, 0);
            var pTest2 = new SPoint(10, 4);
            var p3 = new SPoint(10, 10);
            var stroke = new InterpolatingStroke(
                new PolyStroke(new SPoint[] { p1, p2, p3 }));
            Assert.AreEqual(21, stroke.Count());
            AssertPointPresence(stroke, new SPoint[] { p1, pTest1, p2, pTest2, p3 });
        }

        [TestMethod]
        public void Rotate()
        {
            var p1 = new SPoint(0, 0);
            var p2 = new SPoint(10, 0);
            var p3 = new SPoint(20, 0);
            var s = new PolyStroke(new SPoint[] { p1, p2, p3 });
            var stroke = new RotatingStroke(s, 90.0, 5.0);
            Assert.AreEqual(3, stroke.Count());
            AssertPointPresence(stroke, new SPoint[] {
                Shift(p1, 0, -5), Shift(p2, -5, 0), Shift(p3, 0, 5) });

        }

        [TestMethod]
        public void Dashed()
        {
            var p1 = new SPoint(0, 0);
            var p2 = new SPoint(10, 0);
            var p3 = new SPoint(20, 0);
            var p4 = new SPoint(30, 0);
            var s = new PolyStroke(new SPoint[] { p1, p2, p3, p4 });
            var dashed = new DashedStroke(s, 2);
            Assert.AreEqual(2, dashed.Count());
            AssertPointPresence(dashed, new SPoint[] {p1, p3});
        }

        [TestMethod]
        public void MultipleIterationsByPolystroke()
        {
            const int numberOfIterations = 3;
            var p1 = new SPoint(0, 0);
            var p2 = new SPoint(10, 0);
            var p3 = new SPoint(10, 10);
            var p4 = new SPoint(0, 10);
            var s = new PolyStroke(new SPoint[] { p1, p2, p3, p4 });
            s.NumberOfIterations = numberOfIterations;
            var points = s.ToArray();
            Assert.AreEqual(4*numberOfIterations, points.Length);
            AssertPointPresence(s, new SPoint[] { p1, p2, p3, p4}, 3);
        }

        private SPoint Shift(SPoint basePoint, int xShift, int yShift)
        {
            return new SPoint(basePoint.X + xShift, basePoint.Y + yShift);
        }

        private void AssertPointPresence(Stroke s, SPoint[] points, int count = 1)
        {
            foreach (var p in points)
                AssertPointPresence(s, p, count);
        }

        private void AssertPointPresence(Stroke s, SPoint p, int count=1)
        {
            var points = s.ToList();
            Assert.AreEqual(count, s.Count(i => i == p));
        }
    }
}
