using System;

namespace _20180808Mastermind
{
    internal class Mastermind
    {
        public Mastermind()
        {
        }

        internal (int black, int white) Eval(string correct, string guess)
        {
            int b = 0;
            int w = 0;
            var g = guess.ToCharArray();
            var c = correct.ToCharArray();
            b = CountAndMarkBlacks(g, c);
            w = CountAndMarkWhites(g, c);
            return (b, w);
        }

        private static int CountAndMarkWhites(char[] g, char[] c)
        {
            int w = 0;
            for (int i = 0; i < g.Length; i++)
            {
                if (g[i] == '_')
                    continue;
                for (int j = 0; j < c.Length; j++)
                {
                    if (c[j] == g[i])
                    {
                        c[j] = '_';
                        w++;
                        break;
                    }
                }
            }
            return w;
        }

        private static int CountAndMarkBlacks(char[] g, char[] c)
        {
            int b = 0;
            for (int i = 0; i < Math.Min(g.Length, c.Length); i++)
                if (g[i] == c[i])
                {
                    b++;
                    g[i] = '_';
                    c[i] = '_';
                }
            return b;
        }
    }
}