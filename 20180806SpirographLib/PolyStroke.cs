using OpenCvSharp;
using System;
using System.Collections;
using System.Collections.Generic;

namespace _20180806SpirographLib
{
    public class PolyStroke : Stroke
    {
        protected List<Point> points = new List<Point>();
        public void Add(Point point)
        {
            points.Add(point);
        }

        public IEnumerator<Point> GetEnumerator()
        {
            return points.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return points.GetEnumerator();
        }

        public void AddPointsBetween(Point start, Point end, bool skipStartingPoint = false)
        {
            if (!skipStartingPoint)
                points.Add(start);
            Point p = start;
            while (p != end)
            {
                p.X += Math.Sign(end.X - p.X);
                p.Y += Math.Sign(end.Y - p.Y);
                points.Add(p);
            }
        }
    }
}
