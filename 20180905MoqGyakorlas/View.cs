using System;

namespace _20180905MoqGyakorlas
{
    internal class View : IView
    {
        public View()
        {
        }

        public int GetFrame(int frameIndex)
        {
            throw new ArgumentException($"Invalid frame index: {frameIndex}");
        }
    }
}