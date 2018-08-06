using _20180806SpirographLib;
using OpenCvSharp;
using System;
using System.Linq;

namespace _20180806Spirograph
{
    class Program
    {
        static readonly Vec3b red = new Vec3b(128, 128, 255);
        static readonly Vec3b blue = new Vec3b(255, 128, 128);
        static readonly Vec3b yellow = new Vec3b(128, 255, 255);
        const char Key_Esc = (char)27;

        static void Main(string[] args)
        {
            Mat img = new Mat(1000,1000,MatType.CV_8UC3, new Scalar(0,0,0));

            const int NumberOfAnimationIterations = 10;
            const int NumberOfIterationsInAFrame = 3;
            const int NumberOfFullRotations = 50;
            var topLeftControlPoint = StrokeFactory.CreateInterpolatingStroke(
                new SPoint[] { new SPoint(100, 100, red), new SPoint(900, 100, yellow) }, NumberOfAnimationIterations);
            var topRightControlPoint = StrokeFactory.CreateInterpolatingStroke(
                new SPoint[] { new SPoint(900, 100, blue), new SPoint(900, 900, blue) }, NumberOfAnimationIterations);
            var bottomRightControlPoint = StrokeFactory.CreateInterpolatingStroke(
                new SPoint[] { new SPoint(900, 900, yellow), new SPoint(100, 900, red) }, NumberOfAnimationIterations);
            var bottomLeftControlPoint = StrokeFactory.CreateInterpolatingStroke(
                new SPoint[] { new SPoint(100, 900, blue), new SPoint(100, 100, blue) }, NumberOfAnimationIterations);

            var controlPointAnimation = new AnimationStroke();
            controlPointAnimation.AddStrokeAsTimeDependentControlPoint(topLeftControlPoint);
            controlPointAnimation.AddStrokeAsTimeDependentControlPoint(topRightControlPoint);
            controlPointAnimation.AddStrokeAsTimeDependentControlPoint(bottomRightControlPoint);
            controlPointAnimation.AddStrokeAsTimeDependentControlPoint(bottomLeftControlPoint);
            controlPointAnimation.AddStrokeAsTimeDependentControlPoint(topLeftControlPoint);
            controlPointAnimation.NumberOfIterations = NumberOfIterationsInAFrame;
            var strokeToDraw = new RotatingStroke(
                new InterpolatingStroke(controlPointAnimation), 2.0, 50.0);

            var drawer = new Drawer();
            while (controlPointAnimation.NextFrame())
            {
                img.SetTo(new Scalar(0, 0, 0));
                strokeToDraw.AngularSpeed = CalculateAngularVelocity(strokeToDraw.Count(), NumberOfFullRotations);
                drawer.Draw(img, strokeToDraw, true);
                Cv2.ImShow("Spirograph", img.Clone());
                if (Cv2.WaitKey(20) == Key_Esc)
                    break;
            }
        }

        private static double CalculateAngularVelocity(int numberOfPoints, int numberOfFullRotations)
        {
            return (360.0 * numberOfFullRotations) / numberOfPoints;
        }
    }
}
