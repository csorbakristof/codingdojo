using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace _20180912LinqToXml
{
    // Tests used as TDD during the development of the solution
    [TestClass]
    public class BasicTests
    {
        private Solutions s = new Solutions(@"..\..\rectangles.svg");
        [TestMethod]
        public void TestReadXml()
        {
            Assert.AreEqual("topLevelGroup", s.GetIfOfTopLevelGroup());
        }

        [TestMethod]
        public void EnumerateAllElements()
        {
            Assert.AreEqual(13, s.GetAllElements(nodename => (nodename=="rect" || nodename == "text")).Count());
        }

        [TestMethod]
        public void CountTexts()
        {
            Assert.AreEqual(3, s.GetAllElements(nodename => nodename == "text").Count(e => e.Value == "Alma"));
            Assert.AreEqual(1, s.GetAllElements("text").Count(e => e.Value == "Körte"));
        }
    }
}
