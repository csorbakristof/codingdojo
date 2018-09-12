using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace _20180905PeerReviewBingo
{
    [TestClass]
    public class BasicTests
    {
        readonly Bingo b = new Bingo();
        [TestMethod]
        public void BasicTest()
        {
            const int peerCount = 4;
            const int reviewerCount = 2;
            var bc = new BingoChecker(reviewerCount);
            b.random = new Random(1);
            b.Init(peerCount);
            b.ReviewNum = reviewerCount;
            var m = b.GetMatrix();
            Assert.IsTrue(bc.IsValid(m));
        }

        [TestMethod]
        public void AddingRow_UpdatesRowAndColSums()
        {
            b.Init(10);
            Assert.AreEqual(10, b.ColumnSum.Length);
            for (int i = 0; i < 10; i++)
            {
                Assert.AreEqual(0, b.ColumnSum[i]);
                Assert.AreEqual(0, b.RowSum[i]);
            }
            b.SetElement(2,3);
            Assert.AreEqual(1, b.ColumnSum[3]);
            Assert.AreEqual(1, b.RowSum[2]);
            Assert.AreEqual(0, b.ColumnSum[2]);
            Assert.AreEqual(0, b.RowSum[3]);
            b.SetElement(2,3);
            Assert.AreEqual(2, b.ColumnSum[3]);
            Assert.AreEqual(2, b.RowSum[2]);
            Assert.AreEqual(0, b.ColumnSum[2]);
            Assert.AreEqual(0, b.RowSum[3]);
        }

        [TestMethod]
        public void EnableMaskTest()
        {
            b.Init(5);
            b.ColumnSum[1] = 2;
            b.ColumnSum[2] = 1;
            b.RowSum[1] = 1;
            b.RowSum[2] = 2;
            Assert.IsTrue(b.IsEnabled(1, 2, 2));
            Assert.IsFalse(b.IsEnabled(2, 1, 2));
        }

        [TestMethod]
        public void RandomizeLocationsToEnabledPlaces()
        {
            b.Init(5);
            b.ReviewNum = 2;
            b.ColumnSum = new int[5] { 2, 1, 2, 2, 2 };
            b.RowSum = new int[5] { 2, 1, 2, 2, 2 };
            b.random = new Random(1);
            var location = b.GetRandomAllowedLocation(b.ReviewNumWithDiagonal);
            Assert.AreEqual(1, location.row);
            Assert.AreEqual(1, location.col);
        }
    }
}
