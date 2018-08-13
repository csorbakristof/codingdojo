using System.Linq;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Diagnostics;

namespace _20180813Linq
{
    [TestClass]
    public class Practice1
    {
        private IEnumerable<Circle> circles;
        public Practice1()
        {
            circles = CreateCircles();
        }

        [TestMethod]
        public void CreateDatasource()
        {
            Assert.AreEqual(7, circles.Count());
        }


        [TestMethod]
        public void FindCirclesWithRadiusAtLeast5()
        {
            var res = circles.Where(c => c.Radius >= 5).Count();
            Assert.AreEqual(4, res);
        }

        [TestMethod]
        public void CreateSortedValueList()
        {
            var list = circles.Select(c => c.Value).OrderBy(v => v).ToList();
            Assert.IsTrue(Enumerable.SequenceEqual(new int[] { 0, 1, 1, 1, 2, 3, 10 }, list));
        }


        [TestMethod]
        public void SquaredValueSum()
        {
            var sum = circles.Aggregate(0, (aggreg, c) => aggreg + (c.Value * c.Value));
            Assert.AreEqual(116, sum);
        }


        [TestMethod]
        public void SameRadiusPairs()
        {
            var circles2 = CreateCircles();
            var closePairs = circles.Join(
                circles2,
                c1 => c1.Radius,
                c2 => c2.Radius,
                (c1, c2) => (c1, c2)).ToList();

            Assert.AreEqual(9, closePairs.Count);
        }


        [TestMethod]
        public void ClosePairsInDescartesProduct()
        {
            const int maxDistanceSquare = 10*10;
            var circles2 = CreateCircles();
            var closePairs = circles
                .SelectMany(c1 => circles2.Select(c2 => (c1, c2)))
                .Where(pair => !pair.c1.Equals(pair.c2))
                .Where(p=>AreClose(p.c1, p.c2, maxDistanceSquare))
                .Where(p=>p.c1.Value < p.c2.Value)
                .Select(pair => (pair.c1.Value, pair.c2.Value))
                .ToList();

            Assert.AreEqual(4, closePairs.Count);
            Assert.IsTrue(closePairs.Contains((0, 1)));
            Assert.IsTrue(closePairs.Contains((1, 2)));
            Assert.IsTrue(closePairs.Contains((2, 3)));
            Assert.IsTrue(closePairs.Contains((0, 3)));
        }

        private bool AreClose(Circle a, Circle b, int maxDistanceSquare)
        {
            var distSquare = (a.X - b.X) * (a.X - b.X) + (a.Y - b.Y) * (a.Y - b.Y);
            return distSquare <= maxDistanceSquare;
        }

        // Group by
        [TestMethod]
        public void GroupByRadius()
        {
            var res = circles.GroupBy(
                c => c.Radius,
                c => (c.Radius, c))
                .ToList();

            Assert.AreEqual(6, res.Count());
            Assert.AreEqual(1, res.Where(r=>r.Key==5).Count());
            Assert.AreEqual(2, res.Where(r => r.Key == 8).First().Count());
        }

        private IEnumerable<Circle> CreateCircles()
        {
            // Upper left cluster (sum value 6)
            yield return new Circle(20, 20, 5, 0);
            yield return new Circle(30, 20, 4, 1);
            yield return new Circle(30, 30, 3, 2);
            yield return new Circle(20, 30, 2, 3);

            // Upper right cluster (center distance 20, distance 4, sum value 2)
            yield return new Circle(120, 20, 8, 1);
            yield return new Circle(120, 40, 8, 1);

            // Lower right lonely circle (value 10)
            yield return new Circle(120, 120, 10, 10);
        }

        class Circle
        {
            public Circle()
            {
            }

            public Circle(int x, int y, int r, int v)
            {
                X = x;
                Y = y;
                Radius = r;
                Value = v;
            }

            public int X { get; set; }
            public int Y { get; set; }
            public int Radius { get; set; }
            public int Value { get; set; }

            public override string ToString()
            {
                return $"C({X};{Y};r{Radius})";
            }

            public override bool Equals(object other)
            {
                var o = other as Circle;
                if (o==null)
                    return base.Equals(other);
                else
                    return X == o.X && Y == o.Y
                        && Radius == o.Radius && Value == o.Value;
            }
        }
    }
}
