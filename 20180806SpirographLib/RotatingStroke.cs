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

        public IEnumerator<SPoint> GetEnumerator()
        {
            var controlPoints = delegateStroke.ToList();
            for (int i = 0; i < controlPoints.Count; i++)
            {
                int x = (int)Math.Round(controlPoints[i].X - radius * Math.Sin(i * angularSpeed / 180.0 * Math.PI));
                int y = (int)Math.Round(controlPoints[i].Y - radius * Math.Cos(i * angularSpeed / 180.0 * Math.PI));
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
