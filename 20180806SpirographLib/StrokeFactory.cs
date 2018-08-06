using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20180806SpirographLib
{
    public class StrokeFactory
    {
        public static Stroke CreateInterpolatingStroke(SPoint[] controlPoints)
        {
            return new InterpolatingStroke(new PolyStroke(controlPoints));
        }

        public static Stroke CreateRotatingStroke(SPoint[] controlPoints, double angularSpeed, double radius)
        {
            return new RotatingStroke(
                new PolyStroke(controlPoints),
                angularSpeed, radius);
        }
    }
}
