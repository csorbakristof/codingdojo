using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace _20180912LinqToXml
{
    // The tasks to solve during the laboratory.
    //  Avoid unnecessary code duplication!
    [TestClass]
    public class TasksToComplete
    {
        private readonly Solutions s;

        public TasksToComplete()
        {
            s = new Solutions("rectangles.svg");
        }

        [TestMethod]
        public void CountTextsWithValue()
        {
            Assert.AreEqual(3, s.CountTextsWithValue("Alma"));
            Assert.AreEqual(1, s.CountTextsWithValue("Négyzetek"));
            Assert.AreEqual(1, s.CountTextsWithValue("Barack"));
            Assert.AreEqual(1, s.CountTextsWithValue("Körte"));
        }

        [TestMethod]
        public void GetAllRectangles()
        {
            Assert.AreEqual(7, s.GetAllRectangles().Count());
        }

        [TestMethod]
        public void GetRectanglesWithTextInside()
        {
            Assert.AreEqual(3, s.GetRectanglesWithTextInside().Count());
        }

        [TestMethod]
        public void GetRectanglesWithStrokeWidth()
        {
            Assert.AreEqual(2, s.GetRectanglesWithStrokeWidth(1).Count());
            Assert.AreEqual(5, s.GetRectanglesWithStrokeWidth(2).Count());
        }

        [TestMethod]
        public void GetColorOfRectanglesWithGivenX()
        {
            var correctColors = new string[] { "#ff0000", "#0000ff", "#ffffff" };
            var colors = s.GetColorOfRectanglesWithGivenX(30);
            Assert.IsTrue(UnorderedCompareSequences(correctColors, colors));
        }

        [TestMethod]
        public void GetSingleTextInSingleRectangleWithColor()
        {
            Assert.AreEqual("Barack", s.GetSingleTextInSingleRectangleWithColor("#ff00ff"));
            Assert.AreEqual(null, s.GetSingleTextInSingleRectangleWithColor("#00ff00"));
            Assert.AreEqual("Alma", s.GetSingleTextInSingleRectangleWithColor("#ffffff"));
        }

        [TestMethod]
        public void GetSingleRectanglePairCloseToEachOther()
        {
            (string id1, string id2) = s.GetSingleRectanglePairCloseToEachOther(5.0);
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
        public void GetRectangleLocationById()
        {
            (double x, double y) = s.GetRectangleLocationById("rectTeal");
            Assert.AreEqual(80.0, x, 0.001);
            Assert.AreEqual(180.0, y, 0.001);
        }

        [TestMethod]
        public void GetIdOfRectangeWithLargestY()
        {
            Assert.AreEqual("rectTeal", s.GetIdOfRectangeWithLargestY());
        }

        [TestMethod]
        public void GetRectanglesAtLeastTwiceAsHighAsWide()
        {
            var ids = s.GetRectanglesAtLeastTwiceAsHighAsWide().ToArray();
            Assert.AreEqual(2, ids.Length);
            Assert.IsTrue(ids.Contains("rectBlue"));
            Assert.IsTrue(ids.Contains("rectGreen"));
        }

        [TestMethod]
        public void GetTextsOutsideRectangles()
        {
            var correctTexts = new string[] { "Alma", "Körte", "Négyzetek" };
            Assert.IsTrue(UnorderedCompareSequences(correctTexts, s.GetTextsOutsideRectangles()));
        }

        [TestMethod]
        public void GetBoundingRectangleColorListForEveryText()
        {
            var result = s.GetBoundingRectangleColorListForEveryText();
            Assert.IsTrue(UnorderedCompareSequences<string>(new string[] { "#ffff00", "#ffffff" }, result["Alma"]));
            Assert.IsTrue(UnorderedCompareSequences<string>(new string[] { "#ff00ff" }, result["Barack"]));
            Assert.IsFalse(result.Contains("Szilva"));
            Assert.IsFalse(result["Körte"].Any());
        }

        [TestMethod]
        public void GetEffectiveWidthAndHeightWithGivenStrokeThickness()
        {
            (double w1, double h1) = s.GetEffectiveWidthAndHeight(1);
            Assert.AreEqual(110.0, w1, 0.001);
            Assert.AreEqual(110.0, h1, 0.001);
            (double w2, double h2) = s.GetEffectiveWidthAndHeight(2);
            Assert.AreEqual(140.0, w2, 0.001);
            Assert.AreEqual(190.0, h2, 0.001);
        }

        #region Helpers for the unit tests and their tests
        [TestMethod]
        public void TestUnorderedCompareSequences()
        {
            Assert.IsTrue(UnorderedCompareSequences(new int[] {1, 2, 3}, new int[] {1, 2, 3}));
            Assert.IsTrue(UnorderedCompareSequences(new int[] {1, 2, 3}, new int[] {1, 3, 2}));
            Assert.IsTrue(UnorderedCompareSequences(new int[] {1, 2, 2}, new int[] {2, 1, 2}));
            Assert.IsFalse(UnorderedCompareSequences(new int[] { 1 }, new int[] { 1, 2 }));
            Assert.IsFalse(UnorderedCompareSequences(new int[] { 1 }, new int[] { 2, 3}));
        }

        private bool UnorderedCompareSequences<T>(IEnumerable<T> s1, IEnumerable<T> s2)
        {
            return s1.OrderBy(i => i).SequenceEqual(s2.OrderBy(j => j));
        }
        #endregion
    }
}
