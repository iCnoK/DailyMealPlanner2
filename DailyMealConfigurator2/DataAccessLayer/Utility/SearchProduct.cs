using BusinessLayer.Utility;
using DataAccessLayer.Extensions;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Utility
{
    public static class SearchProduct
    {
        public static List<Product> FindInCategory(Category category, Product product)
        {
            List<Product> products = new List<Product>();
            foreach (var item in category.Products)
            {
                if (item == product)
                {
                    products.Add(item);
                }
            }
            return products;
        }

        public static List<Product> FindInCategory(Category category, string name)
        {
            List<Product> products = new List<Product>();
            foreach (var item in category.Products)
            {
                int distance = name.LevenshteinDistance(item.Name);
                if (distance >= 1 && distance <= 3)
                {
                    products.Add(item);
                }
            }
            return products;
        }

        public static List<Category> FindInCategoriesList(List<Category> categories, string name)
        {
            List<Category> resultCategories = new List<Category>();
            foreach (var category in categories)
            {
                List<Product> products = FindInCategory(category, name);
                if (products.Count != 0)
                {
                    resultCategories.Add(new Category(category.Name, category.Description, products));
                }
            }
            return resultCategories;
        }
    }
}
