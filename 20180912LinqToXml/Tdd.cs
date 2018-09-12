using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace _20180912LinqToXml
{
    // Tests used as TDD during the development of the solution
    [TestClass]
    public class Tdd
    {
        private readonly Solutions s = new Solutions(@"rectangles.svg");

        [TestMethod]
        public void All()
        {
        }
    }
}
