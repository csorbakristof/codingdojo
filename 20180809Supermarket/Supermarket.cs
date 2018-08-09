using System;
using System.Collections.Generic;
using System.Linq;

namespace _20180809Supermarket
{
    internal partial class Supermarket
    {
        private Dictionary<char, Item> items = new Dictionary<char, Item>();
        public Supermarket()
        {
            Add('_', 0);
        }

        internal void Add(char name, int price)
        {
            items.Add(name, new Item(name, price));
        }

        internal void Add(Item item)
        {
            items.Add(item.Name, item);
        }

        char? discountMarker = null;
        double discountFactor = 1.0;

        internal void SetDiscountMarker(char marker, double discountFactor)
        {
            this.discountMarker = marker;
            this.discountFactor = discountFactor;
        }

        List<(string combination, int price)> cheaperCombinations = new List<(string combination, int price)>();

        internal void AddCheaperCombination(string combination, int price)
        {
            cheaperCombinations.Add((combination, price));
        }

        internal int GetPrice(string bag)
        {
            var ctx = new ShoppingContext(bag);
            ExtractDiscountMarker(ctx);
            ExtractAllCombinations(ctx);
            foreach (char itemName in ctx.WorkingBag.Distinct())
            {
                if (!items.ContainsKey(itemName))
                    throw new ArgumentException($"Unknown item: {itemName}");
                items[itemName].Buy(ctx.WorkingBag.Count(c => c == itemName), ctx);
            }
            return ctx.GetPrice();
        }

        private void ExtractDiscountMarker(ShoppingContext ctx)
        {
            if (!discountMarker.HasValue)
                return;
            (string newBag, bool found) = ExtractCombination($"{discountMarker.Value}", ctx.WorkingBag);
            if (found)
            {
                ctx.GlobalDiscountFactor = this.discountFactor;
                ctx.IsClubMember = true;
                ctx.WorkingBag = newBag;
            }
        }

        private void ExtractAllCombinations(ShoppingContext ctx)
        {
            foreach (var c in cheaperCombinations)
            {
                while (true)
                {
                    (string newBag, bool found) = ExtractCombination(c.combination, ctx.WorkingBag);
                    if (!found)
                        break;
                    ctx.WorkingBag = newBag;
                    ctx.WorkingPrice += c.price;
                }
            }
        }

        internal (string newBag, bool found) ExtractCombination(string combination, string bag)
        {
            var b = bag.ToCharArray();
            foreach (char c in combination)
            {
                var idx = Array.FindIndex(b, e => e == c);
                if (idx == -1)
                    return (bag, false);
                b[idx] = '_';
            }
            return (new string(b), true);
        }

        internal class ShoppingContext
        {
            public double GlobalDiscountFactor { get; set; } = 1.0;
            public bool IsClubMember { get; set; } = false;
            public string WorkingBag { get; set; } = null;
            public int WorkingPrice { get; set; } = 0;

            public ShoppingContext(string bag)
            {
                WorkingBag = bag;
            }

            public int GetPrice()
            {
                return (int)Math.Round(WorkingPrice * GlobalDiscountFactor);
            }
        }
    }
}
