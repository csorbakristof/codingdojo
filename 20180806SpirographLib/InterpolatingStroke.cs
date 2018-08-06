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

        public IEnumerator<SPoint> GetEnumerator()
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

        public IEnumerable<SPoint> GetPointsBetween(SPoint start, SPoint end, bool skipStartingPoint = false)
        {
            if (!skipStartingPoint)
                yield return start;
            SPoint p = new SPoint(start);
            int pointNumber = Math.Max(Math.Abs(end.X - p.X), Math.Abs(end.Y - p.Y));
            int i = 0;
            while (p != end)
            {
                p.X += Math.Sign(end.X - p.X);
                p.Y += Math.Sign(end.Y - p.Y);
                for(int j=0; j<3; j++)
                    p.Color[j] = interpolateValue(start.Color[j], end.Color[j], pointNumber, i);
                yield return new SPoint(p);
            }
        }

        private byte interpolateValue(byte a, byte b, int n, int i)
        {
            double start = (double)a;
            double step = ((double)b - (double)a) / (double)n;
            return (byte)Math.Round(start + i * step);
        }
    }
}
