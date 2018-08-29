using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace _20180829RomanNumbers
{
    /// <summary>
    /// Summary description for ReverseTests
    /// </summary>
    [TestClass]
    public class ReverseTests
    {
        readonly Converter c = new Converter();

        [TestMethod]
        public void RecognizesSingleRomanLetters()
        {
            AssertEq("I", 1);
            AssertEq("V", 5);
            AssertEq("X", 10);
            AssertEq("L", 50);
            AssertEq("C", 100);
            AssertEq("D", 500);
            AssertEq("M", 1000);
        }

        [TestMethod]
        public void ConcatenatesSingleLetterNumbers()
        {
            AssertEq("III", 3);
            AssertEq("XVIII", 18);
        }


        [TestMethod]
        public void Recognizes4()
        {
            AssertEq("IV", 4);
        }

        private void AssertEq(string correct, int value)
        {
            Assert.AreEqual(correct, c.Convert(value));
        }
    }
}
