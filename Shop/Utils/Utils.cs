using Shop.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Shop.Utils
{
    static class Utils
    {
        public static List<Drink> ReadFileContent(string file)
        {
            List<string> Drinks = new List<string>();
            string line;
            using (StreamReader reader = new StreamReader(file))
                while ((line = reader.ReadLine()) != null)
                {
                    Console.WriteLine(line);
                    Drinks.Add(line);
                }
            return ParseDrinks(Drinks);
        }

        public static List<Drink> ParseDrinks(List<string> drinks)
        {
            List<Drink> tmp_drinks = new List<Drink>();

            foreach (var drink in drinks)
            {
                string[] drink_components = Regex.Split(drink, ",");

                foreach (var a in drink_components)
                {
                    Console.WriteLine(a);
                }

                if (drink_components[drink_components.Length - 2].LastIndexOf('%') != -1)
                    tmp_drinks.Add(BuildAlcoholicDrink(drink_components));
                else tmp_drinks.Add(BuildSoftDrink(drink_components));
            }

            return tmp_drinks;
        }

        public static AlcoholicDrink BuildAlcoholicDrink(string[] drink_components)
        {

            var alcoholicDrink = new AlcoholicDrink(drink_components[0].Replace("\"", "").Trim(), Double.Parse(drink_components[1].Trim()),
                Double.Parse(drink_components[3].Trim()), Int32.Parse(drink_components[5].Trim()), drink_components[2].Replace("\"", "").Trim(),
                Double.Parse(drink_components[4].Trim().Replace("%", "")));

            return alcoholicDrink;
        }

        public static SoftDrink BuildSoftDrink(string[] drink_components)
        {
            int i = 4;
            List<string> composition = new List<string>();
            while (!drink_components[i].EndsWith("\""))
            {
                composition.Add(drink_components[i].Replace("\"", "").Trim());
                i++;
            }
            composition.Add(drink_components[i].Replace("\"", "").Trim());

            var softDrink = new SoftDrink(drink_components[0].Replace("\"", "").Trim(), Double.Parse(drink_components[1].Trim()),
                Double.Parse(drink_components[3].Trim()), Int32.Parse(drink_components[i + 1].Trim()),
                drink_components[2].Replace("\"", "").Trim(), composition);

            return softDrink;
        }

        public static void RewriteFile(List<Drink> drinks, string file)
        {
            System.IO.File.WriteAllBytes(file, new byte[0]);

            using (StreamWriter sw = new StreamWriter(file))
            {
                foreach (var drink in drinks)
                {
                    sw.WriteLine(drink.ToString());

                }
            }
        }

        public static void Log(string log_content)
        {
            using (StreamWriter sw = new StreamWriter("log.txt"))
            {
                    sw.WriteLine(log_content.ToString());
            }

        }

    }
}
