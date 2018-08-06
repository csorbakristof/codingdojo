using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using _20180806SpirographLib;

namespace _20180806SpirographTests
{
    /// <summary>
    /// Summary description for SPointTests
    /// </summary>
    [TestClass]
    public class SPointTests
    {
        [TestMethod]
        public void BasicOperations()
        {
            var sp = new SPoint(10,20);
            Assert.AreEqual(10, sp.X);
            Assert.AreEqual(20, sp.Y);
        }
    }
}
