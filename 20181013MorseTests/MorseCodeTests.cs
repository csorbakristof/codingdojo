using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _20181013MorseUi;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting.AppContainer;
using Windows.UI.Xaml.Media;

namespace _20181013MorseTests
{
    [TestClass]
    public class MorseCodeTests
    {
        private UpdateMorseCodeCommand cmd = new UpdateMorseCodeCommand(null);

        [UITestMethod]
        public void GetMorseCode()
        {
            Assert.AreEqual("", cmd.GetMorseCodeDotString(""));
            Assert.AreEqual("...", cmd.GetMorseCodeDotString("s"));
            Assert.AreEqual("---", cmd.GetMorseCodeDotString("o"));
            Assert.AreEqual("... --- ...", cmd.GetMorseCodeDotString("sos"));
        }

        [UITestMethod]
        public void EmptyResponseForTextWithUnknownCharacter()
        {
            Assert.AreEqual("", cmd.GetMorseCodeDotString("é"));
        }

        [UITestMethod]
        public void ConvertMorseCode()
        {
            cmd.InterTextClearance = 10.0;
            AssertDoubleCollection(new DoubleCollection() { 2 }, "-");
            AssertDoubleCollection(new DoubleCollection() { 1, 1, 1, 1, 1 }, "...");
            AssertDoubleCollection(new DoubleCollection() { 2, 1, 2, 1, 2 }, "---");
        }

        private void AssertDoubleCollection(DoubleCollection doubles, string text)
        {
            var result = cmd.GetStrokeLengths(text);
            Assert.IsTrue(Enumerable.SequenceEqual(doubles, result));
        }
    }
}
