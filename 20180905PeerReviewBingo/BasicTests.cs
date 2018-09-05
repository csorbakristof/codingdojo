using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace _20180905PeerReviewBingo
{
    [TestClass]
    public class BasicTests
    {
        [TestMethod]
        public void BasicTest()
        {
            var b = new Bingo();
            var mtx = b.GetMatrix(0);
            Assert.IsNull(mtx);
        }
    }

    internal class Bingo
    {
        public Bingo()
        {
        }

        internal object GetMatrix(int n)
        {
            return null;
        }
    }
}
