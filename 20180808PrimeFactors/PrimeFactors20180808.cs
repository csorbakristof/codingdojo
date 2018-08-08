using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace _20180808PrimeFactors
{
    [TestClass]
    public class PrimeFactors20180808
    {
        [TestMethod]
        public void Zero_ReturnsEmpty()
        {
            AssertResponse(0, new int[] { });
        }

        [TestMethod]
        public void One_ReturnsEmpty()
        {
            AssertResponse(1, new int[] { });
        }

        [TestMethod]
        public void Two_Returns2()
        {
            AssertResponse(2, new int[] { 2 });
        }

        [TestMethod]
        public void N4()
        {
            AssertResponse(4, new int[] { 2, 2 });
        }

        [TestMethod]
        public void N3()
        {
            AssertResponse(3, new int[] { 3 });
        }

        [TestMethod]
        public void Further()
        {
            AssertResponse(5, new int[] { 5 });
            AssertResponse(6, new int[] { 2, 3 });
            AssertResponse(7, new int[] { 7 });
            AssertResponse(8, new int[] { 2, 2, 2 });
            AssertResponse(9, new int[] { 3, 3 });
            AssertResponse(10, new int[] { 2, 5 });
            AssertResponse(11, new int[] { 11 });
            AssertResponse(12, new int[] { 2, 2, 3 });
            AssertResponse(15, new int[] { 3, 5 });
        }

        private void AssertResponse(int n, int[] correct)
        {
            var factors = PrimeFactors.Factor(n).ToList();
            Assert.IsTrue(Enumerable.SequenceEqual(correct, factors));
        }
    }

    internal class PrimeFactors
    {
        internal static IEnumerable<int> Factor(int n)
        {
            List<int> factors = new List<int>();
            int d = 2;
            while(n>1)
            {
                if (n % d == 0)
                {
                    yield return d;
                    n /= d;
                }
                else
                {
                    d++;
                }
            }
        }
    }
}
