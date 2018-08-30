using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace _20180829PrimeFactors
{
    [TestClass]
    public class BasicTests
    {
        [TestMethod]
        public void Start() => AssertEq(1, new int[] { 1 });

        [TestMethod]
        public void Test2() => AssertEq(2, new int[] { 2 });

        [TestMethod]
        public void Test4() => AssertEq(4, new int[] { 2, 2 });

        [TestMethod]
        public void Test6() => AssertEq(6, new int[] { 2, 3 });

        [TestMethod]
        public void FurtherTests()
        {
            AssertEq(5, new int[] { 5 });
            AssertEq(9, new int[] { 3, 3 });
            AssertEq(27, new int[] { 3, 3, 3 });
            AssertEq(2*2*3*3*5*7*13, new int[] { 2, 2, 3, 3, 5, 7, 13 });
        }

        private void AssertEq(int num, int[] primeFactors)
        {
            var p = new PrimeFactors();
            Assert.IsTrue(Enumerable.SequenceEqual(primeFactors, p.Factorize(num)));
        }
    }

    internal class PrimeFactors
    {
        internal int[] Factorize(int v)
        {
            return Enumerable.Range(2, v).CollectExponents(v).ToArray();
        }
    }

    static class Extensions
    {
        public static IEnumerable<int> CollectExponents(this IEnumerable<int> factors, int num)
        {
            if (num == 1)
                yield return 1;
            foreach (var f in factors)
            {
                while (num % f == 0)
                {
                    num /= f;
                    yield return f;
                }
            }
        }
    }
}
