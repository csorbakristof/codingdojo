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
        public void PointByPointDrawing()
        {
            Mat img = new Mat(100, 100, MatType.CV_8UC3, new Scalar(0, 0, 0));
            var d = new Drawer();
            Vec3b color = new Vec3b(128, 192, 255);
            Vec3b black = new Vec3b(0, 0, 0);
            var s = new PolyStroke(new SPoint[] {
                new SPoint(10,20,color), new SPoint(20,20,color) });
            d.Draw(img, s, false);
            var indexer = img.GetGenericIndexer<Vec3b>();
            Assert.AreEqual(black, indexer[10, 10]);
            Assert.AreEqual(color, indexer[20, 10]);
            Assert.AreEqual(black, indexer[20, 15]);
            Assert.AreEqual(color, indexer[20, 20]);
        }

        [TestMethod]
        public void ConnectingDrawing()
        {
            Mat img = new Mat(100, 100, MatType.CV_8UC3, new Scalar(0, 0, 0));
            var d = new Drawer();
            Vec3b color = new Vec3b(128, 192, 255);
            var s = new PolyStroke(new SPoint[] {
                new SPoint(10,10,color), new SPoint(20,10,color) });
            d.Draw(img, s, true);
            var indexer = img.GetGenericIndexer<Vec3b>();
            Assert.AreEqual(new Vec3b(0, 0, 0), indexer[50, 50]);
            Assert.AreEqual(color, indexer[10, 10]);
            Assert.AreEqual(color, indexer[10, 15]);
        }

        [TestMethod]
        public void ConnectingDrawingLineWidthCheck()
        {
            Mat img = new Mat(100, 100, MatType.CV_8UC3, new Scalar(0, 0, 0));
            var d = new Drawer();
            Vec3b color = new Vec3b(128, 192, 255);
            var s = new PolyStroke(new SPoint[] {
                new SPoint(10,10,color,3), new SPoint(20,10,color,3) });
            d.Draw(img, s, true);
            var indexer = img.GetGenericIndexer<Vec3b>();
            Assert.AreEqual(new Vec3b(0, 0, 0), indexer[50, 50]);
            Assert.AreEqual(color, indexer[9, 10]);
            Assert.AreEqual(color, indexer[11, 15]);
            Assert.AreEqual(color, indexer[11, 15]);
        }
    }
}
