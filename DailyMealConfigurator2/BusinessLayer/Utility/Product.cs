using BusinessLayer.Interfaces;
using BusinessLayer.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace BusinessLayer.Utility
{
    public class Product
    {
        private Product(string name, int gramms, double protein, double fats, double carbs, double calories, bool flag = false)
        {
            Name = name;
            Gramms = gramms;
            Proteins = protein;
            Fats = fats;
            Carbs = carbs;
            Calories = calories;
        }

        public Product(string name, int gramms, double protein, double fats, double carbs, double calories)
        {
            if (!IsValid(new Product(name, gramms, protein, fats, carbs, calories, true)))
            {
                throw new Exception("Parameters are incorrect");
            }
            Name = name;
            Gramms = gramms;
            Proteins = protein;
            Fats = fats;
            Carbs = carbs;
            Calories = calories;
        }

        public Product()
        {

        }
        [XmlElement]
        public string Name { get; protected set; }
        [XmlElement]
        public int Gramms { get; protected set; }
        [XmlElement]
        public double Proteins { get; protected set; }
        [XmlElement]
        public double Fats { get; protected set; }
        [XmlElement]
        public double Carbs { get; protected set; }
        [XmlElement]
        public double Calories { get; protected set; }
        //[NonSerialized]
        public string GetToolTipView
        {
            get
            {
                return $"Name: {Name}\n" +
                    $"Gramms: {Gramms}\n" +
                    $"Proteins: {Proteins}\n" +
                    $"Fats: {Fats}\n" +
                    $"Carbs: {Carbs}\n" +
                    $"Calories: {Calories}";
            }
        }

        public static bool IsValid(Product product)
        {
            List<IProductRule> rules = new List<IProductRule>()
            {
                new NameRule(), 
                new GrammsRule(), 
                new ProteinsRule(), 
                new FatsRule(), 
                new CarbsRule(),
                new CaloriesRule()
            };
            for (int i = 0; i < rules.Count; i++)
            {
                if (!rules[i].ApplyRule(product))
                {
                    return false;
                }
            }
            return true;
        }
        public static bool IsValid(string name, int gramms, double protein, double fats, double carbs, double calories)
        {
            var nameRule = new NameRule();
            var grammsRule = new GrammsRule();
            var proteinsRule = new ProteinsRule();
            var fatsRule = new FatsRule();
            var carbsRule = new CarbsRule();
            var caloriesRule = new CaloriesRule();
            return nameRule.ApplyRule(name)
                   && grammsRule.ApplyRule(gramms)
                   && proteinsRule.ApplyRule(protein)
                   && fatsRule.ApplyRule(fats)
                   && carbsRule.ApplyRule(carbs)
                   && caloriesRule.ApplyRule(calories);
        }
    }
}
