using System;
using System.Linq;

namespace _20180831PokerHands
{
    internal static class Poker
    {
        public enum Rank
        {
            HighCards, Pair, TwoPairs, Terc, Straight, Flush, FullHouse, FourOfAKind, StraightFlush
        }

        static public (Rank rank, Card[] cards) Eval(string hand)
        {
            var h = GetHand(hand);
            if (IsStraightFlush(h))
                return (Rank.StraightFlush, h);
            if (IsStraight(h))
                return (Rank.Straight, h);
            if (IsFlush(h))
                return (Rank.Flush, h);
            if (IsTerc(h))
                return (Rank.Terc, h);
            if (IsTwoPair(h))
                return (Rank.TwoPairs, h);
            if (IsPair(h))
                return (Rank.Pair, h);
            return (Rank.HighCards, h);
        }

        private static bool IsStraightFlush(Card[] h)
        {
            return IsStraight(h) && IsFlush(h);
        }

        private static bool IsTerc(Card[] h)
        {
            for (int i = 0; i < 3; i++)
                if (h[i].Value == h[i + 2].Value) // Note: h is ordered
                    return true;
            return false;
        }

        private static bool IsTwoPair(Card[] h)
        {
            int firstIndexOfPair1 = -1;
            int firstIndexOfPair2 = -1;
            for (int i = 0; i < 4; i++)
                if (h[i].Value == h[i + 1].Value)
                {
                    if (firstIndexOfPair1 == -1)
                        firstIndexOfPair1 = i;
                    else if (h[i].Value != h[firstIndexOfPair1].Value)
                        firstIndexOfPair2 = i;
                    else
                        return false;    // This is ThreeOfAKind (but will not get here...)
                }
            if (firstIndexOfPair2 != -1)
                return true;
            return false;
        }

        private static bool IsPair(Card[] h)
        {
            for (int i = 0; i < 4; i++)
                if (h[i].Value == h[i + 1].Value)
                    return true;
            return false;
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

        internal static bool IsBiggerCompare(string bigger, string smaller)
        {
            var b = Eval(bigger);
            var s = Eval(smaller);
            if (b.rank != s.rank)
                return b.rank > s.rank;
            for (int i = 0; i < 5; i++)
                if (!b.cards[i].ValueEquals(s.cards[i]))
                    return b.cards[i].CompareTo(s.cards[i]) > 0;
            return false;   // Equals means not bigger...
        }
    }
}
