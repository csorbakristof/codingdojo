using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _20180806SpirographLib
{
    public class SPoint
    {
        public int X { get; set; }
        public int Y { get; set; }

        public SPoint(int x, int y)
        {
            X = x;
            Y = y;
        }

        public static bool operator==(SPoint obj1, SPoint obj2)
        {
            return obj1.Equals(obj2);
        }

        public static bool operator!=(SPoint obj1, SPoint obj2)
        {
            return !obj1.Equals(obj2);
        }

        public override bool Equals(object obj)
        {
            SPoint other = obj as SPoint;
            return (X == other.X && Y == other.Y);
        }
    }
}
