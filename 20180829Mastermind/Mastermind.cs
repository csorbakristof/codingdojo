using System;

namespace _20180829Mastermind
{
    internal class Mastermind
    {
        public Mastermind()
        {
        }

        internal (int, int) Eval(string pattern, string correct)
        {
            (string correctWithMaskedBlacks, int b) =
                MaskMatchesInCorrect(pattern, correct, false);
            (string correctWithMaskedMatches, int w) =
                MaskMatchesInCorrect(pattern, correctWithMaskedBlacks, true);
            return (b, w);
        }

        internal (string maskedCorrect, int num) MaskMatchesInCorrect(string pattern, string correct, bool allowWhiteMatching)
        {
            var chars = correct.ToCharArray();
            int num = 0;
            for (int pIdx = 0; pIdx < pattern.Length; pIdx++)
            {
                int? matchPos = allowWhiteMatching ?
                    doesMatchWhite(pattern[pIdx], chars) :
                    doesMatchBlack(pattern[pIdx], chars, pIdx);
                if (matchPos.HasValue)
                {
                    num++;
                    chars[matchPos.Value] = '_';
                }
            }
            return (new string(chars), num);
        }

        private int? doesMatchWhite(char p, char[] chars)
        {
            for (int i = 0; i < chars.Length; i++)
            {
                if (chars[i] == p)
                    return i;
            }
            return null;
        }

        private int? doesMatchBlack(char p, char[] chars, int idx)
        {
            if (idx >= chars.Length)
                return null;
            return (p == chars[idx] ? idx : (int?)null);
        }

    }
}