using BusinessLayer.MealPlanner;
using BusinessLayer.Utility;
using DataAccessLayer;
using PresentationLayer.Model;
using Prism.Commands;
using Prism.Mvvm;
using ServiceLayer;
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
        public MealTimeModel MealTimeModel { get; private set; }
        

        public event EventHandler AddEventRaise;
        public event EventHandler EditEventRaise;
        public event EventHandler SaveAsPDFRaise;
        protected virtual void OnAddEventRaise()
        {
            AddEventRaise?.Invoke(this, EventArgs.Empty);
        }
        protected virtual void OnEditEventRaise()
        {
            EditEventRaise?.Invoke(this, EventArgs.Empty);
        }
        protected virtual void OnSaveAsPDFRaise()
        {
            SaveAsPDFRaise?.Invoke(this, EventArgs.Empty);
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
                    if (MealTimeModel[SelectedIndex].Products.Count != 0)
                    {
                        ProductInfo = MealTimeModel[SelectedIndex].Products[NestedSelectedIndex].GetToolTipView;
                        MealTimeModel.ChangeMassOfProduct(SelectedIndex, NestedSelectedIndex, sliderValue);
                        ProgressBarValue = CalculateCommonCalories();
                    }
                }
                RaisePropertyChanged("SliderValue");
            }
        }


        private double progressBarValue;
        public double ProgressBarValue
        {
            get => progressBarValue;
            set
            {
                progressBarValue = value;
                RaisePropertyChanged("ProgressBarValue");
            }
        }


        private double progressBarValueMaximum;
        public double ProgressBarValueMaximum
        {
            get => progressBarValueMaximum;
            set
            {
                progressBarValueMaximum = value;
                ProgressBarLabel = Math.Round(value - 1000, 2).ToString();
                RaisePropertyChanged("ProgressBarValueMaximum");
            }
        }


        private string progressBarLabel;
        public string ProgressBarLabel
        {
            get => $"Recommended maximum: {progressBarLabel}";
            set { progressBarLabel = value; RaisePropertyChanged("ProgressBarLabel"); }
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


        private ICommand addMealTimeCommand;
        private ICommand editMealTimeCommand;
        private ICommand deleteMealTimeCommand;
        private ICommand clearAllMealTimesCommand;
        private ICommand exportAsPDF;


        public ICommand AddMealTimeCommand => addMealTimeCommand ?? (addMealTimeCommand = new DelegateCommand<object>(delegate (object obj)
        {
            OnAddEventRaise();
        }));


        public ICommand EditMealTimeCommand => editMealTimeCommand ?? (editMealTimeCommand = new DelegateCommand<object>(delegate (object obj)
        {
            OnEditEventRaise();
        }));


        public ICommand DeleteMealTimeCommand => deleteMealTimeCommand ?? (deleteMealTimeCommand = new DelegateCommand<object>(delegate (object obj)
        {
            MealTimeModel.DeleteMealTime(SelectedIndex);
        }));


        public ICommand ClearAllMealTimesCommand => clearAllMealTimesCommand ?? (clearAllMealTimesCommand = new DelegateCommand<object>(delegate (object obj)
        {
            for (int i = 0; i < MealTimeModel.MealTimesCount; i++)
            {
                int productNumber = MealTimeModel[i].Products.Count;
                for (int j = 0; j < productNumber; j++)
                {
                    MealTimeModel.DeleteProductFromMealTime(i, 0);
                }
            }
            ProgressBarValue = 0;
        }));


        public ICommand ExportAsPDF => exportAsPDF ?? (exportAsPDF = new DelegateCommand<object>(delegate (object obj)
        {
            OnSaveAsPDFRaise();
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
            ProgressBarValue = 0;
            ProgressBarValueMaximum = 1000;
            AddSubscriptionToEvents();
        }

        public double CalculateCommonCalories()
        {
            double counter = 0;
            for (int i = 0; i < MealTimeModel.MealTimesCount; i++)
            {
                var mealTime = MealTimeModel[i];
                foreach (var product in mealTime.Products)
                {
                    counter += product.Calories;
                }
            }
            return counter;
        }

        public void AddProductToMealTime(Product product)
        {
            AddProductToMealTime(SelectedIndex, product);
        }

        public void AddProductToMealTime(int mealTimeIndex, Product product)
        {
            MealTimeModel.AddProductToMealTime(mealTimeIndex, product);
            ProductInfo = product.GetToolTipView;
            ProgressBarValue = CalculateCommonCalories();
        }

        public void AddMealTime(MealTime mealTime)
        {
            MealTimeModel.AddMealTime(mealTime);
            AddSubscriptionToEvents();
            ProgressBarValue = CalculateCommonCalories();
        }

        public void EditMealTime(int mealTimeIndex, MealTime newMealTime)
        {
            MealTimeModel.EditMealTime(mealTimeIndex, newMealTime);
            AddSubscriptionToEvents();
            ProgressBarValue = CalculateCommonCalories();
        }

        private void MealTimeViewModel_RemoveEventRaise(object sender, EventArgs e)
        {
            MealTimeModel.DeleteProductFromMealTime(SelectedIndex, NestedSelectedIndex);
            ProgressBarValue = CalculateCommonCalories();
        }

        private void MealTimeViewModel_SelectedIndexChanged(object sender, EventArgs e)
        {
            NestedSelectedIndex = MealTimeModel.ListBoxItems[SelectedIndex].SelectedIndex >= 0 ?
                MealTimeModel.ListBoxItems[SelectedIndex].SelectedIndex : NestedSelectedIndex;

            if (MealTimeModel.ListBoxItems[SelectedIndex].Products.Count > 0 && NestedSelectedIndex < MealTimeModel.ListBoxItems[SelectedIndex].Products.Count)
            {
                ProductInfo = MealTimeModel.ListBoxItems[SelectedIndex].Products[NestedSelectedIndex].GetToolTipView;
                SliderValue = MealTimeModel[SelectedIndex].Products[NestedSelectedIndex].Gramms;
                ProgressBarValue = CalculateCommonCalories();
            }
        }
    }
}
