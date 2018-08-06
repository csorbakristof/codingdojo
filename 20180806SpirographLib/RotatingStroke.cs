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
        public double AngularSpeed { get; set; }
        public double Radius { get; set; }
        public RotatingStroke(Stroke delegateStroke, double angularSpeed, double radius)
        {
            this.delegateStroke = delegateStroke;
            this.AngularSpeed = angularSpeed;
            this.Radius = radius;
        }

        public IEnumerator<SPoint> GetEnumerator()
        {
            var controlPoints = delegateStroke.ToList();
            for (int i = 0; i < controlPoints.Count; i++)
            {
                int x = (int)Math.Round(controlPoints[i].X - Radius * Math.Sin(i * AngularSpeed / 180.0 * Math.PI));
                int y = (int)Math.Round(controlPoints[i].Y - Radius * Math.Cos(i * AngularSpeed / 180.0 * Math.PI));
                var result = new SPoint(controlPoints[i]);
                result.X = x;
                result.Y = y;
                yield return result;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
