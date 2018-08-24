﻿using System;
using System.Linq;

namespace _20180824BankOcr
{
    internal class Recognizer
    {
        public Recognizer()
        {
        }

        internal string Eval(string pattern)
        {
            if (pattern.Length != 85)
                throw new ArgumentException();
            string res = "";
            for (int i = 0; i < 9; i++)
                res += RecognizeDigit(ExtractDigit(pattern, i));
            return res;
        }

        internal string ExtractDigit(string pattern, int idx, int sequenceLength=9)
        {
            int step = sequenceLength * 3 + 1;
            string line1 = pattern.Substring(0+3*idx, 3);
            string line2 = pattern.Substring(step + 3 * idx, 3);
            string line3 = pattern.Substring(2 * step + 3 * idx, 3);
            return line1 + line2 + line3;
        }

        internal bool IsChecksumValid(string code)
        {
            return (CalculateChecksum(code) == 0);
        }

        internal int CalculateChecksum(string value)
        {
            int[] d = value.ToCharArray()
                .Select(c=>(int)Char.GetNumericValue(c)).Reverse().ToArray();
            int sum = 0;
            for (int i = 0; i < 9; i++)
                sum += (i + 1) * d[i];
            return sum % 11;
        }

        internal string GetStatus(string pattern)
        {
            var eval = Eval(pattern);
            if (eval.Contains("?"))
                return "ILL";
            if (!IsChecksumValid(eval))
                return "ERR";
            return "";
        }

        internal string GetReport(string pattern)
        {
            var eval = Eval(pattern);
            var status = GetStatus(pattern);
            return eval + (status == "" ? "" : " " + status);
        }

        internal string RecognizeDigit(string patternOfDigit)
        {
            for (int i = 0; i < 10; i++)
                if (patternOfDigit == ExtractDigit(numberSequence, i, 10))
                    return i.ToString();
            return "?";
        }

        public const string numberSequence =
            " _     _  _     _  _  _  _  _ \n" +
            "| |  | _| _||_||_ |_   ||_||_|\n" +
            "|_|  ||_  _|  | _||_|  ||_| _|\n" + "\n";

    }
}