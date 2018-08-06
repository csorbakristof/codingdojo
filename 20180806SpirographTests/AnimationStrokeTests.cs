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

        [TestMethod]
        public void AnimateInterpolatingStroke()
        {
            SPoint StartPointInFrame0 = new SPoint(0, 0);
            SPoint StartPointInFrame10 = new SPoint(0, 10);
            SPoint EndPointInFrame0 = new SPoint(100, 0);
            SPoint EndPointInFrame10 = new SPoint(100, 10);
            var controlPointAnimation = new AnimationStroke();
            var s1 = StrokeFactory.CreateInterpolatingStroke(new SPoint[] { StartPointInFrame0, StartPointInFrame10 });
            controlPointAnimation.AddStrokeAsTimeDependentControlPoint(s1);
            var s2 = StrokeFactory.CreateInterpolatingStroke(new SPoint[] { EndPointInFrame0, EndPointInFrame10 });
            controlPointAnimation.AddStrokeAsTimeDependentControlPoint(s2);
            var s = new InterpolatingStroke(controlPointAnimation);

            for(int t=0; t<=10; t++)
            {
                Assert.IsTrue(controlPointAnimation.NextFrame());
                var currentPoints = s.ToArray();
                AssertAllPointsInHorizontalLine(currentPoints, t, 0, 100);
            }
            Assert.IsFalse(controlPointAnimation.NextFrame());
        }

        private void AssertAllPointsInHorizontalLine(SPoint[] currentPoints, int y, int x0, int x1)
        {
            for (int x = x0; x <= x1; x++)
                PointListsHelper.AssertPointPresence(currentPoints, new SPoint(x, y), 1);
        }
    }
}
