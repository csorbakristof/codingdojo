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
            (string correctWithMaskedBlacks, int b) = BlackMasked(pattern, correct);
            (string correctWithMaskedMatches, int w) = WhiteMasked(pattern, correctWithMaskedBlacks);
            return (b, w);
        }

        internal (string maskedCorrect, int black) BlackMasked(string pattern, string correct)
        {
            var chars = correct.ToCharArray();
            int b = 0;
            for (int i = 0; i < checkLength(pattern, correct); i++)
                if (pattern[i] == correct[i])
                {
                    b++;
                    chars[i] = '_';
                }
            return (new string(chars), b);
        }

        private static int checkLength(string pattern, string correct)
        {
            return Math.Min(pattern.Length, correct.Length);
        }

        internal (string maskedCorrect, int white) WhiteMasked(string pattern, string correct)
        {
            var chars = correct.ToCharArray();
            int w = 0;
            for (int pIdx = 0; pIdx < pattern.Length; pIdx++)
            {
                for (int cIdx = 0; cIdx < correct.Length; cIdx++)
                {
                    if (chars[cIdx] == pattern[pIdx])
                    {
                        w++;
                        chars[cIdx] = '_';
                        break;
                    }
                }
            }
            return (new string(chars), w);
        }
    }
}