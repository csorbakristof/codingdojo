using OpenCvSharp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace _20180806SpirographLib
{
    public class InterpolatingStroke : Stroke
    {
        private Stroke delegateStroke;
        public InterpolatingStroke(Stroke delegateStroke)
        {
            this.delegateStroke = delegateStroke;
        }

        public IEnumerator<Point> GetEnumerator()
        {
            var controlPoints = delegateStroke.ToList();
            for (int i = 0; i < controlPoints.Count - 1; i++)
            {
                var newPoints = GetPointsBetween(controlPoints[i], controlPoints[i + 1], (i != 0));
                foreach (var p in newPoints)
                    yield return p;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public IEnumerable<Point> GetPointsBetween(Point start, Point end, bool skipStartingPoint = false)
        {
            if (!skipStartingPoint)
                yield return start;
            Point p = start;
            while (p != end)
            {
                p.X += Math.Sign(end.X - p.X);
                p.Y += Math.Sign(end.Y - p.Y);
                yield return p;
            }
        }
    }
}
