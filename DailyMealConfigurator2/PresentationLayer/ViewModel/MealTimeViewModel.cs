using BusinessLayer.MealPlanner;
using BusinessLayer.Utility;
using PresentationLayer.Model;
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
    public class MealTimeViewModel : BindableBase
    {
        private MealTimeModel MealTimeModel { get; set; }

        public event EventHandler AddEventRaise;
        public event EventHandler EditEventRaise;
        protected virtual void OnAddEventRaise()
        {
            AddEventRaise?.Invoke(this, EventArgs.Empty);
        }
        protected virtual void OnEditEventRaise()
        {
            EditEventRaise?.Invoke(this, EventArgs.Empty);
        }

        private Visibility visibility;
        public Visibility Visibility
        {
            get => visibility;
            set { visibility = value; RaisePropertyChanged("Visibility"); }
        }


        private int selectedIndex;
        public int SelectedIndex
        {
            get => selectedIndex;
            set { selectedIndex = value; RaisePropertyChanged("SelectedIndex"); }
        }


        private int nestedSelectedIndex;
        public int NestedSelectedIndex
        {
            get => nestedSelectedIndex;
            set { nestedSelectedIndex = value; RaisePropertyChanged("NestedSelectedIndex"); }
        }


        private int sliderValue;
        public int SliderValue
        {
            get => sliderValue;
            set 
            { 
                sliderValue = value;
                if (SelectedIndex >= 0 && NestedSelectedIndex >= 0)
                {
                    if (MealTimeModel.ListBoxItems[SelectedIndex].Products.Count != 0)
                    {
                        ProductInfo = MealTimeModel.ListBoxItems[SelectedIndex].Products[NestedSelectedIndex].GetToolTipView;
                        MealTimeModel.ChangeMassOfProduct(SelectedIndex, NestedSelectedIndex, value);
                    }
                }
                RaisePropertyChanged("SliderValue"); 
            }
        }


        private string productInfo;
        public string ProductInfo
        {
            get => productInfo;
            set { productInfo = value; RaisePropertyChanged("ProductInfo"); }
        }


        public ObservableCollection<MainListBoxItem> MainListBoxItems
        {
            get => MealTimeModel.ListBoxItems;
            set => MealTimeModel.ListBoxItems = value;
        }


        private ICommand addCategory;
        private ICommand editCategory;
        private ICommand deleteCategory;


        public ICommand AddCategory => addCategory ?? (addCategory = new DelegateCommand<object>(delegate (object obj)
        {
            OnAddEventRaise();
        }));


        public ICommand EditCategory => editCategory ?? (editCategory = new DelegateCommand<object>(delegate (object obj)
        {
            OnEditEventRaise();
        }));


        public ICommand DeleteCategory => deleteCategory ?? (deleteCategory = new DelegateCommand<object>(delegate (object obj)
        {
            MealTimeModel.DeleteMealTime(SelectedIndex);
        }));

        public void AddSubscriptionToEvents()
        {
            foreach (var item in MealTimeModel.ListBoxItems)
            {
                item.SelectedIndexChanged += MealTimeViewModel_SelectedIndexChanged;
                item.RemoveEventRaise += MealTimeViewModel_RemoveEventRaise;
            }
        }

        private void RemoveSubscriptionFromEvents()
        {
            foreach (var item in MealTimeModel.ListBoxItems)
            {
                item.SelectedIndexChanged -= MealTimeViewModel_SelectedIndexChanged;
                item.RemoveEventRaise -= MealTimeViewModel_RemoveEventRaise;
            }
        }

        public MealTimeViewModel()
        {
            MealTimeModel = new MealTimeModel();
            SelectedIndex = 0;

            AddSubscriptionToEvents();
        }

        public void AddProductToMealTime(Product product)
        {
            AddProductToMealTime(SelectedIndex, product);
        }

        public void AddProductToMealTime(int mealTimeIndex, Product product)
        {
            MealTimeModel.AddProductToMealTime(mealTimeIndex, product);
            ProductInfo = product.GetToolTipView;
            SliderValue = product.Gramms;
        }

        public void AddMealTime(MealTime mealTime)
        {
            MealTimeModel.AddMealTime(mealTime);
        }

        public void EditMealTime(int mealTimeIndex, MealTime newMealTime)
        {
            MealTimeModel.EditMealTime(mealTimeIndex, newMealTime);
        }

        private void MealTimeViewModel_RemoveEventRaise(object sender, EventArgs e)
        {
            MealTimeModel.DeleteProductFromMealTime(SelectedIndex, NestedSelectedIndex);
        }

        private void MealTimeViewModel_SelectedIndexChanged(object sender, EventArgs e)
        {
            NestedSelectedIndex = MealTimeModel.ListBoxItems[SelectedIndex].SelectedIndex >= 0 ? MealTimeModel.ListBoxItems[SelectedIndex].SelectedIndex : NestedSelectedIndex;
            ProductInfo = MealTimeModel.ListBoxItems[SelectedIndex].Products[NestedSelectedIndex].GetToolTipView;// + $"\n{NestedSelectedIndex}";

            //ProductInfo = NestedSelectedIndex >= 0 ? MealTimeModel.ListBoxItems[SelectedIndex].Products[NestedSelectedIndex].GetToolTipView + $"\n{NestedSelectedIndex}" : string.Empty;
        }
    }
}
