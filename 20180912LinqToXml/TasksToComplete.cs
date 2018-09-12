using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Linq;

namespace _20180912LinqToXml
{
    // The tasks to solve during the laboratory.
    //  Avoid unnecessary code duplication!
    [TestClass]
    public class TasksToComplete
    {
        private Solutions s = new Solutions(@"..\..\rectangles.svg");

        [TestMethod]
        public void Texts()
        {
            Assert.AreEqual(3, s.CountTextsWithValue("Alma"));
            Assert.AreEqual(1, s.CountTextsWithValue("Négyzetek"));
            Assert.AreEqual(1, s.CountTextsWithValue("Barack"));
            Assert.AreEqual(1, s.CountTextsWithValue("Körte"));
        }

        [TestMethod]
        public void Rectangles()
        {
            Assert.AreEqual(7, s.GetAllRectangles().Count());
        }

        [TestMethod]
        public void RectanglesWithTextInside()
        {
            Assert.AreEqual(3, s.GetRectanglesWithTextInside().Count());
        }

        [TestMethod]
        public void StrokeWidth()
        {
            Assert.AreEqual(2, s.GetRectanglesWithStrokeWidth(1).Count());
            Assert.AreEqual(5, s.GetRectanglesWithStrokeWidth(2).Count());
        }

        [TestMethod]
        public void ColorOfRectanglesWithGivenX()
        {
            var correctColors = new string[] { "#ff0000", "#0000ff", "#ffffff" };
            Assert.IsTrue(correctColors.SequenceEqual(s.GetColorOfRectanglesWithGivenX(30)));
        }

        [TestMethod]
        public void TextInRectangleWithGivenColor()
        {
            Assert.AreEqual("Barack", s.GetTextInRectangleWithColor("#ff00ff"));
            Assert.AreEqual(null, s.GetTextInRectangleWithColor("#00ff00"));
            Assert.AreEqual("Alma", s.GetTextInRectangleWithColor("#ffffff"));
        }

        [TestMethod]
        public void GetRectanglesCloseToEachOther()
        {
            (string id1, string id2) = s.GetRectanglePairsCloseToEachOther();
            Assert.IsTrue((id1 == "rectBlue" && id2 == "rectWhite")
                || (id1 == "rectWhite" && id2 == "rectBlue"));
        }

        [TestMethod]
        public void ConcatenateOrderedTextsInsideRectangles()
        {
            string correctResult = "Alma, Alma, Barack";
            Assert.AreEqual(correctResult, s.ConcatenateOrderedTextsInsideRectangles());
        }

        [TestMethod]
        public void RectLocationById()
        {
            (double x, double y) = s.GetRectangleLocationById("rectTeal");
            Assert.AreEqual(80.0, x, 0.001);
            Assert.AreEqual(180.0, y, 0.001);
        }

        [TestMethod]
        public void RectWithLargestY()
        {
            Assert.AreEqual("rectTeal", s.GetIdOfRectangeWithLargestY());
        }

        [TestMethod]
        public void RectanglesAtLeastTwiceAsHighAsWide()
        {
            var ids = s.GetRectanglesAtLeastTwiceAsHighAsWide().ToArray();
            Assert.AreEqual(2, ids.Length);
            Assert.IsTrue(ids.Contains("rectBlue"));
            Assert.IsTrue(ids.Contains("rectGreen"));
        }

        [TestMethod]
        public void TextsOutsideRectangles()
        {
            var correctTexts = new string[] { "Alma", "Körte", "Négyzetek" };
            Assert.IsTrue(correctTexts.SequenceEqual(s.GetTextsOutsideRectangles().OrderBy(s=>s).ToArray()));
        }
    }
}
