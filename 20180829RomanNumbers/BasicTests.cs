using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace _20180829RomanNumbers
{
    [TestClass]
    public class BasicTests
    {
        readonly Converter c = new Converter();

        [TestMethod]
        public void Instantiation()
        {
            AssertEq(0, "");
        }


        [TestMethod]
        public void CountsI()
        {
            AssertEq(1, "I");
            AssertEq(2, "II");
            AssertEq(3, "III");
        }

        [TestMethod]
        public void RecognizeAllLettersByThemselves()
        {
            AssertEq(1, "I");
            AssertEq(5, "V");
            AssertEq(10, "X");
            AssertEq(50, "L");
            AssertEq(100, "C");
            AssertEq(500, "D");
            AssertEq(1000, "M");
        }


        [TestMethod]
        public void RecognizeSingleCompositeNumbers()
        {
            AssertEq(4, "IV");
            AssertEq(9, "IX");
            AssertEq(40, "XL");
            AssertEq(90, "XC");
            AssertEq(400, "CD");
            AssertEq(900, "CM");
        }


        [TestMethod]
        public void RecognizeArbitraryNumbers()
        {
            AssertEq(2018, "MMXVIII");
            AssertEq(1980, "MCMLXXX");
            AssertEq(1987, "MCMLXXXVII");
            AssertEq(58, "LVIII");
        }

        private void AssertEq(int value, string roman)
        {
            Assert.AreEqual(value, c.Convert(roman));
        }
    }
}
