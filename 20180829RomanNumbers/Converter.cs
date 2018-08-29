using System;
using System.Collections.Specialized;
using System.Linq;

namespace _20180829RomanNumbers
{
    internal class Converter
    {
        readonly (char L, int V)[] letters = new(char, int)[] {
            ('I',1),('V',5),('X',10),('L',50),('C',100),('D',500),('M',1000) };

        readonly (string L, int V)[] extendedLetters = new(string, int)[] {
            ("CM",900),("CD",400),("XC",90),("XL",40),("IX",9),("IV",4) };

        internal int Convert(string v)
        {
            int[] seq = v.ToCharArray().Select(c => letters.Single(l => l.L == c).V ).ToArray();
            for(int i=0; i<seq.Length; i++)
                if (i < seq.Length - 1 && seq[i] < seq[i + 1])
                    seq[i] = -seq[i];
            return seq.Sum();
        }

        internal string Convert(int val)
        {
            var sb = new System.Text.StringBuilder();
            var mergedLetters = letters.Select(l=>(L:l.L.ToString(), V:l.V)).
                Concat(extendedLetters).OrderByDescending(l=>l.V);
            while (val > 0)
            {
                (var nextLetter, var nextValue) = mergedLetters.First(l => l.V <= val);
                val -= nextValue;
                sb.Append(nextLetter);
            }
            return sb.ToString();
        }
    }
}