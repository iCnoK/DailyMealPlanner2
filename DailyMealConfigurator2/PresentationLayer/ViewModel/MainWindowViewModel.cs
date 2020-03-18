using BusinessLayer.Enums;
using DataAccessLayer.DataAccess;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        public ObservableCollection<TreeViewItem> TreeViewItems { get; set; }
        //private string treeViewSelectedValuePath;
        //public string TreeViewSelectedValuePath
        //{
        //    get => treeViewSelectedValuePath;
        //    set
        //    {

        //    }
        //}
        #endregion

        public MainWindowViewModel()
        {
            Database dataBase = new Database(DataAccessLayer.Enums.DatabaseType.DefaultFile);
            TreeViewItems = new ObservableCollection<TreeViewItem>();
            foreach (var item in dataBase.Categories)
            {
                TreeViewItems.Add(new TreeViewItem(item));
            }
        }
    }
}
