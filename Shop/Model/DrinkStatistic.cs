using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Model
{
    class DrinkStatistic
    {
        private string drinkName;
        private int soldQuantity;
        private int rebuyQuantity;
        private double priceSum;
        private double drinkPurchasePrice;

        public DrinkStatistic(string drinkName, int soldQuantity, int rebuyQuantity, double priceSum, double drinkPurchasePrice)
        {
            this.drinkName = drinkName;
            this.soldQuantity = soldQuantity;
            this.rebuyQuantity = rebuyQuantity;
            this.priceSum = priceSum;
            this.drinkPurchasePrice = drinkPurchasePrice;
        }

        public string DrinkName
        {
            get { return drinkName; }
            set { drinkName = value; }
        }

        public int SoldQuantity
        {
            get { return soldQuantity; }
            set { soldQuantity = value; }
        }

        public int RebuyQuantity
        {
            get { return rebuyQuantity; }
            set { rebuyQuantity = value; }
        }

        public double PriceSum
        {
            get { return priceSum; }
            set { priceSum = value; }
        }

        public double DrinkPurchasePrice
        {
            get { return drinkPurchasePrice; }
            set { drinkPurchasePrice = value; }
        }
    }
}
