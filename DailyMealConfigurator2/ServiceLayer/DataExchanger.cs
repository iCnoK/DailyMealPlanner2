using BusinessLayer.MealPlanner;
using BusinessLayer.Utility;
using DataAccessLayer.DataAccess;
using DataAccessLayer.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace ServiceLayer
{
    public class DataExchanger
    {
        Database Database { get; set; }

        public event EventHandler DatabaseChanged;

        public Category this[int index]
        {
            get => Database[index];
        }
        public Product this[int category, int product]
        {
            get => Database[category].Products[product];
        }

        public DataExchanger()
        {
            Database = new Database();
            Database.DatabaseChanged += Database_DatabaseChanged;
        }

        private void Database_DatabaseChanged(object sender, EventArgs e)
        {
            OnDatabaseChanged();
        }

        protected virtual void OnDatabaseChanged()
        {
            DatabaseChanged?.Invoke(this, EventArgs.Empty);
        }

        #region DATABASE
        public void AddCategory(Category category)
        {
            Database.AddCategory(category);
        }

        public void EditCategory(int index, Category newCategory)
        {
            Database.EditCategory(index, newCategory);
        }

        public void DeleteCategory(int index)
        {
            Database.DeleteCategory(index);
        }

        public void AddProductToCategory(int categoryIndex, Product product)
        {
            Database.AddProductToCategory(categoryIndex, product);
        }

        public void EditProductInCatogory(int categoryIndex, int productIndex, Product newProduct)
        {
            Database.EditProductInCatogory(categoryIndex, productIndex, newProduct);
        }

        public void DeleteProductFromCategory(int categoryIndex, int productIndex)
        {
            Database.DeleteProductFromCategory(categoryIndex, productIndex);
        }

        public int FindCategory(Category desiredCategory)
        {
            return Database.FindCategory(desiredCategory);
        }

        public int FindProductInCategory(Category desiredCategory, Product desiredProduct)
        {
            return Database.FindProductInCategory(desiredCategory, desiredProduct);
        }

        public int FindProductInCategory(int categoryIndex, Product desiredProduct)
        {
            return Database.FindProductInCategory(categoryIndex, desiredProduct);
        }

        public bool FindAndReplaceCategory(Category desiredCategory, Category newCategory)
        {
            var categoryIndex = Database.FindCategory(desiredCategory);
            if (categoryIndex >= 0)
            {
                Database.EditCategory(categoryIndex, newCategory);
                return true;
            }
            return false;
        }

        public bool FindAndReplaceProductInCategory(Category desiredCategory, Product desiredProduct, Product newProduct)
        {
            var categoryIndex = Database.FindCategory(desiredCategory);
            if (categoryIndex >= 0)
            {
                var productIndex = Database.FindProductInCategory(categoryIndex, desiredProduct);
                if (productIndex >= 0)
                {
                    Database.EditProductInCatogory(categoryIndex, productIndex, newProduct);
                    return true;
                }
            }
            return false;
        }

        public void SaveData()
        {
            Database.Serialize();
        }

        public List<Category> SearchProductsInCategories(string searchName)
        {
            return SearchProduct.FindInCategoriesList(Database.Categories, searchName, true);
        }

        public List<Category> SearchCategories(string searchName)
        {
            return SearchProduct.FindCategories(Database.Categories, searchName);
        }
        
        public List<Category> GetCategories()
        {
            try
            {
                return new List<Category>(Database.Categories.ToArray());
            }
            catch (Exception)
            {
                return null;
            }

        }

        public ObservableCollection<Category> GetCategoriesObservableCollection()
        {
            return new ObservableCollection<Category>(Database.Categories.ToArray());
        }


        public void ExportAsPDF(string file, User user, List<MealTime> mealTimes, double totalCalories)
        {
            PDFExporter.ExportToPDFAsync(file, user, mealTimes, totalCalories);
        }
        #endregion
    }
}
