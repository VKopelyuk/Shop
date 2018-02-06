using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Model
{
    class SoftDrink : Drink
    {
        private string drinkGroup;
        private List<string> drinkComposition;

        public SoftDrink(string drinkName, double drinkPurchasePrice,
             double drinkVolume, int drinkAvalaibleQuantity,
             string drinkGroup, List<string> drinkComposition) : base(drinkName, drinkPurchasePrice, drinkVolume, drinkAvalaibleQuantity)
        {
            this.drinkGroup = drinkGroup;
            this.drinkComposition = drinkComposition;
        }

        public string DrinkGroup
        {
            get { return drinkGroup; }
            set { drinkGroup = value; }
        }

        public List<string> DrinkComposition
        {
            get { return drinkComposition; }
            set { drinkComposition = value; }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder("\"");

            for (int i = 0; i < drinkComposition.Count - 1; i++)
            {
                sb.Append(drinkComposition[i] + ", ");
            }
            sb.Append(drinkComposition[drinkComposition.Count - 1]);
            sb.Append("\"");
            return "\"" + DrinkName + "\", " + DrinkPurchasePrice + ", \"" + DrinkGroup + "\", " + DrinkVolume + ", " + sb.ToString() + ", " + DrinkAvalaibleQuantity;
        }

    }
}
