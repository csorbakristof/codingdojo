using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace _20180809Supermarket
{
    [TestClass]
    public class Supermarket20180809
    {
        private Supermarket sm = new Supermarket();

        [TestMethod]
        public void Nothing_CostsNothing()
        {
            AssertPrice("", 0);
        }


        [TestMethod]
        public void UnknownSingleItem_ThrowsException()
        {
            try
            {
                sm.GetPrice("A");
                Assert.Fail();
            }
            catch(ArgumentException e)
            {
                Assert.AreEqual("Unknown item: A", e.Message);
            }
        }


        [TestMethod]
        public void AddedSingleItem_ReturnsPrice()
        {
            sm.Add('A', 3);
            AssertPrice("A", 3);
        }


        [TestMethod]
        public void TwoItems_PriceAdded()
        {
            sm.Add('A', 3);
            sm.Add('B', 4);
            AssertPrice("AB", 7);
        }


        [TestMethod]
        public void ManyItemsSomeMissing_ThrowsException()
        {
            sm.Add('A', 3);
            sm.Add('B', 4);
            try
            {
                sm.GetPrice("AXB");
                Assert.Fail();
            }
            catch(ArgumentException e)
            {
                Assert.AreEqual("Unknown item: X", e.Message);
            }
        }


        [TestMethod]
        public void SomeItemsMultipleTimes_PriceIsCorrect()
        {
            sm.Add('A', 3);
            sm.Add('B', 4);
            sm.Add('C', 5);
            AssertPrice("AABCC", 3+3+4+5+5);
        }

        // Mennyisegi kedvezmenyek (tetszoleges szamu lepcsovel)
        [TestMethod]
        public void ManyIsCheaper()
        {
            var item = new ItemWithPriceSteps('A');
            item.AddPriceStep(1, 3);
            item.AddPriceStep(5, 2);
            item.AddPriceStep(10, 1);
            sm.Add(item);
            AssertPrice("A", 3);
            AssertPrice("AA", 2*3);
            AssertPrice("AAAAA", 5*2);
            AssertPrice("AAAAAA", 6 * 2);
            AssertPrice("AAAAAAAAAA", 10 * 1);
            AssertPrice("AAAAAAAAAAAA", 12 * 1);
        }


        [TestMethod]
        public void PriceStepsDefinedInRandomOrder_PriceIsCorrect()
        {
            var item = new ItemWithPriceSteps('A');
            item.AddPriceStep(10, 1);
            item.AddPriceStep(1, 3);
            item.AddPriceStep(5, 2);
            sm.Add(item);
            AssertPrice("AAAAA", 5 * 2);
            AssertPrice("AAAAAAAAAAAA", 12 * 1);
        }


        [TestMethod]
        public void ManyItemsSomeWithPriceSteps_PriceIsCorrect()
        {
            sm.Add('A', 2);
            var itemB = new ItemWithPriceSteps('B');
            itemB.AddPriceStep(1, 3);
            itemB.AddPriceStep(3, 2);
            sm.Add(itemB);
            var itemC = new ItemWithPriceSteps('C');
            itemC.AddPriceStep(1, 10);
            itemC.AddPriceStep(5, 5);
            sm.Add(itemC);
            AssertPrice("ABACCBAABB", 4*2 + 4*2 + 2*10);
            AssertPrice("BBCCCCCC", 2*3 + 6*5);
        }

        // Kettot fizet harmat vihet!
        [TestMethod]
        public void EveryThirdIsFree()
        {
            sm.Add(new ItemWithEveryNthIsFree('A', 3, 3));
            AssertPrice("A", 1*3);
            AssertPrice("AA", 2*3);
            AssertPrice("AAA", 2*3);
            AssertPrice("AAAA", 3 * 3);
            AssertPrice("AAAAAA", 4 * 3);
            AssertPrice("AAAAAAA", 5 * 3);
        }

        // A es B egyutt olcsobb!
        [TestMethod]
        public void CombinationSubtractionFromBag()
        {
            (string newBag, bool found) = sm.ExtractCombination("AB", "ABB");
            Assert.IsTrue(found);
            Assert.AreEqual("__B", newBag);
        }

        [TestMethod]
        public void CheaperCombinations()
        {
            sm.Add('A', 3);
            sm.Add('B', 5);
            sm.Add('C', 1);
            sm.AddCheaperCombination("AB", 5);
            AssertPrice("AAA", 9);
            AssertPrice("BBB", 15);
            AssertPrice("AC", 4);
            AssertPrice("BC", 6);
            AssertPrice("AB", 5);
            AssertPrice("ABC", 6);
            AssertPrice("AABC", 9);
            AssertPrice("AAABBC", 2*5+ 1*3 + 1*1);
        }

        [TestMethod]
        public void AddingUnderscoreAsItem_ThrowsException()
        {
            try
            {
                sm.Add('_', 2);
                Assert.Fail();
            }
            catch(ArgumentException)
            {
            }
        }

        // Klubtagoknak most minden 20%-kal olcsobb!

        [TestMethod]
        public void ClubMemberGlobalDiscounts()
        {
            sm.SetDiscountMarker('.', 0.8);
            sm.Add('A', 3);
            sm.Add('B', 10);
            AssertPrice(".AABBB", (int)Math.Round((2 * 3 + 3 * 10) * 0.8));
        }

        // Klubtag kedvezmeny adott termekekbol


        [TestMethod]
        public void SingleItemCheaperForClubMembers()
        {
            sm.SetDiscountMarker('.', 1.0);
            sm.Add(new ItemCheaperForClubMembers('A', 3, 2));
            sm.Add('B', 5);
            AssertPrice(".AAB", 2*2 + 5);
        }


        // Esetleges folytatas:
        // - Arukeszlet tamogatas: van mindenbol elegendo?
        // - Arukeszlet erteke (nem trivialis a kedvezmenyek miatt!)?

        private void AssertPrice(string items, int correctPrice)
        {
            Assert.AreEqual(correctPrice, sm.GetPrice(items));
        }
    }
}
