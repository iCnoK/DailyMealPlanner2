using BusinessLayer.Utility;
using System.Collections.Generic;

namespace BusinessLayer.MealPlanner
{
    public class MealTime
    {
        public MealTime(Category category)
        {
            Products = category.Products;
            Name = category.Name;
            Description = category.Description;
        }

        public MealTime(string name, string description)
        {
            Products = new List<Product>();
            Name = name;
            Description = description;
        }

        public MealTime(List<Product> products, string name, string description)
        {
            Products = products;
            Name = name;
            Description = description;
        }

        public List<Product> Products { get; private set; }

        public string Name { get; private set; }

        public string Description { get; private set; }

        public void Add(Product product)
        {
            Products.Add((Product)product.Clone());
        }

        public Category GetCategory()
        {
            return new Category(Name, Description, new List<Product>(Products.ToArray()));
        }

        public MealTime Clone()
        {
            return new MealTime(new List<Product>(Products.ToArray()), Name, Description);
        }
    }
}
