using BusinessLayer.Utility;
using PresentationLayer.Utility;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PresentationLayer.ViewModel
{
    public class EditorViewModel : BindableBase
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

        private int gramms;
        public int Gramms
        {
            get => gramms;
            set { gramms = value; RaisePropertyChanged("Gramms"); }
        }

        private double proteins;
        public double Proteins
        {
            get => proteins;
            set { proteins = value; RaisePropertyChanged("Proteins"); }
        }

        private double fats;
        public double Fats
        {
            get => fats;
            set { fats = value; RaisePropertyChanged("Fats"); }
        }

        private double carbs;
        public double Carbs
        {
            get => carbs;
            set { carbs = value; RaisePropertyChanged("Carbs"); }
        }

        private double calories;
        public double Calories
        {
            get => calories;
            set { calories = value; RaisePropertyChanged("Calories"); }
        }

        public Product CopyOfTheDisplayedProduct { get; private set; }

        private ICommand saveProduct;
        public ICommand SaveProduct => saveProduct ?? (saveProduct = new DelegateCommand<object>(delegate (object obj)
        {
            OnEditEnded(new SenderTypeEventArgs("save"));
        }));

        private ICommand returnProduct;
        public ICommand ReturnProduct => returnProduct ?? (returnProduct = new DelegateCommand<object>(delegate (object obj)
        {
            OnEditEnded(new SenderTypeEventArgs("return"));
        }));

        public event EventHandler EditEnded;
        protected virtual void OnEditEnded(EventArgs args)
        {
            EditEnded?.Invoke(this, args);
        }

        public EditorViewModel()
        {
            Clear();
            //Name = string.Empty;
            //Gramms = 0;
            //Proteins = 0;
            //Fats = 0;
            //Carbs = 0;
            //Calories = 0;
            //CopyOfTheDisplayedProduct = null;
        }

        public EditorViewModel(Product product)
        {
            ShowProduct(product);
            //Name = product.Name;
            //Gramms = product.Gramms;
            //Proteins = product.Proteins;
            //Fats = product.Fats;
            //Carbs = product.Carbs;
            //Calories = product.Calories;
            //CopyOfTheDisplayedProduct = product;
        }

        public void ShowProduct(Product product)
        {
            Name = product.Name;
            Gramms = product.Gramms;
            Proteins = product.Proteins;
            Fats = product.Fats;
            Carbs = product.Carbs;
            Calories = product.Calories;
            CopyOfTheDisplayedProduct = product;
        }

        public void Clear()
        {
            Name = string.Empty;
            Gramms = 0;
            Proteins = 0;
            Fats = 0;
            Carbs = 0;
            Calories = 0;
            CopyOfTheDisplayedProduct = null;
        }

        public Product GetProduct()
        {
            return new Product(Name, Gramms, Proteins, Fats, Carbs, Calories);
        }
    }
}
