using Microsoft.VisualStudio.TestTools.UnitTesting;
using _20180806SpirographLib;
using OpenCvSharp;

namespace _20180806SpirographTests
{
    /// <summary>
    /// Summary description for DrawerTests
    /// </summary>
    [TestClass]
    public class DrawerTests
    {
        [TestMethod]
        public void BasicDrawing()
        {
            Mat img = new Mat(100, 100, MatType.CV_8UC3, new Scalar(0, 0, 0));
            var d = new Drawer();
            Vec3b color = new Vec3b(128, 192, 255);
            var s = new PolyStroke(new SPoint[] {
                new SPoint(10,20,color), new SPoint(20,30,color) });
            d.Draw(img, s);
            var indexer = img.GetGenericIndexer<Vec3b>();
            Assert.AreEqual(new Vec3b(0,0,0), indexer[10, 10]);
            Assert.AreEqual(color, indexer[20, 10]);
            Assert.AreEqual(color, indexer[30, 20]);
        }
    }
}
