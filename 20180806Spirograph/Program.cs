using _20180806SpirographLib;
using OpenCvSharp;

namespace _20180806Spirograph
{
    class Program
    {
        static void Main(string[] args)
        {
            Mat img = new Mat(1000,1000,MatType.CV_8UC3, new Scalar(0,0,0));
            Vec3b colorA = new Vec3b(255, 192, 128);
            Vec3b colorB = new Vec3b(128, 192, 255);
            var sPoly = new PolyStroke(new SPoint[] {
                new SPoint(200,200,colorA), new SPoint(800,200,colorB),
                new SPoint(800,800,colorA), new SPoint(200,800,colorB),
                new SPoint(200,200,colorA)});
            sPoly.NumberOfIterations = 3;
            var sInterp = new InterpolatingStroke(sPoly);
            var s = new RotatingStroke(sInterp, 10, 100);
            var drawer = new Drawer();
            drawer.Draw(img, s, true);
            Cv2.ImShow("Spirograph", img);
            Cv2.WaitKey(0);
        }
    }
}
