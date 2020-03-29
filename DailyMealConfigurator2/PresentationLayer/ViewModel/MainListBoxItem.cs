using BusinessLayer.Utility;
using PresentationLayer.Utility;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace PresentationLayer.ViewModel
{
    public class MainListBoxItem : BindableBase
    {
        public Product this[int index]
        {
            get => Products[index];
            set => Products[index] = value;
        }

        public event EventHandler SelectedIndexChanged;
        protected virtual void OnSelectedIndexChanged(EventArgs args)
        {
            SelectedIndexChanged?.Invoke(this, args);
        }
        public event EventHandler AddEventRaise;
        public event EventHandler EditEventRaise;
        public event EventHandler RemoveEventRaise;
        protected virtual void OnAddEventRaise()
        {
            AddEventRaise?.Invoke(this, EventArgs.Empty);
        }
        protected virtual void OnEditEventRaise()
        {
            EditEventRaise?.Invoke(this, EventArgs.Empty);
        }
        protected virtual void OnRemoveEventRaise()
        {
            RemoveEventRaise?.Invoke(this, EventArgs.Empty);
        }

        public ObservableCollection<Product> Products { get; set; }

        private ICommand addProductToMealTime;
        private ICommand editProduct;
        private ICommand deleteProduct;

        public ICommand AddProductToMealTime => addProductToMealTime ?? (addProductToMealTime = new DelegateCommand<object>(delegate (object obj)
        {
            OnAddEventRaise();
        }));
        public ICommand EditProduct => editProduct ?? (editProduct = new DelegateCommand<object>(delegate (object obj)
        {
            OnEditEventRaise();
        }));
        public ICommand DeleteProduct => deleteProduct ?? (deleteProduct = new DelegateCommand<object>(delegate (object obj)
        {
            OnRemoveEventRaise();
            //Products.RemoveAt(SelectedIndex);
        }));

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
            set 
            { 
                selectedIndex = value; 
                OnSelectedIndexChanged(EventArgs.Empty); 
                RaisePropertyChanged("SelectedIndex"); 
            }
        }

        private bool isExpanded;
        public bool IsExpanded
        {
            get => isExpanded;
            set { isExpanded = value; RaisePropertyChanged("IsExpanded"); }
        }

        public string GetToolTipView
        {
            get
            {
                return $"Name: {CategoryName}\n" +
                    $"Description:\n{Description}\n" +
                    $"Number of products: {Products.Count}";
            }
        }

        //private bool isSelected;
        //public bool IsSelected
        //{
        //    get => isSelected;
        //    set
        //    {
        //        isSelected = value;
        //        OnSelectedIndexChanged(EventArgs.Empty);
        //        RaisePropertyChanged("IsSelected");
        //    }
        //}

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

        public Category GetCategory()
        {
            return new Category(CategoryName, Description, new List<Product>(Products.ToList<Product>()));
        }
    }
}
