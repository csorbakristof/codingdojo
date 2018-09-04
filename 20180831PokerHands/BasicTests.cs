using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace _20180831PokerHands
{
    [TestClass]
    public class BasicTests
    {
        // Clubs (treff), Diamonds (káró), Hearts (szív), Spades (pikk)
        // 2, 3, 4, 5, 6, 7, 8, 9, T (ten), J (Jack), Q (Queen), K (King), A (Ace)
        [TestMethod]
        public void CanCompareValues()
        {
            var pairs = new(Card h, Card l)[]
            {
                (new Card("CA"), new Card("CK")),
                (new Card("CJ"), new Card("CT")),
                (new Card("C6"), new Card("C5")),
                (new Card("C4"), new Card("C2")),
                (new Card("CK"), new Card("C3")),
                (new Card("CQ"), new Card("C7"))
            };
            foreach (var p in pairs)
            {
                Assert.IsTrue(p.l.CompareTo(p.h) < 0);
                Assert.IsTrue(p.h.CompareTo(p.l) > 0);
                Assert.IsTrue(p.l.CompareTo(p.l) == 0);
                Assert.IsTrue(p.h.CompareTo(p.h) == 0);
            }
        }

        [TestMethod]
        public void CanParse()
        {
            Assert.AreEqual(new Card() { Suit = 'C', Value = 'T' }, new Card("CT"));
        }

        [TestMethod]
        public void IsSameSuit()
        {
            Assert.IsTrue((new Card("CT")).SuitEquals(new Card("C8")));
        }

        [TestMethod]
        public void CanParseAndOrderHand()
        {
            string hand = "C3 D6 HT D4 C2";
            Card[] cards = Poker.GetHand(hand);
            Assert.AreEqual(5, cards.Length);
            Assert.AreEqual(new Card("HT"), cards[0]);
            Assert.AreEqual(new Card("D6"), cards[1]);
            Assert.AreEqual(new Card("D4"), cards[2]);
            Assert.AreEqual(new Card("C3"), cards[3]);
            Assert.AreEqual(new Card("C2"), cards[4]);
        }

        [TestMethod]
        public void RecognizeHighestCard()
        {
            AssertEval("C2 C3 D4 D6 HT", Poker.Rank.HighCards);
        }

        const string testFlush = "C2 C3 C4 C6 CT";
        [TestMethod]
        public void RecognizeFlush()
        {
            AssertEval(testFlush, Poker.Rank.Flush);
            Assert.AreNotEqual(Poker.Rank.Flush, Poker.Eval("C2 D3 C4 S6 CT").rank);
        }

        const string testStraight = "C7 D8 S9 ST CJ";
        [TestMethod]
        public void RecognizeStraight()
        {
            AssertEval(testStraight, Poker.Rank.Straight);
            Assert.AreNotEqual(Poker.Rank.Straight, Poker.Eval("C2 D3 S4 S6 CT").rank);
        }


        const string testPair = "C2 D3 S3 HJ HA";
        [TestMethod]
        public void RecognizePair()
        {
            AssertEval(testPair, Poker.Rank.Pair);
        }

        const string testTwoPairs = "C2 D3 S3 HJ CJ";
        [TestMethod]
        public void RecognizeTwoPairs()
        {
            AssertEval(testTwoPairs, Poker.Rank.TwoPairs);
        }

        private void AssertEval(string hand, Poker.Rank correctRank)
        {
            Assert.AreEqual(correctRank, Poker.Eval(hand).rank);
        }
    }
}
