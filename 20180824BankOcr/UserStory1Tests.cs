using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace _20180824BankOcr
{
    [TestClass]
    public class UserStory1Tests
    {
        private Recognizer r = new Recognizer();
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void InproperInputFormat_ThrowsException()
        {
            r.Eval("");
        }


        [TestMethod]
        public void All0_EvalsCorrectly()
        {
            Assert.AreEqual("000000000", r.Eval(Patterns.all0));
        }

        [TestMethod]
        public void All1_EvalsCorrectly()
        {
            Assert.AreEqual("111111111", r.Eval(Patterns.all1));
        }

        [TestMethod]
        public void StartsWith1_ExtractsDigitCorrectly()
        {
            Assert.AreEqual(Patterns.digit1, r.ExtractDigit(Patterns.all1,0));
        }

        [TestMethod]
        public void StartsWith2_ExtractsDigitCorrectly()
        {
            Assert.AreEqual(Patterns.digit2, r.ExtractDigit(Patterns.all2,0));
        }

        [TestMethod]
        public void CanExtractArbitraryDigit()
        {
            Assert.AreEqual(Patterns.digit1, r.ExtractDigit(Patterns.sequencePattern, 0));
            Assert.AreEqual(Patterns.digit2, r.ExtractDigit(Patterns.sequencePattern, 1));
        }


        [TestMethod]
        public void ExtractsInternalDigitPatternsCorrectly()
        {
            Assert.AreEqual(Patterns.digit1, r.ExtractDigit(Recognizer.numberSequence, 1, 10));
            Assert.AreEqual(Patterns.digit2, r.ExtractDigit(Recognizer.numberSequence, 2, 10));
        }

        [TestMethod]
        public void CanRecognizeDigit2()
        {
            Assert.AreEqual("2", r.RecognizeDigit(Patterns.digit2));

        }

        [TestMethod]
        public void All2_EvalsCorrectly()
        {
            Assert.AreEqual("222222222", r.Eval(Patterns.all2));
        }


        [TestMethod]
        public void SequencePattern_EvalsCorrectly()
        {
            Assert.AreEqual("123456789", r.Eval(Patterns.sequencePattern));
        }
    }
}
