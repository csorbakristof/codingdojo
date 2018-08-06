using OpenCvSharp;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace _20180806SpirographLib
{
    public class RotatingStroke : Stroke
    {
        private Stroke delegateStroke;
        private double angularSpeed;
        private double radius;
        public RotatingStroke(Stroke delegateStroke, double angularSpeed, double radius)
        {
            this.delegateStroke = delegateStroke;
            this.angularSpeed = angularSpeed;
            this.radius = radius;
        }

        public IEnumerator<Point> GetEnumerator()
        {
            var controlPoints = delegateStroke.ToList();
            for (int i = 0; i < controlPoints.Count; i++)
            {
                double x = Math.Round(controlPoints[i].X - radius * Math.Sin(i * angularSpeed / 180.0 * Math.PI));
                double y = Math.Round(controlPoints[i].Y - radius * Math.Cos(i * angularSpeed / 180.0 * Math.PI));
                yield return new Point(x, y);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
