﻿using System;
using System.Linq;
using _20180806SpirographLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenCvSharp;

namespace _20180806SpirographTests
{
    [TestClass]
    public class StrokeTests
    {
        Stroke stroke = new Stroke();

        [TestMethod]
        public void PointStorage()
        {
            var p1 = new Point(0, 0);
            var p2 = new Point(1, 1);
            stroke.Add(p1);
            stroke.Add(p2);
            AssertPointPresence(p1);
            AssertPointPresence(p2);
        }

        [TestMethod]
        public void EnumeratesPointsBetweenEndpoints()
        {
            var p1 = new Point(0, 0);
            var p2 = new Point(10, 0);
            var pTest = new Point(6, 0);
            stroke.AddPointsBetween(p1, p2);
            AssertPointPresence(p1);
            AssertPointPresence(pTest);
            AssertPointPresence(p2);
            Assert.AreEqual(11, stroke.Count());
        }

        [TestMethod]
        public void Interpolate()
        {
            var p1 = new Point(0, 0);
            var pTest1 = new Point(6, 0);
            var p2 = new Point(10, 0);
            var pTest2 = new Point(10, 4);
            var p3 = new Point(10, 10);
            stroke.Add(p1);
            stroke.Add(p2);
            stroke.Add(p3);
            stroke.Interpolate();
            AssertPointPresence(p1);
            AssertPointPresence(pTest1);
            Assert.AreEqual(1, stroke.Count(i => i == p2));
            AssertPointPresence(pTest2);
            AssertPointPresence(p3);
            Assert.AreEqual(21, stroke.Count());
        }

        [TestMethod]
        public void Rotate()
        {
            var p1 = new Point(0, 0);
            var p2 = new Point(10, 0);
            var p3 = new Point(20, 0);
            stroke.Add(p1);
            stroke.Add(p2);
            stroke.Add(p3);
            stroke.Rotate(90.0, 5.0);
            Assert.AreEqual(3, stroke.Count());
            var points = stroke.ToList();
            AssertPointPresence(Shift(p1, 0, -5));
            AssertPointPresence(Shift(p2, -5, 0));
            AssertPointPresence(Shift(p3, 0, 5));

        }

        private Point Shift(Point basePoint, int xShift, int yShift)
        {
            return new Point(basePoint.X + xShift, basePoint.Y + yShift);
        }

        private void AssertPointPresence(Point p)
        {
            Assert.IsTrue(stroke.Any(i => i == p));
        }
    }
}
