using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace _20180824BankOcr
{
    /// <summary>
    /// Summary description for UserStory3Tests
    /// </summary>
    [TestClass]
    public class UserStory3Tests
    {
        private Recognizer r = new Recognizer();

        [TestMethod]
        public void ChecksumCheckWorks()
        {
            Assert.AreEqual(true, r.IsChecksumValid("000000000"));
            Assert.AreEqual(true, r.IsChecksumValid(codeWithCorrectChecksum));
            Assert.AreEqual(false, r.IsChecksumValid(codeWithWrongChecksum));
        }

        [TestMethod]
        public void InternalTest_PatternWithCorrectChecksum_IsCorrect()
        {
            Assert.AreEqual(codeWithCorrectChecksum, r.Eval(patternWithCorrectChecksum));
        }

        [TestMethod]
        public void UnrecognizedDigitMarkedWithQuestionmark()
        {
            Assert.AreEqual(codeWithWrong3And7, r.Eval(patternWithWrong3And7));
        }

        [TestMethod]
        public void StatusReturnedCorrectly()
        {
            Assert.AreEqual("", r.GetStatus(patternWithCorrectChecksum));
            Assert.AreEqual("ERR", r.GetStatus(patternWithWrongChecksum));
            Assert.AreEqual("ILL", r.GetStatus(patternWithWrong3And7));
        }

        [TestMethod]
        public void ReportIsCorrect()
        {
            Assert.AreEqual(codeWithCorrectChecksum, r.GetReport(patternWithCorrectChecksum));
            Assert.AreEqual(codeWithWrongChecksum+" ERR", r.GetReport(patternWithWrongChecksum));
            Assert.AreEqual(codeWithWrong3And7+" ILL", r.GetReport(patternWithWrong3And7));
        }

        private string codeWithWrong3And7 = "12?456?89";
        private string patternWithWrong3And7 =
            "    _  _     _  _  _  _  _ \n" +
            "  | _| _ |_||_ |_   ||_||_|\n" +
            "  ||_  _|  | _||_| _||_| _|\n" + "\n";

        private const string codeWithCorrectChecksum = "345882865";
        private string patternWithCorrectChecksum =
            " _     _  _  _  _  _  _  _ \n" +
            " _||_||_ |_||_| _||_||_ |_ \n" +
            " _|  | _||_||_||_ |_||_| _|\n" + "\n";

        const string codeWithWrongChecksum = "111111111";
        private string patternWithWrongChecksum =
            "                           \n" +
            "  |  |  |  |  |  |  |  |  |\n" +
            "  |  |  |  |  |  |  |  |  |\n" + "\n";
    }
}
