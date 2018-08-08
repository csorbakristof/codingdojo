using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace _20180808Mastermind
{
    [TestClass]
    public class MastermindTests20180808
    {
        [TestMethod]
        public void ZeroResults()
        {
            AssertResponse("", "", 0, 0);
            AssertResponse("ABC", "", 0, 0);
            AssertResponse("ABC", "XYZ012", 0, 0);
        }


        [TestMethod]
        public void SingleCharacterMatch_ReturnsB1W0()
        {
            AssertResponse("A", "A", 1, 0);
        }


        [TestMethod]
        public void BlackMatches_CountedCorrectly()
        {
            AssertResponse("AB", "AB", 2, 0);
            AssertResponse("AX", "AY", 1, 0);
            AssertResponse("AXB", "AYBZ", 2, 0);
        }


        [TestMethod]
        public void UnequalLengthHandledCorrectly()
        {
            AssertResponse("XY", "ABCD", 0, 0);
            AssertResponse("ABCD", "XY", 0, 0);
            AssertResponse("XY", "", 0, 0);
            AssertResponse("", "ABCD", 0, 0);
        }

        [TestMethod]
        public void WhiteMatchesOnly_CountedCorrectly()
        {
            AssertResponse("AB", "BA", 0, 2);
            AssertResponse("ACB", "CBA", 0, 3);
            AssertResponse("ACX", "CYA", 0, 2);
        }


        [TestMethod]
        public void FurtherTests()
        {
            AssertResponse("AXBXCXD", "AYBYYC", 2, 1);
            AssertResponse("A", "YA", 0, 1);
            AssertResponse("ABC", "ABC", 3, 0);
        }
        private void AssertResponse(string correct, string guess, int correctBlack, int correctWhite)
        {
            var mm = new Mastermind();
            (int black, int white) = mm.Eval(correct, guess);
            Assert.AreEqual(correctBlack, black);
            Assert.AreEqual(correctWhite, white);
        }
    }
}