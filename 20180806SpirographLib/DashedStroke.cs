using System.Collections;
using System.Collections.Generic;
using _20180806SpirographLib;
using OpenCvSharp;

namespace _20180806SpirographTests
{
    public class DashedStroke : Stroke
    {
        private Stroke delegateStroke;
        private int periodLength;

        public DashedStroke(Stroke delegateStroke, int periodLength)
        {
            this.delegateStroke = delegateStroke;
            this.periodLength = periodLength;
        }

        public IEnumerator<SPoint> GetEnumerator()
        {
            int i = 0;
            foreach (var p in delegateStroke)
            {
                if (i % periodLength < periodLength / 2)
                    yield return p;
                i++;
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}