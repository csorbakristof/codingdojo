
using System;
using _20181013MorseUi;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Microsoft.VisualStudio.TestTools.UnitTesting.AppContainer;
using Windows.UI.Xaml.Media;

namespace _20181013MorseTests
{
    [TestClass]
    public class ViewModelTests
    {
        private MainViewModel vm;
        private UpdateMorseCodeCommand cmd;

        public ViewModelTests()
        {
            vm = new MainViewModel();
            cmd = new UpdateMorseCodeCommand(vm);
        }

        private readonly DoubleCollection SOS = new DoubleCollection()
            { 1,1, 1,1, 1,3, 2,1, 2,1, 2,3, 1,1, 1,1, 1,10};

        [UITestMethod]
        public void Sos()
        {
            cmd.InterTextClearance = 10.0;
            cmd.Execute("sos");
            Assert.IsTrue(System.Linq.Enumerable.SequenceEqual(SOS, vm.MorseLineDashArray));
        }
    }
}
