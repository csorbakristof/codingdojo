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
            Assert.AreEqual("000000000", r.Eval(all0));
        }

        [TestMethod]
        public void All1_EvalsCorrectly()
        {
            Assert.AreEqual("111111111", r.Eval(all1));
        }

        [TestMethod]
        public void StartsWith1_ExtractsDigitCorrectly()
        {
            Assert.AreEqual(digit1, r.ExtractDigit(all1,0));
        }

        [TestMethod]
        public void StartsWith2_ExtractsDigitCorrectly()
        {
            Assert.AreEqual(digit2, r.ExtractDigit(all2,0));
        }

        [TestMethod]
        public void CanExtractArbitraryDigit()
        {
            Assert.AreEqual(digit1, r.ExtractDigit(sequencePattern, 0));
            Assert.AreEqual(digit2, r.ExtractDigit(sequencePattern, 1));
        }


        [TestMethod]
        public void ExtractsInternalDigitPatternsCorrectly()
        {
            Assert.AreEqual(digit1, r.ExtractDigit(Recognizer.numberSequence, 1, 10));
            Assert.AreEqual(digit2, r.ExtractDigit(Recognizer.numberSequence, 2, 10));
        }

        [TestMethod]
        public void CanRecognizeDigit2()
        {
            Assert.AreEqual("2", r.RecognizeDigit(digit2));

        }

        [TestMethod]
        public void All2_EvalsCorrectly()
        {
            Assert.AreEqual("222222222", r.Eval(all2));
        }


        [TestMethod]
        public void SequencePattern_EvalsCorrectly()
        {
            Assert.AreEqual("123456789", r.Eval(sequencePattern));
        }

        #region Patterns
        private string all0 =
            " _  _  _  _  _  _  _  _  _ \n" +
            "| || || || || || || || || |\n" +
            "|_||_||_||_||_||_||_||_||_|\n" + "\n";
        private string all1 =
            "                           \n" +
            "  |  |  |  |  |  |  |  |  |\n" +
            "  |  |  |  |  |  |  |  |  |\n" + "\n";
        private string all2 =
            " _  _  _  _  _  _  _  _  _ \n" +
            " _| _| _| _| _| _| _| _| _|\n" +
            "|_ |_ |_ |_ |_ |_ |_ |_ |_ \n" + "\n";
        private string sequencePattern =
            "    _  _     _  _  _  _  _ \n" +
            "  | _| _||_||_ |_   ||_||_|\n" +
            "  ||_  _|  | _||_|  ||_| _|\n" + "\n";

        private string digit1 =
            "   " +
            "  |" +
            "  |";
        private string digit2 =
            " _ " +
            " _|" +
            "|_ ";

        #endregion



    }
}
