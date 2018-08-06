using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20180806SpirographLib
{
    public class StrokeFactory
    {
        public static Stroke CreateInterpolatingStroke(SPoint[] controlPoints, int NumberOfIterations=1)
        {
            var ps = new PolyStroke(controlPoints);
            ps.NumberOfIterations = NumberOfIterations;
            return new InterpolatingStroke(ps);
        }

        public static Stroke CreateRotatingStroke(SPoint[] controlPoints, double angularSpeed, double radius, int NumberOfIterations = 1)
        {
            var ps = new PolyStroke(controlPoints);
            ps.NumberOfIterations = NumberOfIterations;
            return new RotatingStroke(
                ps,
                angularSpeed, radius);
        }
    }
}
