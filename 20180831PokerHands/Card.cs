﻿using System;

namespace _20180831PokerHands
{
    internal class Card : IComparable
    {
        private static char[] values = new char[] { '2', '3', '4', '5', '6', '7', '8', '9', 'T', 'J', 'Q', 'K', 'A' };

        public Card()
        {
        }

        public Card(string card)
        {
            Suit = card[0];
            Value = card[1];
        }

        public char Suit { get; set; }
        public char Value { get; set; }

        public override bool Equals(object obj)
        {
            if (!(obj is Card o))
                return false;
            return (this.Suit == o.Suit && this.Value == o.Value);
        }

        public override int GetHashCode()
        {
            var hashCode = -1625629942;
            hashCode = hashCode * -1521134295 + Suit.GetHashCode();
            hashCode = hashCode * -1521134295 + Value.GetHashCode();
            return hashCode;
        }

        internal bool SuitEquals(Card other)
        {
            return (Suit == other.Suit);
        }

        public int CompareTo(object obj)
        {
            // Positive return value means this > obj.
            return Array.IndexOf(values, this.Value) -
                Array.IndexOf(values, (obj as Card).Value);
        }

        internal bool ValueEquals(Card other)
        {
            return this.CompareTo(other) == 0;
        }

        public override string ToString()
        {
            return $"{Suit}{Value}";
        }
    }
}
