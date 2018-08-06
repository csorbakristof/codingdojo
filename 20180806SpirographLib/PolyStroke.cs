using OpenCvSharp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20180806SpirographLib
{
    public class PolyStroke : Stroke
    {
        private List<Point> points = new List<Point>();
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

        public void Interpolate()
        {
            var controlPoints = points;
            points = new List<Point>();
            for (int i = 0; i < controlPoints.Count - 1; i++)
                AddPointsBetween(controlPoints[i], controlPoints[i + 1], (i != 0));
        }

        public void Rotate(double angularSpeed, double radius)
        {
            var controlPoints = points;
            points = new List<Point>();
            for (int i = 0; i < controlPoints.Count; i++)
            {
                double x = Math.Round(controlPoints[i].X - radius * Math.Sin(i * angularSpeed / 180.0 * Math.PI));
                double y = Math.Round(controlPoints[i].Y - radius * Math.Cos(i * angularSpeed / 180.0 * Math.PI));
                points.Add(new Point(x, y));
            }
        }
    }
}
