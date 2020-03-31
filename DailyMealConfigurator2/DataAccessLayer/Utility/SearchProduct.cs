using BusinessLayer.Utility;
using DataAccessLayer.Extensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

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
                int distance = name.LevenshteinDistance(item.Name.ToLower());
                if (distance == 1 || distance == 2)
                {
                    products.Add(item);
                }
            }
            return products;
        }

        public static List<Category> FindCategories(List<Category> categories, string name)
        {
            List<Category> resultCategories = new List<Category>();
            foreach (var category in categories)
            {
                if (Regex.IsMatch(category.Name.ToLower(), name, RegexOptions.IgnoreCase))
                {
                    resultCategories.Add(category);
                }
            }
            return resultCategories;
        }

        public static List<Category> FindInCategoriesList(List<Category> categories, string name, bool experimentalMode = false)
        {
            List<Category> resultCategories = new List<Category>();
            if (!experimentalMode)
            {
                foreach (var category in categories)
                {
                    List<Product> products = FindInCategory(category, name.ToLower());
                    if (products.Count != 0)
                    {
                        resultCategories.Add(new Category(category.Name, category.Description, products));
                    }
                }
                return resultCategories;
            }
            else
            {
                foreach (var category in categories)
                {
                    var products = new List<Product>();
                    foreach (var product in category.Products)
                    {
                        if (Regex.IsMatch(product.Name.ToLower(), name, RegexOptions.IgnoreCase))
                        {
                            products.Add(product);
                        }
                    }
                    if (products.Count != 0)
                    {
                        resultCategories.Add(new Category(category.Name, category.Description, products));
                    }
                }
                return resultCategories;
            }
        }
    }
}
