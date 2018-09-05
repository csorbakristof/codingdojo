using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace _20180905PeerReviewBingo
{
    /// <summary>
    /// Summary description for BingoCheckTests
    /// </summary>
    [TestClass]
    public class BingoCheckTests
    {
        [TestMethod]
        public void BingoCheckGetsNonrectMatrix_ReturnsInvalid()
        {
            AssertInvalid(new bool[2, 3]);
        }

        private readonly bool[,] correctSingleReviewMatrix = new bool[3, 3] {
            { false, false, true },
            { true, false, false },
            { false, true, false }
        };

        private readonly bool[,] correctDualReviewMatrix = new bool[3, 3] {
            { false, true, true },
            { true, false, true },
            { true, true, false }
        };

        private readonly BingoChecker bc = new BingoChecker(1);
        [TestMethod]
        public void CheckingAValid_ReturnsValid()
        {
            AssertValid(correctSingleReviewMatrix);
        }

        [TestMethod]
        public void CheckingWithTrueInDiagonal_ReturnsInvalid()
        {
            correctSingleReviewMatrix[1, 1] = true;
            Assert.IsFalse(bc.IsValid(correctSingleReviewMatrix));
        }

        [TestMethod]
        public void CheckReviewCountsPerSource()
        {
            bc.ReviwerPerTarget = 1;
            AssertValid(correctSingleReviewMatrix);
            correctSingleReviewMatrix[0, 1] = true;
            Assert.IsFalse(bc.IsValid(correctSingleReviewMatrix));
            correctSingleReviewMatrix[0, 1] = false;
            correctSingleReviewMatrix[0, 2] = false;
            Assert.IsFalse(bc.IsValid(correctSingleReviewMatrix));
            bc.ReviwerPerTarget = 2;
            AssertValid(correctDualReviewMatrix);
        }

        [TestMethod]
        public void CheckSameRowsCameCols_ReturnInvalid()
        {
            bool[,] mSameRows = new bool[3, 3] {
                { false, false, true },
                { false, false, true },
                { false, true, false } };
            bool[,] mSameCols = new bool[3, 3] {
                { false, true,  true },
                { true,  false, false },
                { false, false, false } };
            AssertInvalid(mSameRows);
            AssertInvalid(mSameCols);
        }

        #region Assert helpers
        private void AssertValid(bool[,] m)
        {
            Assert.IsTrue(bc.IsValid(m));
        }

        private void AssertInvalid(bool[,] m)
        {
            Assert.IsFalse(bc.IsValid(m));
        }
        #endregion
    }
}
