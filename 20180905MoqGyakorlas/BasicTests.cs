using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace _20180905MoqGyakorlas
{
    [TestClass]
    public class BasicTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Basics()
        {
            IView view = new View();
            view.GetFrame(-1);
        }

        [TestMethod]
        [ExpectedException(typeof(Moq.MockException))]
        public void CheckMockedInstance()
        {
            var mock = new Moq.Mock<IView>();
            mock.Setup(i => i.GetFrame(1)).Returns(2);
            Assert.AreEqual(2, mock.Object.GetFrame(1));
            mock.Setup(i => i.GetFrame(1)).Returns(2);
            Assert.AreNotEqual(2, mock.Object.GetFrame(2));
            mock.Verify(i => i.GetFrame(2));
            mock.Verify(i => i.GetFrame(3));
        }
    }
}
