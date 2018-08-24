using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace _20180824BankOcr
{
    /// <summary>
    /// Summary description for UserStory4Tests
    /// </summary>
    [TestClass]
    public class UserStory4Tests
    {
        private Recognizer r = new Recognizer();

        [TestMethod]
        public void SingleCharacterMistake_ReturnsCorrect()
        {
            Assert.AreEqual(Patterns.correctCodeForPatternWithSingleCharMistake,
                r.GetCorrectedCode(Patterns.patternWithSingleCharMistake));
        }

        // Ran out of time for today, missing:
        // - Check for ambiguous correction (multiple options found)
        // - Add AMB case to report output
    }
}
