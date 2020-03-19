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
    public class MainListBoxItem : BindableBase
    {
        public event EventHandler SelectedIndexChanged;
        protected virtual void OnSelectedIndexChanged()
        {
            SelectedIndexChanged?.Invoke(this, EventArgs.Empty);
        }

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

        private int selectedIndex;
        public int SelectedIndex
        {
            get => selectedIndex;
            set { selectedIndex = value; OnSelectedIndexChanged(); RaisePropertyChanged("SelectedIndex"); }
        }

        private bool isExpanded;
        public bool IsExpanded
        {
            get => isExpanded;
            set { isExpanded = value; RaisePropertyChanged("IsExpanded"); }
        }

        public MainListBoxItem(Category category)
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
