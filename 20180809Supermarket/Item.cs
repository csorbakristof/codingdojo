using System.Collections.Generic;
using System.Linq;
using static _20180809Supermarket.Supermarket;

namespace _20180809Supermarket
{
    class Item
    {
        protected int price;
        public char Name { get; protected set; }

        public Item(char name, int price)
        {
            this.Name = name;
            this.price = price;
        }

        public virtual void Buy(int amount, ShoppingContext context)
        {
            context.WorkingPrice += this.price * amount;
        }
    }

    internal class ItemWithPriceSteps : Item
    {
        List<(int minAmount, int price)> priceSteps = new List<(int minAmount, int price)>();
        public ItemWithPriceSteps(char name) : base(name, 0)
        {
        }

        public void AddPriceStep(int minAmount, int price)
        {
            priceSteps.Add((minAmount, price));
        }

        public override void Buy(int amount, ShoppingContext context)
        {
            int finalPrice = priceSteps.OrderByDescending(ps => ps.minAmount)
                .Where(ps => ps.minAmount <= amount).First().price;
            context.WorkingPrice += finalPrice * amount;
        }
    }

    internal class ItemWithEveryNthIsFree : Item
    {
        private int nthFree;
        public ItemWithEveryNthIsFree(char name, int price, int nthFree)
            : base(name, price)
        {
            this.nthFree = nthFree;
        }

        public override void Buy(int amount, ShoppingContext context)
        {
            context.WorkingPrice += price * (amount - amount / nthFree);
        }
    }

    class ItemCheaperForClubMembers : Item
    {
        private int priceForClubMembers;

        public ItemCheaperForClubMembers(char name, int price, int priceForClubMembers)
            : base(name, price)
        {
            this.priceForClubMembers = priceForClubMembers;
        }

        public override void Buy(int amount, ShoppingContext context)
        {
            context.WorkingPrice +=
                context.IsClubMember ? amount*priceForClubMembers : this.price;
        }
    }
}
