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
            Assert.AreEqual(true, r.IsChecksumValid(Patterns.codeWithCorrectChecksum));
            Assert.AreEqual(false, r.IsChecksumValid(Patterns.codeWithWrongChecksum));
        }

        [TestMethod]
        public void InternalTest_PatternWithCorrectChecksum_IsCorrect()
        {
            Assert.AreEqual(Patterns.codeWithCorrectChecksum,
                r.Eval(Patterns.patternWithCorrectChecksum));
        }

        [TestMethod]
        public void UnrecognizedDigitMarkedWithQuestionmark()
        {
            Assert.AreEqual(Patterns.codeWithWrong3And7,
                r.Eval(Patterns.patternWithWrong3And7));
        }

        [TestMethod]
        public void StatusReturnedCorrectly()
        {
            Assert.AreEqual("", r.GetStatus(Patterns.patternWithCorrectChecksum));
            Assert.AreEqual("ERR", r.GetStatus(Patterns.patternWithWrongChecksum));
            Assert.AreEqual("ILL", r.GetStatus(Patterns.patternWithWrong3And7));
        }

        [TestMethod]
        public void ReportIsCorrect()
        {
            Assert.AreEqual(Patterns.codeWithCorrectChecksum,
                r.GetReport(Patterns.patternWithCorrectChecksum));
            Assert.AreEqual(Patterns.codeWithWrongChecksum +" ERR",
                r.GetReport(Patterns.patternWithWrongChecksum));
            Assert.AreEqual(Patterns.codeWithWrong3And7 +" ILL",
                r.GetReport(Patterns.patternWithWrong3And7));
        }
    }
}
