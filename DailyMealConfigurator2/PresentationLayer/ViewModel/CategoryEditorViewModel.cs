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
using System.Windows;
using System.Windows.Input;

namespace PresentationLayer.ViewModel
{
    public class CategoryEditorViewModel : BindableBase
    {
        private Visibility visibility;
        public Visibility Visibility
        {
            get => visibility;
            set { visibility = value; RaisePropertyChanged("Visibility"); }
        }

        private string name;
        public string Name
        {
            get => name;
            set { name = value; RaisePropertyChanged("Name"); }
        }

        private string description;
        public string Description
        {
            get => description;
            set { description = value; RaisePropertyChanged("Description"); }
        }

        public Category CopyOfTheDisplayedCategory { get; private set; }

        private ICommand saveCategory;
        public ICommand SaveCategory => saveCategory ?? (saveCategory = new DelegateCommand<object>(delegate (object obj)
        {
            OnEditEnded(new SenderTypeEventArgs("save"));
        }));

        private ICommand returnCategory;
        public ICommand ReturnCategory => returnCategory ?? (returnCategory = new DelegateCommand<object>(delegate (object obj)
        {
            OnEditEnded(new SenderTypeEventArgs("return"));
        }));

        public event EventHandler EditEnded;
        protected virtual void OnEditEnded(EventArgs args)
        {
            EditEnded?.Invoke(this, args);
        }

        private List<Product> Products { get; set; }

        public CategoryEditorViewModel()
        {
            Clear();
        }

        public CategoryEditorViewModel(Category category)
        {
            ShowCategory(category);
        }

        public void ShowCategory(Category category)
        {
            Name = category.Name;
            Description = category.Description;
            Products = category.Products;
            CopyOfTheDisplayedCategory = category;
        }

        public void Clear()
        {
            Name = string.Empty;
            Description = string.Empty;
            Products = null;
            CopyOfTheDisplayedCategory = null;
        }

        public Category GetCategory()
        {
            return new Category(Name, Description, Products);
        }
    }
}
