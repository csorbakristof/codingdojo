using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20180806SpirographLib
{
    public class AnimationStroke : PolyStroke
    {
        private List<IEnumerator<SPoint>> enumerators = new List<IEnumerator<SPoint>>();

        public void AddStrokeAsTimeDependentControlPoint(IStroke stroke)
        {
            enumerators.Add(stroke.GetEnumerator());
        }

        public bool NextFrame()
        {
            points.Clear();
            foreach(var e in enumerators)
            {
                if (!e.MoveNext()) return false;
                points.Add(e.Current);
            }
            return true;
        }
    }
}
