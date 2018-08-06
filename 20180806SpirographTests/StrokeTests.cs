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
            PointListsHelper.AssertPointPresence(s, new SPoint[] { p1, p2 });
        }

        [TestMethod]
        public void Rotate()
        {
            var color = new Vec3b(128, 192, 255);
            var p1 = new SPoint(0, 0, color);
            var p2 = new SPoint(10, 0);
            var p3 = new SPoint(20, 0);
            var s = new PolyStroke(new SPoint[] { p1, p2, p3 });
            var stroke = new RotatingStroke(s, 90.0, 5.0);
            Assert.AreEqual(3, stroke.Count());
            PointListsHelper.AssertPointPresence(stroke, new SPoint[] {
                PointListsHelper.Shift(p1, 0, -5),
                PointListsHelper.Shift(p2, -5, 0),
                PointListsHelper.Shift(p3, 0, 5) });
        }

        [TestMethod]
        public void RotationPreservesColorAndLineWidth()
        {
            var color = new Vec3b(128, 192, 255);
            var p1 = new SPoint(0, 0, color, 1);
            var p2 = new SPoint(10, 0, color, 2);
            var p3 = new SPoint(20, 0, color, 3);
            var s = new RotatingStroke(new PolyStroke(new SPoint[] { p1, p2, p3 }), 90.0, 5.0);
            Assert.AreEqual(3, s.Count());
            var points = s.ToArray();
            Assert.AreEqual(color, points[0].Color);
            for(int i=1; i<3; i++)
                Assert.AreEqual(i+1, points[i].LineWidth);
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
            PointListsHelper.AssertPointPresence(dashed, new SPoint[] {p1, p3});
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
            PointListsHelper.AssertPointPresence(s, new SPoint[] { p1, p2, p3, p4}, 3);
        }
    }
}
