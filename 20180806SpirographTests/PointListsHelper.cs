using _20180806SpirographLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace _20180806SpirographTests
{
    class PointListsHelper
    {
        public static SPoint Shift(SPoint basePoint, int xShift, int yShift)
        {
            return new SPoint(basePoint.X + xShift, basePoint.Y + yShift, basePoint.Color);
        }

        public static void AssertPointPresence(IStroke s, SPoint p, int count = 1)
        {
            AssertPointPresence(s.ToArray(), p, count);
        }

        public static void AssertPointPresence(IStroke s, SPoint[] points, int count = 1)
        {
            AssertPointPresence(s.ToArray(), points, count);
        }

        public static void AssertPointPresence(SPoint[] strokePoints, SPoint[] points, int count = 1)
        {
            foreach (var p in points)
                AssertPointPresence(strokePoints, p, count);
        }

        public static void AssertPointPresence(SPoint[] strokePoints, SPoint p, int count = 1)
        {
            Assert.AreEqual(count, strokePoints.Count(i => i == p));
        }
    }
}
