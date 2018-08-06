using OpenCvSharp;
using System;
using System.Collections;
using System.Collections.Generic;

namespace _20180806SpirographLib
{
    public class PolyStroke : Stroke
    {
        private List<SPoint> points = new List<SPoint>();

        public PolyStroke()
        {
        }

        public PolyStroke(SPoint[] points)
        {
            this.points.AddRange(points);
        }

        public void Add(SPoint point)
        {
            points.Add(point);
        }

        public IEnumerator<SPoint> GetEnumerator()
        {
            return points.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return points.GetEnumerator();
        }
    }
}
