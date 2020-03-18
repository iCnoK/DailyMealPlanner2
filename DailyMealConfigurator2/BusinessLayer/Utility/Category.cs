using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace BusinessLayer.Utility
{
    [Serializable]
    public class Category
    {
        public List<Product> Products { get; private set; }
        [XmlAttribute]
        public string Name { get; private set; }
        [XmlAttribute]
        public string Description { get; private set; }

        public void ChangeName(string newName)
        {
            if (!string.IsNullOrEmpty(newName))
            {
                Name = newName;
            }
            else
            {
                throw new Exception("String was null or empty");
            }
        }
        public void ChangeDescription(string newDescription)
        {
            Description = newDescription;
            //if (!string.IsNullOrEmpty(newDescription))
            //{
            //    Description = newDescription;
            //}
            //else
            //{
            //    throw new Exception("String was null or empty");
            //}
        }
        public bool AddProduct(Product product)
        {
            if (product != null)
            {
                Products.Add(product);
                return true;
            }
            return false;
        }
        public bool DeleteProduct(Product product)
        {
            return Products.Remove(product);
        }

        public Category()
        {
            Products = new List<Product>();
        }

        public Category(string name, string description) : this()
        {
            ChangeName(name);
            ChangeDescription(description);
        }

        public Category(string name, string description, List<Product> products) : this(name, description)
        {
            Products = new List<Product>(products.ToArray());
        }
    }
}
