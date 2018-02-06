using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Model
{
    abstract class Drink
    {
        
        private string drinkName;
        private double drinkPurchasePrice;
        private double drinkVolume;
        private int drinkAvalaibleQuantity;

        public Drink(string drinkName, double drinkPurchasePrice, double drinkVolume, int drinkAvalaibleQuantity)
        {
            this.drinkName = drinkName;
            this.drinkPurchasePrice = drinkPurchasePrice;
            this.drinkVolume = drinkVolume;
            this.drinkAvalaibleQuantity = drinkAvalaibleQuantity;
        }

        public string DrinkName
        {
            get { return drinkName; }
            set { drinkName = value; }
        }

        public double DrinkPurchasePrice
        {
            get { return drinkPurchasePrice; }
            set
            {
                if (value >= 0)
                {
                    drinkPurchasePrice = value;
                }
                else
                    throw new Exception("Ціна не може бути <= 0");
            }
        }

        public double DrinkVolume
        {
            get { return drinkVolume; }
            set
            {
                if (value >= 0)
                {
                    drinkVolume = value;
                }
                else
                    throw new Exception("Об'єм не може бути <= 0");
            }
        }

        public int DrinkAvalaibleQuantity
        {
            get { return drinkAvalaibleQuantity; }
            set
            {
                if (value >= 0)
                {
                    drinkAvalaibleQuantity = value;
                }
                else
                    throw new Exception("К-ть напоїв не може бути <= 0");
            }
        }
    }
}
