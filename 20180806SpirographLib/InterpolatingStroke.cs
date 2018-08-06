using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20180806SpirographLib
{
    public class InterpolatingStroke : PolyStroke
    {
        public void Interpolate()
        {
            var controlPoints = points;
            points = new List<Point>();
            for (int i = 0; i < controlPoints.Count - 1; i++)
                AddPointsBetween(controlPoints[i], controlPoints[i + 1], (i != 0));
        }

    }
}
