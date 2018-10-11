using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20180806SpirographLib
{
    public static class StrokeFactory
    {
        public static IStroke CreateInterpolatingStroke(SPoint[] controlPoints, int numberOfIterations=1)
        {
            var ps = new PolyStroke(controlPoints);
            ps.NumberOfIterations = numberOfIterations;
            return new InterpolatingStroke(ps);
        }

        public static IStroke CreateRotatingStroke(SPoint[] controlPoints, double angularSpeed, double radius, int NumberOfIterations = 1)
        {
            var ps = new PolyStroke(controlPoints);
            ps.NumberOfIterations = NumberOfIterations;
            return new RotatingStroke(
                ps,
                angularSpeed, radius);
        }
    }
}
