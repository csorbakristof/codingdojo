using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using _20180806SpirographLib;
using OpenCvSharp;

namespace _20180806SpirographTests
{
    /// <summary>
    /// Summary description for InterpolatingStrokeTests
    /// </summary>
    [TestClass]
    public class InterpolatingStrokeTests
    {

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
            PointListsHelper.AssertPointPresence(stroke, new SPoint[] { p1, pTest1, p2, pTest2, p3 });
        }


        [TestMethod]
        public void InterpolateValueTest()
        {
            var s = new TestInterpolatingStroke(null);
            for (int i = 0; i < 10; i++)
                Assert.AreEqual(i, s.InterpolateValue(0, 9, 10, i));
        }

        [TestMethod]
        public void InterpolatedPointListTest()
        {
            var p1 = new SPoint(0, 0, new Vec3b(0, 0, 0));
            var p2 = new SPoint(10, 0, new Vec3b(20, 20, 20));
            var s = new TestInterpolatingStroke(null);
            var points = s.GetPointsBetween(p1, p2).ToArray();
            var pTest = new SPoint(5, 0, new Vec3b(10, 10, 10));
            Assert.AreEqual(11, points.Length);
            for (int i = 0; i < 10; i++)
            {
                Assert.AreEqual(i, points[i].X);
                Assert.AreEqual(0, points[i].Y);
                for(int j=0; j<3; j++)
                    Assert.AreEqual(2 * i, points[i].Color[j]);
            }
        }

        [TestMethod]
        public void ColorInterpolation()
        {
            var p1 = new SPoint(0, 0, new Vec3b(0, 0, 0));
            var pTest = new SPoint(5, 0, new Vec3b(10, 10, 10));
            var p2 = new SPoint(10, 0, new Vec3b(20, 20, 20));
            var stroke = new InterpolatingStroke(
                new PolyStroke(new SPoint[] { p1, p2 }));
            PointListsHelper.AssertPointPresence(stroke, new SPoint[] { p1, pTest, p2 });
        }

        private class TestInterpolatingStroke : InterpolatingStroke
        {
            public TestInterpolatingStroke(Stroke delegateStroke) : base(delegateStroke)
            {
            }
            public new System.Collections.Generic.IEnumerable<SPoint> GetPointsBetween(SPoint start, SPoint end, bool skipStartingPoint = false)
            {
                return base.GetPointsBetween(start, end, skipStartingPoint);
            }

            public new byte InterpolateValue(byte a, byte b, int n, int i)
            {
                return base.InterpolateValue(a, b, n, i);
            }
        }
    }
}
