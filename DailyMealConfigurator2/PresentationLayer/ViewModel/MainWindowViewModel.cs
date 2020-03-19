using BusinessLayer.Enums;
using DataAccessLayer.DataAccess;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

//private string text;
//public string Text
//{
//    get => text;
//    set
//    {
//        text = value;
//        RaisePropertyChanged("");
//    }
//}

namespace PresentationLayer.ViewModel
{
    public class MainWindowViewModel : BindableBase
    {
        #region UserInfo
        #region TextBoxes
        private int age;
        public int Age
        {
            get => age;
            set
            {
                age = value;
                RaisePropertyChanged("Age");
            }
        }
        private int height;
        public int Height
        {
            get => height;
            set
            {
                height = value;
                RaisePropertyChanged("Height");
            }
        }
        private int weight;
        public int Weight
        {
            get => weight;
            set
            {
                weight = value;
                RaisePropertyChanged("Weight");
            }
        }
        #endregion
        #region ComboBox
        public IEnumerable<DailyActivity> DailyActivityList
        {
            get
            {
                return Enum.GetValues(typeof(DailyActivity)).Cast<DailyActivity>();
            }
        }
        private int comboBoxSelectedIndex;
        public int ComboBoxSelectedIndex
        {
            get => comboBoxSelectedIndex;
            set
            {
                comboBoxSelectedIndex = value;
                RaisePropertyChanged("ComboBoxSelectedIndex");
            }
        }
        #endregion
        #region Daily calories rate, ARM, BMR
        private double dailyCaloriesRate;
        public double DailyCaloriesRate
        {
            get => dailyCaloriesRate;
            set
            {
                dailyCaloriesRate = value;
                RaisePropertyChanged("DailyCaloriesRate");
            }
        }
        private double aRM;
        public double ARM
        {
            get => aRM;
            set
            {
                aRM = value;
                RaisePropertyChanged("ARM");
            }
        }
        private double bMR;
        public double BMR
        {
            get => bMR;
            set
            {
                bMR = value;
                RaisePropertyChanged("BMR");
            }
        }
        #endregion
        #endregion
        #region Categories

        #region Buttons
        private ICommand expandOfCollapseAll;
        private ICommand editCategory;
        private ICommand deleteCategory;

        public bool IsExpanded { get; private set; }
        public ICommand ExpandOrCollapseAll => expandOfCollapseAll ?? (expandOfCollapseAll = new DelegateCommand<object>(delegate (object obj)
        {
            if (IsExpanded)
            {
                IsExpanded = false;
                CollapseAllExpanders();
            }
            else
            {
                IsExpanded = true;
                ExpandAllExpanders();
            }
        }));
        public ICommand EditCategory => editCategory ?? (editCategory = new DelegateCommand<object>(delegate (object obj)
        {
            
        }));
        public ICommand DeleteCategory => deleteCategory ?? (deleteCategory = new DelegateCommand<object>(delegate (object obj)
        {
            MainListBoxItems.RemoveAt(SelectedIndex);
        }));
        #endregion

        public ObservableCollection<MainListBoxItem> MainListBoxItems { get; set; }

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

        private string searchText;
        public string SearchText
        {
            get => searchText;
            set { searchText = value; RaisePropertyChanged("SearchText"); }
        }

        private void ExpandAllExpanders()
        {
            foreach (var item in MainListBoxItems)
            {
                item.IsExpanded = true;
            }
        }

        private void CollapseAllExpanders()
        {
            foreach (var item in MainListBoxItems)
            {
                item.IsExpanded = false;
            }
        }
        #endregion

        public MainWindowViewModel()
        {
            Database dataBase = new Database(DataAccessLayer.Enums.DatabaseType.DefaultFile);
            MainListBoxItems = new ObservableCollection<MainListBoxItem>();
            foreach (var item in dataBase.Categories)
            {
                MainListBoxItems.Add(new MainListBoxItem(item));
                MainListBoxItems[MainListBoxItems.Count - 1].SelectedIndexChanged += MainWindowViewModel_SelectedIndexChanged;
            }
        }

        private void MainWindowViewModel_SelectedIndexChanged(object sender, EventArgs e)
        {
            NestedSelectedIndex = MainListBoxItems[SelectedIndex].SelectedIndex;
        }
    }
}
