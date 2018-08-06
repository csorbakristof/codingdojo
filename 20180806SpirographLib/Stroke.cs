using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenCvSharp;

namespace _20180806SpirographLib
{
    public class Stroke : IEnumerable<Point>
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

        public void AddPointsBetween(Point start, Point end)
        {
            points.Add(start);
            Point p = start;
            while(p != end)
            {
                p.X += Math.Sign(end.X - p.X);
                p.Y += Math.Sign(end.Y - p.Y);
                points.Add(p);
            }
        }
    }
}
