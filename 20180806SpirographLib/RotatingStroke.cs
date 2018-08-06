using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace _20180806SpirographLib
{
    public class RotatingStroke : PolyStroke
    {
        private PolyStroke delegateStroke;
        public RotatingStroke(PolyStroke delegateStroke)
        {
            this.delegateStroke = delegateStroke;
        }

        public void Rotate(double angularSpeed, double radius)
        {
            var controlPoints = delegateStroke.ToList();
            points.Clear();
            for (int i = 0; i < controlPoints.Count; i++)
            {
                double x = Math.Round(controlPoints[i].X - radius * Math.Sin(i * angularSpeed / 180.0 * Math.PI));
                double y = Math.Round(controlPoints[i].Y - radius * Math.Cos(i * angularSpeed / 180.0 * Math.PI));
                points.Add(new Point(x, y));
            }
        }
    }
}
