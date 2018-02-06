using Shop.Model;
using Shop.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Shop
{
    class Shop
    {
        private List<Drink> drinks;
        private DateTime date;
        private Dictionary<string, DrinkStatistic> drinkSoldsInfo;
        public Shop()
        {
            this.drinks = Utils.Utils.ReadFileContent("drink.csv");
            this.date = new DateTime(2018, 2, 6, 8, 0, 0);
            this.drinkSoldsInfo = new Dictionary<string, DrinkStatistic>();
        }
        public void StartWork()
        {
            Console.WriteLine("Початок роботи");
            for (int i = 0; i < 30; i++)
            {
                Console.WriteLine("\n\n\nПочаток " + (i + 1) + " дня");
                emulateDay();
                Console.WriteLine("Кінець" + (i + 1) + " дня");

            }
            Utils.Utils.Log(Report().ToString());
            try
            {
                Utils.Utils.RewriteFile(drinks,"drink.csv");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private void emulateDay()
        {
            for (int i = 8; i < 21; i++)
            {
                Console.WriteLine("\nІде " + (i + 1) + " година-------------------------------------------");
                emulateHour();
            }
            this.date.AddHours(11);
            rebuyProducts();
        }

        private void emulateHour()
        {
            int buysCount = new Random().Next(11);
            Console.WriteLine("Ця година матиме " + buysCount + " покупок");
            for (int i = 0; i < buysCount; i++)
            {
                Console.WriteLine("Покупка № " + (i + 1));
                emulateBuy();
            }

            this.date.AddHours(1);
        }

        private void emulateBuy()
        {
            Drink drink = this.drinks[new Random().Next(drinks.Count)];
            int quantity = new Random().Next(11);

            if (drink.DrinkAvalaibleQuantity >= quantity)
            {
                drink.DrinkAvalaibleQuantity = drink.DrinkAvalaibleQuantity - quantity;
                double price = calculatePrice(drink, quantity);

                DrinkStatistic info=null;

                if (drinkSoldsInfo.ContainsKey(drink.DrinkName))
                    info = this.drinkSoldsInfo[drink.DrinkName];
                //DrinkStatistic info = null;

                if (info == null)
                {
                    info = new DrinkStatistic(drink.DrinkName, quantity, 0, price, drink.DrinkPurchasePrice);
                }
                else
                {
                    info.SoldQuantity = info.SoldQuantity + quantity;
                    info.PriceSum = info.PriceSum + price;
                }
                if(!drinkSoldsInfo.ContainsKey(drink.DrinkName))
                    drinkSoldsInfo.Add(drink.DrinkName, info);
                Console.WriteLine("\"" + drink.DrinkName + "\" буде куплено у кількості " + quantity + " що коштуватиме " + String.Format("{0:0.##}", price));
            }
            else
            {
                Console.WriteLine("\"" + drink.DrinkName + "\" нажаль немає потрібної кількості " + quantity);
            }
        }

        private double calculatePrice(Drink drink, int quantity)
        {
            double initialPrice = drink.DrinkPurchasePrice;
            if (date.DayOfWeek == DayOfWeek.Sunday || date.DayOfWeek == DayOfWeek.Saturday)
                return (initialPrice * quantity) * 1.15;
            else if (date.Hour >= 18 && date.Hour <= 20)
                return (initialPrice * quantity) * 1.08;
            else if (quantity > 2)
                return (initialPrice * (quantity - 2)) * 1.07 + ((initialPrice * 2) * 1.10);
            else
                return (initialPrice * quantity) * 1.10;
        }


        private void rebuyProducts()
        {
            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine("Дозакупка товару");
            foreach (var drink in drinks)
            {
                if (drink.DrinkAvalaibleQuantity < 10)
                {
                    drink.DrinkAvalaibleQuantity = drink.DrinkAvalaibleQuantity + 150;
                    DrinkStatistic info = null;

                    if (drinkSoldsInfo.ContainsKey(drink.DrinkName))
                        info = drinkSoldsInfo[drink.DrinkName];

                    if (info == null)
                    {
                        info = new DrinkStatistic(drink.DrinkName, 0, 150, 0.0, drink.DrinkPurchasePrice);
                    }
                    else
                    {
                        info.RebuyQuantity = info.RebuyQuantity + 150;
                    }

                    if (!drinkSoldsInfo.ContainsKey(drink.DrinkName))
                        drinkSoldsInfo.Add(drink.DrinkName, info);
                    Console.WriteLine("Товар \"" + drink.DrinkName + " дозакуплено");
                }
            }
            Console.WriteLine("------------------------------------------------------------");
        }
        private StringBuilder Report()
        {
            Console.WriteLine("------------------------------------------------------------");
            StringBuilder sb = new StringBuilder("------------------------------------------------------------");
            sb.AppendLine();
            Double soldsSum = 0.0, drinkCost = 0.0, rebuySum = 0.0;
            foreach (var drink_info_value in drinkSoldsInfo.Values)
            {
                DrinkStatistic info = drink_info_value;
                Console.WriteLine("Для товару \'" + info.DrinkName + "\" було :");
                sb.AppendLine("Для товару \'" + info.DrinkName + "\" було :");
                Console.WriteLine("\t\tПродано " + info.SoldQuantity);
                sb.AppendLine("\t\tПродано " + info.SoldQuantity);
                Console.WriteLine("\t\tДозакуплено " + info.RebuyQuantity);
                sb.AppendLine("\t\tДозакуплено " + info.RebuyQuantity);
                Console.WriteLine("\t\tПри закупочній ціні + " + info.DrinkPurchasePrice);
                sb.AppendLine("\t\tПри закупочній ціні + " + info.DrinkPurchasePrice);
                Console.WriteLine("\t\tПрибуток від продаж " + String.Format("{0:0.##}", info.PriceSum));
                sb.AppendLine("\t\tПрибуток від продаж " + String.Format("{0:0.##}", info.PriceSum));
                soldsSum += info.PriceSum;
                drinkCost += info.SoldQuantity * info.DrinkPurchasePrice;
                rebuySum += info.RebuyQuantity * info.DrinkPurchasePrice;
            }
            Console.WriteLine("Прибуток магазину (сума продаж - собівартість проданого товару) = " + String.Format("{0:0.##}", soldsSum) + " - " + String.Format("{0:0.##}", drinkCost) +
                    " = " + String.Format("{0:0.##}", (soldsSum - drinkCost)));
            sb.AppendLine("Прибуток магазину (сума продаж - собівартість проданого товару) = " + String.Format("{0:0.##}", soldsSum) + " - " + String.Format("{0:0.##}", drinkCost) +
                    " = " + String.Format("{0:0.##}", (soldsSum - drinkCost)));
            Console.WriteLine("Витрати на дозакупку товару " + String.Format("{0:0.##}", rebuySum));
            sb.AppendLine("Витрати на дозакупку товару " + String.Format("{0:0.##}", rebuySum));
            Console.WriteLine("------------------------------------------------------------");
            sb.AppendLine("------------------------------------------------------------");
            return sb;
        }
    }
}
