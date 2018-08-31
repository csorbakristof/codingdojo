using System;
using System.Linq;
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
            AssertEval("C2 C3 D4 D6 HT", (Poker.Rank.HighCards, "HT"));
        }

        const string testFlush = "C2 C3 C4 C6 CT";
        [TestMethod]
        public void RecognizeFlush()
        {
            AssertEval(testFlush, (Poker.Rank.Flush, "CT"));
            Assert.AreNotEqual(Poker.Rank.Flush, Poker.Eval("C2 D3 C4 S6 CT").rank);
        }

        const string testStraight = "C7 D8 S9 ST CJ";
        [TestMethod]
        public void RecognizeStraight()
        {
            AssertEval(testStraight, (Poker.Rank.Straight, "CJ"));
            Assert.AreNotEqual(Poker.Rank.Straight, Poker.Eval("C2 D3 S4 S6 CT").rank);
        }


        const string testPair = "C2 D3 S3 HJ HA";
        [TestMethod]
        public void RecognizePair()
        {
            AssertEval(testPair, (Poker.Rank.Pair, "3"));
        }

        const string testTwoPairs = "C2 D3 S3 HJ CJ";
        [TestMethod]
        [Ignore]
        public void RecognizeTwoPairs()
        {
            AssertEval(testTwoPairs, (Poker.Rank.TwoPairs, "J"));
        }

        private void AssertEval(string hand, (Poker.Rank rank, string value) correct)
        {
            Assert.AreEqual(correct, Poker.Eval(hand));
        }
    }


    internal static class Poker
    {
        public enum Rank
        {
            HighCards, Pair, TwoPairs, ThreeOfAKind, Straight, Flush, FullHouse, FourOfAKind, StraightFlush
        }

        static public (Rank rank, string value) Eval(string hand)
        {
            var h = GetHand(hand);
            var rank = Rank.HighCards;
            string value = h[0].ToString();
            var v = IsPair(h);
            if (v != null)
            {
                value = v;
                rank = Rank.Pair;
            }
            if (IsStraight(h))
                rank = Rank.Straight;
            if (IsFlush(h))
                rank = Rank.Flush;
            return (rank, value);
        }

        private static string IsPair(Card[] h)
        {
            for (int i = 0; i < 4; i++)
                if (h[i].Value == h[i + 1].Value)
                    return h[i].Value.ToString();
            return null;
        }

        private static bool IsStraight(Card[] h)
        {
            for (int i = 0; i < 4; i++)
                if (h[i].CompareTo(h[i + 1]) != 1)
                    return false;
            return true;
        }

        private static bool IsFlush(Card[] cards)
        {
            for (int i = 1; i < 5; i++)
                if (!cards[0].SuitEquals(cards[i]))
                    return false;
            return true;
        }

        internal static Card[] GetHand(string hand)
        {
            string[] cardStrings = hand.Split(' ');
            var cards = cardStrings.Select(s => new Card(s)).OrderByDescending(c=>c).ToArray();
            return cards;
        }
    }
}
