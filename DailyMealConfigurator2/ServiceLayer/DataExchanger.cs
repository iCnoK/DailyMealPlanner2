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

        public DataExchanger()
        {
            Database = new Database();
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
        #endregion
        #region MyRegion

        #endregion
    }
}
