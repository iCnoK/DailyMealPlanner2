using BusinessLayer.Utility;
using System.Collections.Generic;

namespace BusinessLayer.MealPlanner
{
    public class MealTime
    {
        public MealTime(List<Product> products, string name, string description)
        {
            Products = products;
            Name = name;
            Description = description;
        }

        public List<Product> Products { get; private set; }

        public string Name { get; private set; }

        public string Description { get; private set; }
    }
}
