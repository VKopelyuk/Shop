using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Model
{
    class AlcoholicDrink :  Drink
    {
        private string classification;
        private double grade;

        public AlcoholicDrink(string drinkName, double drinkPurchasePrice, 
            double drinkVolume, int drinkAvalaibleQuantity, 
            string classification, double grade ) : base(drinkName,drinkPurchasePrice,drinkVolume,drinkAvalaibleQuantity)
        {
            this.classification = classification;
            this.grade = grade;
        }

        public string Classification
        {
            get { return classification; }
            set { classification = value; }
        }

        public double Grade
        {
            get { return grade; }
            set
            {
                if (grade >= 0)
                {
                    grade = value;
                }
                else
                    throw new Exception("Градус не може бути <= 0");
            }
        }

        public override string ToString()
        {
            return "\"" + DrinkName + "\", " + DrinkPurchasePrice + ", " + "\"" + Classification + "\", " + DrinkVolume + ", " + Grade + "%, " + DrinkAvalaibleQuantity;
        }
    }
}
