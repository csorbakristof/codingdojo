using System;
using System.Collections.Specialized;
using System.Linq;

namespace _20180829RomanNumbers
{
    internal class Converter
    {
        readonly (char L, int V)[] letters = new(char, int)[] {
            ('I',1),('V',5),('X',10),('L',50),('C',100),('D',500),('M',1000) };

        internal int Convert(string v)
        {
            int[] seq = v.ToCharArray().Select(c => letters.Single(l => l.L == c).V ).ToArray();
            for(int i=0; i<seq.Length; i++)
                if (i < seq.Length - 1 && seq[i] < seq[i + 1])
                    seq[i] = -seq[i];
            return seq.Sum();
        }
    }
}