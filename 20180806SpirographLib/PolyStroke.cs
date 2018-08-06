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
    }
}
