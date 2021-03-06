﻿using System;
using System.Collections.Generic;
using OpenCvSharp;

namespace _20180806SpirographLib
{
    public class SPoint
    {
        public int X { get; set; }
        public int Y { get; set; }
        public Vec3b Color;
        public int LineWidth = 1;

        public SPoint(int x, int y)
        {
            X = x;
            Y = y;
            Color = new Vec3b(255, 255, 255);
        }

        public SPoint(int x, int y, Vec3b color, int lineWidth=1)
        {
            X = x;
            Y = y;
            Color = color;
            LineWidth = lineWidth;
        }

        public SPoint(SPoint other)
        {
            X = other.X;
            Y = other.Y;
            Color = other.Color;
            LineWidth = other.LineWidth;
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
            return (X == other.X && Y == other.Y && LineWidth==other.LineWidth
                && Color.Item0 == other.Color.Item0
                && Color.Item1 == other.Color.Item1
                && Color.Item2 == other.Color.Item2);
        }

        public bool EqualsPosition(SPoint other)
        {
            return (X == other.X && Y == other.Y);
        }

        public override int GetHashCode()
        {
            var hashCode = -196163389;
            hashCode = hashCode * -1521134295 + X.GetHashCode();
            hashCode = hashCode * -1521134295 + Y.GetHashCode();
            hashCode = hashCode * -1521134295 + EqualityComparer<Vec3b>.Default.GetHashCode(Color);
            return hashCode;
        }

        public override string ToString()
        {
            return $"SPoint({X},{Y},color({Color.Item0},{Color.Item1},{Color.Item2}),width({LineWidth}))";
        }
    }
}
