using OpenCvSharp;
using System.Collections.Generic;
using System.Linq;

namespace _20180806SpirographLib
{
    public class InterpolatingStroke : PolyStroke
    {
        private PolyStroke delegateStroke;
        public InterpolatingStroke(PolyStroke delegateStroke)
        {
            this.delegateStroke = delegateStroke;
        }

        public void Interpolate()
        {
            var controlPoints = delegateStroke.ToList();
            points.Clear();
            for (int i = 0; i < controlPoints.Count - 1; i++)
                AddPointsBetween(controlPoints[i], controlPoints[i + 1], (i != 0));
        }

    }
}
