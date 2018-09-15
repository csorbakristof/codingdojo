using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace _20180912LinqToXml
{
    [TestClass]
    public class TaskTests
    {
        private readonly Solutions s1;
        private readonly Solutions s2;

        public TaskTests()
        {
            s1 = new Solutions("rectangles1.svg");
            s2 = new Solutions("rectangles2.svg");
        }

        [TestMethod]
        public void CountTextsWithValue()
        {
            Assert.AreEqual(1, s1.CountTextsWithValue("Négyzetek"));
            Assert.AreEqual(3, s1.CountTextsWithValue("Alma"));
            Assert.AreEqual(1, s1.CountTextsWithValue("Barack"));
            Assert.AreEqual(1, s1.CountTextsWithValue("Körte"));

            Assert.AreEqual(1, s2.CountTextsWithValue("Gyümölcsök"));
            Assert.AreEqual(3, s2.CountTextsWithValue("Szilva"));
            Assert.AreEqual(3, s2.CountTextsWithValue("Barack"));
            Assert.AreEqual(1, s2.CountTextsWithValue("Banán"));
            Assert.AreEqual(2, s2.CountTextsWithValue("Meggy"));
        }

        [TestMethod]
        public void GetAllRectangles()
        {
            Assert.AreEqual(7, s1.GetAllRectangles().Count());
            Assert.AreEqual(12, s2.GetAllRectangles().Count());
        }

        [TestMethod]
        public void GetRectanglesWithTextInside()
        {
            Assert.AreEqual(3, s1.GetRectanglesWithTextInside().Count());
            Assert.AreEqual(8, s2.GetRectanglesWithTextInside().Count());
        }

        [TestMethod]
        public void GetRectanglesWithStrokeWidth()
        {
            Assert.AreEqual(2, s1.GetRectanglesWithStrokeWidth(1).Count());
            Assert.AreEqual(5, s1.GetRectanglesWithStrokeWidth(2).Count());

            Assert.AreEqual(8, s2.GetRectanglesWithStrokeWidth(1).Count());
            Assert.AreEqual(4, s2.GetRectanglesWithStrokeWidth(2).Count());
        }

        [TestMethod]
        public void GetColorOfRectanglesWithGivenX()
        {
            var correctColors = new string[] { "#ff0000", "#0000ff", "#ffffff" };
            var colors = s1.GetColorOfRectanglesWithGivenX(30);
            Assert.IsTrue(UnorderedCompareSequences(correctColors, colors));

            correctColors = new string[] { "#ffffff", "#ff0000", "#ff0000", "#0000ff" };
            colors = s2.GetColorOfRectanglesWithGivenX(20);
            Assert.IsTrue(UnorderedCompareSequences(correctColors, colors));

        }

        [TestMethod]
        public void GetSingleTextInSingleRectangleWithColor()
        {
            Assert.AreEqual("Barack", s1.GetSingleTextInSingleRectangleWithColor("#ff00ff"));
            Assert.AreEqual(null, s1.GetSingleTextInSingleRectangleWithColor("#00ff00"));
            Assert.AreEqual("Alma", s1.GetSingleTextInSingleRectangleWithColor("#ffffff"));

            Assert.AreEqual("Meggy", s2.GetSingleTextInSingleRectangleWithColor("#ff00ff"));
            Assert.AreEqual("Szilva", s2.GetSingleTextInSingleRectangleWithColor("#ffffff"));
        }

        [TestMethod]
        public void GetSingleRectanglePairCloseToEachOther()
        {
            AssertIdPair("rectWhite", "rectBlue", s1.GetSingleRectanglePairCloseToEachOther(5.0));
            AssertIdPair("rectTeal", "rectGreen", s2.GetSingleRectanglePairCloseToEachOther(5.0));
        }

        private void AssertIdPair(string correctId1, string correctId2, (string id1, string id2) result)
        {
            Assert.IsTrue((result.id1 == correctId1 && result.id2 == correctId2)
                || (result.id1 == correctId2 && result.id2 == correctId1));
        }

        [TestMethod]
        public void ConcatenateOrderedTextsInsideRectangles()
        {
            string correctResult = "Alma, Alma, Barack";
            Assert.AreEqual(correctResult, s1.ConcatenateOrderedTextsInsideRectangles());

            correctResult = "Barack, Barack, Barack, Meggy, Meggy, Szilva, Szilva, Szilva";
            Assert.AreEqual(correctResult, s2.ConcatenateOrderedTextsInsideRectangles());
        }

        [TestMethod]
        public void GetRectangleLocationById()
        {
            AssertLocation(80.0, 180.0, s1.GetRectangleLocationById("rectTeal"));
            AssertLocation(20.0, 33.75, s2.GetRectangleLocationById("rectWhite"));
        }

        private void AssertLocation(double correctX, double correctY, (double x, double y) result)
        {
            Assert.AreEqual(correctX, result.x, 0.001);
            Assert.AreEqual(correctY, result.y, 0.001);
        }

        [TestMethod]
        public void GetIdOfRectangeWithLargestY()
        {
            Assert.AreEqual("rectTeal", s1.GetIdOfRectangeWithLargestY());
            Assert.AreEqual("rectLowerYellow", s2.GetIdOfRectangeWithLargestY());
        }

        [TestMethod]
        public void GetRectanglesAtLeastTwiceAsHighAsWide()
        {
            var ids = s1.GetRectanglesAtLeastTwiceAsHighAsWide().ToArray();
            Assert.AreEqual(2, ids.Length);
            Assert.IsTrue(ids.Contains("rectBlue"));
            Assert.IsTrue(ids.Contains("rectGreen"));

            ids = s2.GetRectanglesAtLeastTwiceAsHighAsWide().ToArray();
            Assert.AreEqual(1, ids.Length);
            Assert.AreEqual("rectLowerBlue", ids[0]);
        }

        [TestMethod]
        public void GetTextsOutsideRectangles()
        {
            var correctTexts = new string[] { "Alma", "Körte", "Négyzetek" };
            Assert.IsTrue(UnorderedCompareSequences(correctTexts, s1.GetTextsOutsideRectangles()));

            correctTexts = new string[] { "Banán", "Gyümölcsök" };
            Assert.IsTrue(UnorderedCompareSequences(correctTexts, s2.GetTextsOutsideRectangles()));
        }

        [TestMethod]
        public void GetBoundingRectangleColorListForEveryText()
        {
            var result = s1.GetBoundingRectangleColorListForEveryText();
            Assert.IsTrue(UnorderedCompareSequences<string>(new string[] { "#ffff00", "#ffffff" }, result["Alma"]));
            Assert.IsTrue(UnorderedCompareSequences<string>(new string[] { "#ff00ff" }, result["Barack"]));
            Assert.IsFalse(result.Contains("Szilva"));
            Assert.IsFalse(result["Körte"].Any());

            result = s2.GetBoundingRectangleColorListForEveryText();
            Assert.IsTrue(UnorderedCompareSequences<string>(new string[] { "#ffffff", "#00ffff", "#ffff00" }, result["Szilva"]));
            Assert.IsTrue(UnorderedCompareSequences<string>(new string[] { "#ff00ff", "#00ff00" }, result["Meggy"]));
            Assert.IsFalse(result.Contains("Alma"));
            Assert.IsFalse(result["Banán"].Any());

        }

        [TestMethod]
        public void GetEffectiveWidthAndHeightWithGivenStrokeThickness()
        {
            AssertWidthHeight(s1, 1, 110.0, 110.0);
            AssertWidthHeight(s1, 2, 140.0, 190.0);
            AssertWidthHeight(s2, 1, 170.0, 230.0);
            AssertWidthHeight(s2, 2, 170.0, 216.25);
        }

        private void AssertWidthHeight(Solutions s, int strokeThickness, double width, double height)
        {
            (double w, double h) = s.GetEffectiveWidthAndHeight(strokeThickness);
            Assert.AreEqual(width, w, 0.001);
            Assert.AreEqual(height, h, 0.001);
        }

        [TestMethod]
        public void GetColorsOfRectsInGroup()
        {
            var colors = s1.GetColorsOfRectsInGroup("group1");
            Assert.IsTrue(UnorderedCompareSequences<string>(new string[] { "#ff0000", "#ffff00" },
                colors));

            colors = s2.GetColorsOfRectsInGroup("group2");
            Assert.IsTrue(UnorderedCompareSequences<string>(new string[] { "#00ff00", "#0000ff" },
                colors));
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
