using _20180806SpirographLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20180806SpirographTests
{
    class PointListsHelper
    {
        public static SPoint Shift(SPoint basePoint, int xShift, int yShift)
        {
            return new SPoint(basePoint.X + xShift, basePoint.Y + yShift, basePoint.Color);
        }

        public static void AssertPointPresence(Stroke s, SPoint[] points, int count = 1)
        {
            foreach (var p in points)
                AssertPointPresence(s, p, count);
        }

        public static void AssertPointPresence(Stroke s, SPoint p, int count = 1)
        {
            Assert.AreEqual(count, s.Count(i => i == p));
        }
    }
}
