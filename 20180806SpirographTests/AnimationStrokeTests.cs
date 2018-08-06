using _20180806SpirographLib;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20180806SpirographTests
{
    [TestClass]
    public class AnimationStrokeTests
    {
        [TestMethod]
        public void SimpleAnimation()
        {
            SPoint A1 = new SPoint(10, 10);
            SPoint A2 = new SPoint(20, 10);
            SPoint B1 = new SPoint(10, 50);
            SPoint B2 = new SPoint(20, 50);
            var anim = new AnimationStroke();
            var s1 = new PolyStroke(new SPoint[] { A1, A2 });
            anim.AddStrokeAsTimeDependentControlPoint(s1);
            var s2 = new PolyStroke(new SPoint[] { B1, B2 });
            anim.AddStrokeAsTimeDependentControlPoint(s2);
            var hadMore = anim.NextFrame();
            Assert.IsTrue(hadMore);
            PointListsHelper.AssertPointPresence(anim,
                new SPoint[] { A1, B1 });
            hadMore = anim.NextFrame();
            Assert.IsTrue(hadMore);
            PointListsHelper.AssertPointPresence(anim,
                new SPoint[] { A2, B2 });
            Assert.IsFalse(anim.NextFrame());
        }
    }
}
