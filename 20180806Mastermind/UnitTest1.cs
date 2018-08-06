using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace _20180806Mastermind
{
    [TestClass]
    public class UnitTest1
    {
        private MMind m = new MMind();

        [TestMethod]
        public void EmptyString_ReturnsZeroZero()
        {
            AssertResponse("", "", 0, 0);
        }

        [TestMethod]
        public void MatchingString_ReturnsLengthAsBlack()
        {
            AssertResponse("AABC", "AABC", 4, 0);
        }

        [TestMethod]
        public void DifferentCodeLength_ThrowsException()
        {
            try
            {
                AssertResponse("AB", "A", 0, 0);
                Assert.Fail();
            }
            catch(ArgumentException a)
            {
                Assert.IsTrue(a.Message.StartsWith("Code length"));
            }
        }


        [TestMethod]
        public void OnlyExactMatches_ReturnsCorrectCount()
        {
            AssertResponse("ABCD", "ABXY", 2, 0);
        }

        [TestMethod]
        public void ExactMatches_Marked()
        {
            AssertExactMatchMarking("ABC", "ABC", "___", "___", 3);
        }

        [TestMethod]
        public void ExactMatches_Marked2()
        {
            AssertExactMatchMarking("ABYC", "ABXC", "__Y_", "__X_", 3);
        }

        [TestMethod]
        public void SingleWhiteMatch_Found()
        {
            AssertWhiteMatch("A", "XYZA", 1);
        }

        [TestMethod]
        public void MultipleWhiteMatches_Found()
        {
            AssertWhiteMatch("AB", "XYZAB", 2);
        }

        [TestMethod]
        public void CollidingWhiteMatches_Found()
        {
            AssertWhiteMatch("ABB", "XYZAB", 2);
        }

        [TestMethod]
        public void SingleMisplacedMatch()
        {
            AssertResponse("ABCD", "XYZA", 0, 1);
        }

        [TestMethod]
        public void FullTests()
        {
            AssertResponse("AZZB", "ABXY", 1, 1);
            AssertResponse("ACDD", "AXYA", 1, 0);
            AssertResponse("BA", "AB", 0, 2);
            AssertResponse("XY", "AB", 0, 0);
            AssertResponse("YYAAAA", "AAXXXX", 0, 2);
        }


        [TestMethod]
        public void FullTests2()
        {
            AssertResponse("YYYYAA", "AAAXXX", 0, 2);
        }

        private void AssertWhiteMatch(string guess, string correct, int white)
        {
            Assert.AreEqual(white, m.MaskWhiteMatches(guess.ToCharArray(), correct.ToCharArray()));
        }

        private void AssertExactMatchMarking(string guess, string correct, string markedGuess, string markedCorrect, int correctBlackCount)
        {
            var c = correct.ToCharArray();
            var g = guess.ToCharArray();
            int b = m.MaskExactMatches(c, g);
            Assert.AreEqual(markedCorrect, new string(c));
            Assert.AreEqual(markedGuess, new string(g));
            Assert.AreEqual(correctBlackCount, b);

        }

        private void AssertResponse(string guess, string correct, int correctBlack, int correctWhite)
        {
            (int black, int white) = m.Eval(correct, guess);
            Assert.AreEqual(correctBlack, black);
            Assert.AreEqual(correctWhite, white);
        }

        private class MMind
        {
            public MMind()
            {
            }

            internal (int black, int white) Eval(string correct, string guess)
            {
                if (correct.Length != guess.Length)
                    throw new ArgumentException("Code length mismatch.");
                var c = correct.ToCharArray();
                var g = guess.ToCharArray();
                int black = MaskExactMatches(c, g);
                int white = MaskWhiteMatches(g, c);
                return (black, white);
            }

            internal int MaskExactMatches(char[] correct, char[] guess)
            {
                int n = 0;
                for (int i = 0; i < guess.Length; i++)
                    if (correct[i] == guess[i])
                    {
                        n++;
                        guess[i] = '_';
                        correct[i] = '_';
                    }
                return n;
            }

            internal int MaskWhiteMatches(char[] guess, char[] correct)
            {
                int n = 0;
                foreach (char g in guess)
                    if (g != '_')
                        for (int j = 0; j < correct.Length; j++)
                            if (g == correct[j])
                            {
                                n++;
                                correct[j] = '_';
                                break;
                            }

                return n;
            }
        }
    }
}
