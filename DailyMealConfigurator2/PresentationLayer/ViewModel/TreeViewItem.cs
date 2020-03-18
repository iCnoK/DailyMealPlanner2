using BusinessLayer.Utility;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.ViewModel
{
    public class TreeViewItem : BindableBase
    {
        public ObservableCollection<Product> Products { get; set; }

        private string categoryName;
        public string CategoryName
        {
            get => categoryName;
            set
            {
                categoryName = value;
                RaisePropertyChanged("CategoryName");
            }
        }

        private string description;
        public string Description
        {
            get => description;
            set
            {
                description = value;
                RaisePropertyChanged("Description");
            }
        }

        private bool isSelected;
        public bool IsSelected
        {
            get => isSelected;
            set { isSelected = value; RaisePropertyChanged("IsSelected"); }
        }

        public TreeViewItem(Category category)
        {
            Products = new ObservableCollection<Product>();
            CategoryName = category.Name;
            Description = category.Description;
            foreach (var item in category.Products)
            {
                Products.Add(item);
            }
        }
    }
}
