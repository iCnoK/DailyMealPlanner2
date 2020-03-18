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
    public  class Product
    {
        public Product(string name, int gramms, double protein, double fats, double carbs, double calories)
        {
            Name = name;
            Gramms = gramms;
            Protein = protein;
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
        public double Protein { get; protected set; }
        [XmlElement]
        public double Fats { get; protected set; }
        [XmlElement]
        public double Carbs { get; protected set; }
        [XmlElement]
        public double Calories { get; protected set; }
        public static bool IsValid(Product product)
        {
            List<IRule> rules = new List<IRule>()
            {
                new NameRule(), 
                new GrammsRule(), 
                new ProteinRule(), 
                new FatsRule(), 
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
    }
}
