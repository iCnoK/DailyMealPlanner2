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

        private string name;
        private int gramms;
        private double proteins;
        private double fats;
        private double carbs;
        private double calories;

        [XmlElement]
        public string Name
        {
            get => name;
            set
            {
                var rule = new NameRule();
                if (rule.ApplyRule(value))
                {
                    name = value;
                }
                else
                {
                    throw new Exception("Invalid name");
                }
            }
        }

        [XmlElement]
        public int Gramms
        {
            get => gramms;
            set
            {
                var rule = new GrammsRule();
                if (rule.ApplyRule(value))
                {
                    gramms = value;
                }
                else
                {
                    throw new Exception("Invalid gramms count");
                }
            }
        }

        [XmlElement]
        public double Proteins
        {
            get => proteins;
            set
            {
                var rule = new ProteinsRule();
                if (rule.ApplyRule(value))
                {
                    proteins = value;
                }
                else
                {
                    throw new Exception("Invalid proteins count");
                }
            }
        }

        [XmlElement]
        public double Fats
        {
            get => fats;
            set
            {
                var rule = new FatsRule();
                if (rule.ApplyRule(value))
                {
                    fats = value;
                }
                else
                {
                    throw new Exception("Invalid fats count");
                }
            }
        }

        [XmlElement]
        public double Carbs
        {
            get => carbs;
            set
            {
                var rule = new CarbsRule();
                if (rule.ApplyRule(value))
                {
                    carbs = value;
                }
                else
                {
                    throw new Exception("Invalid carbs count");
                }
            }
        }

        [XmlElement]
        public double Calories
        {
            get => calories;
            set
            {
                var rule = new CaloriesRule();
                if (rule.ApplyRule(value))
                {
                    calories = value;
                }
                else
                {
                    throw new Exception("Invalid calories count");
                }
            }
        }
        
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
