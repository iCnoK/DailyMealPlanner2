using BusinessLayer.Utility;
using DataAccessLayer.DataAccess;
using DataAccessLayer.Utility;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public void SaveData()
        {
            Database.Serialize();
        }

        public List<Category> SearchInCategories(string searchName)
        {
            return SearchProduct.FindInCategoriesList(Database.Categories, searchName);
        }

        //public void SetNewCategories(List<Category> categories)
        //{
        //    Database.Categories = categories;
        //}

        public List<Category> GetCategories()
        {
            return new List<Category>(Database.Categories.ToArray());
        }

        public ObservableCollection<Category> GetCategoriesObservableCollection()
        {
            return new ObservableCollection<Category>(Database.Categories.ToArray());
        }
        #endregion
        #region MyRegion

        #endregion
    }
}
