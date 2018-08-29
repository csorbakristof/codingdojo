using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace _20180829Mastermind
{
    [TestClass]
    public class BasicTests
    {
        private Mastermind m = new Mastermind();

        [TestMethod]
        public void Instantiation()
        {
            AssertEq((0, 0), "", "");
        }

        [TestMethod]
        public void SingleCharPatternMatching_ReturnsBlack1()
        {
            AssertEq((1, 0), "A", "A");
        }

        [TestMethod]
        public void SingleCharPatternMismatch_ReturnsZeros()
        {
            AssertEq((0, 0), "A", "B");
        }

        [TestMethod]
        public void MatchingPattern_ReturnsAllBlack()
        {
            AssertEq((2, 0), "AA", "AA");
            AssertEq((3, 0), "ABC", "ABC");
        }

        [TestMethod]
        public void PartialOnlyBlackMatchingPattern_BlackIsCorrect()
        {
            AssertEq((2, 0), "ABXY", "ABZZ");
            AssertEq((2, 0), "ABXY", "ABZ");
            AssertEq((2, 0), "XAB", "ZAB");
        }


        [TestMethod]
        public void BlackMaskerEquals_MasksAll()
        {
            Assert.AreEqual(("___", 3), m.BlackMasked("ABC", "ABC"));
        }

        [TestMethod]
        public void BlackMaskerUnequals_MasksNone()
        {
            Assert.AreEqual(("XYZ", 0), m.BlackMasked("ABC", "XYZ"));
        }

        [TestMethod]
        public void BlackMasker_ReturnsCorrect()
        {
            Assert.AreEqual(("___ZZ", 3), m.BlackMasked("ABCXY", "ABCZZ"));
        }

        [TestMethod]
        public void SingleWhiteMatch_ReturnsWhite1()
        {
            AssertEq((0, 1), "XAB", "ZZA");
        }


        [TestMethod]
        public void WhiteMasked()
        {
            Assert.AreEqual(("_AYYY", 1), m.WhiteMasked("XXAXX", "AAYYY"));
            Assert.AreEqual(("YYY__", 2), m.WhiteMasked("ABXXX", "YYYAB"));
        }

        [TestMethod]
        public void MultiWhiteMatchWithUnequalNumberInPatternAndCorrect()
        {
            AssertEq((0, 1), "XXAXX", "AAYYY");
            AssertEq((0, 1), "XAAXX", "YYYAY");
        }


        [TestMethod]
        public void MixedTestsWithSameLength()
        {
            AssertEq((1, 1), "AAXXX", "AYYYAY");
            AssertEq((1, 1), "ABXXX", "AYYYBY");
            AssertEq((2, 2), "AABBXX", "AAYYBB");
            AssertEq((0, 1), "AXXXXX", "YYYYYA");

        }

        [TestMethod]
        public void MixedTestsWithUnequalLength()
        {
            AssertEq((2, 2), "AABBXXXX", "AAYYBB");
            AssertEq((2, 2), "AABBXX", "AAYYBBYY");
        }

        void AssertEq((int, int) result, string pattern, string correct)
        {
            Assert.AreEqual(result, m.Eval(pattern, correct));
        }
    }
}
