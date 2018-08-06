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
    }
}
