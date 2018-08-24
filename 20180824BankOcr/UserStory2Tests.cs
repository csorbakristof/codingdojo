using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace _20180824BankOcr
{
    /// <summary>
    /// Summary description for UserStory2Tests
    /// </summary>
    [TestClass]
    public class UserStory2Tests
    {
        private Recognizer r = new Recognizer();

        [TestMethod]
        public void CheckSum_CalculatesCorrectly()
        {
            Assert.AreEqual(0, r.CalculateChecksum("000000000"));
            Assert.AreEqual(1, r.CalculateChecksum("100000000"));
            Assert.AreEqual((1+2+3+4+5+6+7+8+9)%11, r.CalculateChecksum("111111111"));
            Assert.AreEqual(2 * (1 + 2 + 3 + 4 + 5 + 6 + 7 + 8 + 9) % 11,
                r.CalculateChecksum("222222222"));
            Assert.AreEqual((1 + 2*2 + 3*3 + 4*4 + 5*5 + 6*6 + 7*7 + 8*8 + 9*9) % 11,
                r.CalculateChecksum("123456789"));
        }
    }
}
