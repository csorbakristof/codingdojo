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

        const string testTerc = "C2 SK D9 C9 H9";
        [TestMethod]
        public void RecognizeTerc()
        {
            AssertEval(testTerc, Poker.Rank.Terc);
        }

        const string testStraightFlush = "C2 C3 C4 C5 C6";
        [TestMethod]
        public void RecognizeStraightFlush()
        {
            AssertEval(testStraightFlush, Poker.Rank.StraightFlush);
        }


        [TestMethod]
        public void ComparisonOfDifferents()
        {
            Assert.IsTrue(Poker.IsBiggerCompare(testStraightFlush, testTerc));
            Assert.IsTrue(Poker.IsBiggerCompare(testTerc, testTwoPairs));
            Assert.IsTrue(Poker.IsBiggerCompare(testTwoPairs, testPair));
            Assert.IsTrue(Poker.IsBiggerCompare(testFlush, testStraight));
            Assert.IsTrue(Poker.IsBiggerCompare(testStraightFlush, testFlush));
        }


        [TestMethod]
        public void ComparisonOfSameRank()
        {
            // High cards, Ace vs King
            Assert.IsTrue(Poker.IsBiggerCompare("CA CJ C8 D4 S9", "CK C7 C8 D4 S9"));
            // Flush, Ace vs King
            Assert.IsTrue(Poker.IsBiggerCompare("CA CJ C8 C4 C9", "CK C7 C8 C4 C9"));
            // Two pairs, second differs
            Assert.IsTrue(Poker.IsBiggerCompare("CA SA C8 S8 D3", "CA SA C7 S7 D3"));
            // Two pairs, 5th card differs
            Assert.IsTrue(Poker.IsBiggerCompare("CA SA C8 S8 D3", "CA SA C8 S8 D2"));
        }

        [TestMethod]
        public void ComparisonOfSameHands()
        {
            Assert.IsFalse(Poker.IsBiggerCompare(testPair, testPair));
        }

        [TestMethod]
        public void EqualityAndHashCode()
        {
            (var dummy, Card[] c) = Poker.Eval(testStraightFlush);
            Assert.AreNotEqual(c[0].GetHashCode(), c[1].GetHashCode());
            Assert.IsFalse(c[0].Equals(c[1]));
            Assert.IsFalse(c[0].Equals(new object()));
        }

        [TestMethod]
        public void TestToString()
        {
            Assert.AreEqual("S9", (new Card() { Suit = 'S', Value = '9' }).ToString());
        }

        private void AssertEval(string hand, Poker.Rank correctRank)
        {
            Assert.AreEqual(correctRank, Poker.Eval(hand).rank);
        }
    }
}
