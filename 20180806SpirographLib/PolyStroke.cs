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

        public int NumberOfIterations { get; set; } = 1;

        public void Add(SPoint point)
        {
            points.Add(point);
        }

        public IEnumerator<SPoint> GetEnumerator()
        {
            for(int i=0; i<NumberOfIterations; i++)
            {
                foreach (var p in points)
                    yield return p;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
