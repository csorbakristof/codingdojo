using System;
using _20180806SpirographLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenCvSharp;

namespace _20180806SpirographTests
{
    [TestClass]
    public class StrokeTests
    {
        [TestMethod]
        public void TestStroke()
        {
            var s = new Stroke();
            s.Add(new Point(0, 0));
        }
    }
}
