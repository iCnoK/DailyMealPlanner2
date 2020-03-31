using BusinessLayer.Enums;
using BusinessLayer.Utility;
using DataAccessLayer.DataAccess;
using DataAccessLayer.Enums;
using PresentationLayer.Model;
using PresentationLayer.Utility;
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
using System.Windows.Media;

namespace PresentationLayer.ViewModel
{
    public class MainWindowViewModel : BindableBase
    {
        public event EventHandler<MessageBoxEventArgs> MessageBoxRequest;
        protected void MessageBox_Show(Action<MessageBoxResult> resultAction, string messageBoxText, string caption = "", MessageBoxButton button = MessageBoxButton.OK, MessageBoxImage icon = MessageBoxImage.None, MessageBoxResult defaultResult = MessageBoxResult.None, MessageBoxOptions options = MessageBoxOptions.None)
        {
            if (this.MessageBoxRequest != null)
            {
                this.MessageBoxRequest(this, new MessageBoxEventArgs(resultAction, messageBoxText, caption, button, icon, defaultResult, options));
            }
        }

        //protected void AskTheQuestion()
        //{
        //    MessageBox_Show(ProcessTheAnswer, "Are you sure you want to do this?", "Alert", System.Windows.MessageBoxButton.YesNo);
        //}

        //public void ProcessTheAnswer(MessageBoxResult result)
        //{
        //    if (result == MessageBoxResult.Yes)
        //    {
        //        // Do something
        //    }
        //}


        private MainWindowModel MainWindowModel { get; set; }


        #region UserInfo

        UserDataExchanger UserDataExchanger { get; set; }

        public int Age
        {
            get => UserDataExchanger.GetAge;
            set
            {
                if (UserDataExchanger.GetAge != value)
                {
                    UserDataExchanger.ChangeAge(value);
                }

                RaisePropertyChanged("Age");
            }
        }


        public int Height
        {
            get => UserDataExchanger.GetHeight;
            set
            {
                if (UserDataExchanger.GetHeight != value)
                {
                    UserDataExchanger.ChangeHeight(value);
                }

                RaisePropertyChanged("Height");
            }
        }


        public int Weight
        {
            get => UserDataExchanger.GetWeight;
            set
            {
                if (UserDataExchanger.GetWeight != value)
                {
                    UserDataExchanger.ChangeWeight(value);
                }

                RaisePropertyChanged("Weight");
            }
        }


        public IEnumerable<DailyActivity> DailyActivityList
        {
            get
            {
                return Enum.GetValues(typeof(DailyActivity)).Cast<DailyActivity>();
            }
        }


        public IEnumerable<SearchRootType> SearchRootTypeList
        {
            get
            {
                return Enum.GetValues(typeof(SearchRootType)).Cast<SearchRootType>();
            }
        }


        public DailyActivity DailyActivityComboBoxSelectedItem
        {
            get => UserDataExchanger.GetDailyActivity;
            set
            {
                if (UserDataExchanger.GetDailyActivity != value)
                {
                    UserDataExchanger.ChangeDailyActivity(value);
                }

                RaisePropertyChanged("DailyActivityComboBoxSelectedItem");
            }
        }


        private SearchRootType searchRootTypeComboBosSelectedItem;
        public SearchRootType SearchRootTypeComboBoxSelectedItem
        {
            get => searchRootTypeComboBosSelectedItem;
            set
            {
                searchRootTypeComboBosSelectedItem = value;
                RaisePropertyChanged("SearchRootTypeComboBoxSelectedItem");
            }
        }


        public double DailyCaloriesRate
        {
            get => Math.Round(UserDataExchanger.GetDailyCaloriesRate, 2);
            set
            {
                RaisePropertyChanged("DailyCaloriesRate");
            }
        }


        public double ARM
        {
            get => Math.Round(UserDataExchanger.GetARM, 2);
            set
            {
                RaisePropertyChanged("ARM");
            }
        }


        public double BMR
        {
            get => Math.Round(UserDataExchanger.GetBMR, 2);
            set
            {
                RaisePropertyChanged("BMR");
            }
        }

        #endregion

        #region Categories

        private ICommand searchTextBoxTextChanged;
        private ICommand pushProductToMealTime;
        private ICommand addProduct;
        private ICommand addCategory;
        private ICommand editCategory;
        private ICommand deleteCategory;
        private ICommand windowClosed;

        public bool IsExpanded { get; private set; }


        private bool addCategoryIsPressed = false;


        private bool addProductIsPressed = false;


        public ICommand SearchTextBoxTextChanged => searchTextBoxTextChanged ?? (searchTextBoxTextChanged = new DelegateCommand<object>(delegate (object obj)
        {
            if (SearchRootTypeComboBoxSelectedItem == SearchRootType.CategoryType)
            {
                MainWindowModel.SearchCategory(SearchText);
            }
            else if (SearchRootTypeComboBoxSelectedItem == SearchRootType.ProductType)
            {
                MainWindowModel.SearchProducts(SearchText);
            }
        }));


        public ICommand PushProductToMealTime => pushProductToMealTime ?? (pushProductToMealTime = new DelegateCommand<object>(delegate (object obj)
        {
            if (string.IsNullOrEmpty(SearchText) && SelectedIndex >= 0 && NestedSelectedIndex >= 0 && MainWindowModel.DataExchanger[SelectedIndex].Products[NestedSelectedIndex] != null)
            {
                if (MealTimeStatus.SelectedIndex >= 0 && MealTimeStatus.SelectedIndex < MealTimeStatus.MainListBoxItems.Count)
                {
                    MealTimeStatus.AddProductToMealTime(MealTimeStatus.SelectedIndex, MainWindowModel.DataExchanger[SelectedIndex].Products[NestedSelectedIndex]);
                }
                else
                {
                    MessageBox_Show(null, "You have not selected a meal time!", "Error", MessageBoxButton.OK,
                        MessageBoxImage.Error, MessageBoxResult.OK);
                }
            }
            else if (!string.IsNullOrEmpty(SearchText))
            {
                if (SelectedIndex >= 0 && NestedSelectedIndex >= 0)
                {
                    MealTimeStatus.AddProductToMealTime(MealTimeStatus.SelectedIndex, MainWindowModel.ListBoxItems[SelectedIndex].Products[NestedSelectedIndex]);
                }
                else
                {
                    MessageBox_Show(null, "You have not selected a meal time!", "Error", MessageBoxButton.OK,
                        MessageBoxImage.Error, MessageBoxResult.OK);
                }
            }
            else
            {
                MessageBox_Show(null, "You have not selected a product!", "Error", MessageBoxButton.OK,
                        MessageBoxImage.Error, MessageBoxResult.OK);
            }
        }));


        public ICommand AddProduct => addProduct ?? (addProduct = new DelegateCommand<object>(delegate (object obj)
        {
            addProductIsPressed = true;
            ProductEditorStatus.ShowProduct(new Product("Name", 100, 1, 1, 1, 1));
            CategoriesListBoxIsEnabled = false;
            ProductEditorFocus();
        }));


        public ICommand AddCategory => addCategory ?? (addCategory = new DelegateCommand<object>(delegate (object obj)
        {
            addCategoryIsPressed = true;
            CategoryEditorStatus.ShowCategory(new Category("Name", "Description"));
            CategoriesListBoxIsEnabled = false;
            CategoryEditorFocus();
        }));


        public ICommand EditCategory => editCategory ?? (editCategory = new DelegateCommand<object>(delegate (object obj)
        {
            if (string.IsNullOrEmpty(SearchText))
            {
                CategoryEditorStatus.ShowCategory(MainWindowModel.DataExchanger[SelectedIndex]);
            }
            else
            {
                CategoryEditorStatus.ShowCategory(SelectedCategory.GetCategory());
            }
            CategoriesListBoxIsEnabled = false;
            CategoryEditorFocus();
        }));


        public ICommand DeleteCategory => deleteCategory ?? (deleteCategory = new DelegateCommand<object>(delegate (object obj)
        {
            if (string.IsNullOrEmpty(SearchText))
            {
                MainWindowModel.DataExchanger.DeleteCategory(SelectedIndex);
            }
            else
            {
                var categoryIndex = MainWindowModel.DataExchanger.FindCategory(SelectedCategory.GetCategory());
                if (categoryIndex >= 0)
                {
                    MainWindowModel.DataExchanger.DeleteCategory(categoryIndex);
                }
            }
            SearchText = string.Empty;
        }));


        public ICommand WindowClosed => windowClosed ?? (windowClosed = new DelegateCommand<object>(delegate (object obj)
        {
            MainWindowModel.DataExchanger.SaveData();
        }));


        public EditorViewModel ProductEditorStatus { get; set; }


        public CategoryEditorViewModel CategoryEditorStatus { get; set; }


        public MealTimeViewModel MealTimeStatus { get; set; }


        public ObservableCollection<MainListBoxItem> MainListBoxItems
        {
            get => MainWindowModel.ListBoxItems;
            set => MainWindowModel.ListBoxItems = value;
        }


        private int selectedIndex;
        public int SelectedIndex
        {
            get => selectedIndex;
            set { selectedIndex = value; RaisePropertyChanged("SelectedIndex"); }
        }


        private MainListBoxItem selectedCategory;
        public MainListBoxItem SelectedCategory
        {
            get => selectedCategory;
            set { selectedCategory = value; RaisePropertyChanged("SelectedCategory"); }
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


        private bool categoriesListBoxIsEnabled;
        public bool CategoriesListBoxIsEnabled
        {
            get => categoriesListBoxIsEnabled;
            set { categoriesListBoxIsEnabled = value; RaisePropertyChanged("CategoriesListBoxIsEnabled"); }
        }

        #endregion

        #region Editors and meal time

        private readonly SolidColorBrush TransparentBrush = new SolidColorBrush(Colors.Transparent);


        private readonly SolidColorBrush FocusBrush = new SolidColorBrush(Colors.White);


        private SolidColorBrush productEditorRectangleFill;
        public SolidColorBrush ProductEditorRectangleFill
        {
            get => productEditorRectangleFill;
            set { productEditorRectangleFill = value; RaisePropertyChanged("ProductEditorRectangleFill"); }
        }


        private SolidColorBrush categoryEditorRectangleFill;
        public SolidColorBrush CategoryEditorRectangleFill
        {
            get => categoryEditorRectangleFill;
            set { categoryEditorRectangleFill = value; RaisePropertyChanged("CategoryEditorRectangleFill"); }
        }


        private SolidColorBrush mealTimesRectangleFill;
        public SolidColorBrush MealTimesRectangleFill
        {
            get => mealTimesRectangleFill;
            set { mealTimesRectangleFill = value; RaisePropertyChanged("MealTimesRectangleFill"); }
        }


        private void ProductEditorFocus()
        {
            ProductEditorStatus.Visibility = Visibility.Visible;
            CategoryEditorStatus.Visibility = Visibility.Collapsed;
            MealTimeStatus.Visibility = Visibility.Collapsed;
            ProductEditorRectangleFill = FocusBrush;
            CategoryEditorRectangleFill = TransparentBrush;
            MealTimesRectangleFill = TransparentBrush;
        }


        private void CategoryEditorFocus()
        {
            ProductEditorStatus.Visibility = Visibility.Collapsed;
            CategoryEditorStatus.Visibility = Visibility.Visible;
            MealTimeStatus.Visibility = Visibility.Collapsed;
            ProductEditorRectangleFill = TransparentBrush;
            CategoryEditorRectangleFill = FocusBrush;
            MealTimesRectangleFill = TransparentBrush;
        }


        private void MealTimesFocus()
        {
            ProductEditorStatus.Visibility = Visibility.Collapsed;
            CategoryEditorStatus.Visibility = Visibility.Collapsed;
            MealTimeStatus.Visibility = Visibility.Visible;
            ProductEditorRectangleFill = TransparentBrush;
            CategoryEditorRectangleFill = TransparentBrush;
            MealTimesRectangleFill = FocusBrush;
        }

        #endregion

        public MainWindowViewModel()
        {
            MainWindowModel = new MainWindowModel();

            CategoriesListBoxIsEnabled = true;
            ProductEditorStatus = new EditorViewModel();
            CategoryEditorStatus = new CategoryEditorViewModel();
            MealTimeStatus = new MealTimeViewModel();

            UserDataExchanger = new UserDataExchanger();

            UserDataExchanger.UserChanged += UserDataExchanger_UserChanged;

            MealTimesFocus();

            AddSubscriptionToEvents();

            MainWindowModel.PropertyChanged += MainWindowModel_PropertyChanged;

            ProductEditorStatus.EditEnded += ProductEditorStatus_EditEnded;
            CategoryEditorStatus.EditEnded += CategoryEditorStatus_EditEnded;
            MealTimeStatus.AddEventRaise += MealTimeStatus_AddEventRaise;
            MealTimeStatus.EditEventRaise += MealTimeStatus_EditEventRaise;
            MealTimeStatus.SaveAsPDFRaise += MealTimeStatus_SaveAsPDFRaise;

            UserDataExchanger_UserChanged(null, EventArgs.Empty);
        }


        private void AddSubscriptionToEvents()
        {
            foreach (var item in MainWindowModel.ListBoxItems)
            {
                item.SelectedIndexChanged += MainWindowViewModel_SelectedIndexChanged;
                item.AddEventRaise += MainWindowViewModel_AddEventRaise;
                item.EditEventRaise += MainWindowViewModel_EditEventRaise;
                item.RemoveEventRaise += MainWindowViewModel_RemoveEventRaise;
            }
        }

        private void RemoveSubscriptionFromEvents()
        {
            foreach (var item in MainWindowModel.ListBoxItems)
            {
                item.SelectedIndexChanged -= MainWindowViewModel_SelectedIndexChanged;
                item.AddEventRaise -= MainWindowViewModel_AddEventRaise;
                item.EditEventRaise -= MainWindowViewModel_EditEventRaise;
                item.RemoveEventRaise -= MainWindowViewModel_RemoveEventRaise;
            }
        }

        private void ProductEditorStatus_EditEnded(object sender, EventArgs e)
        {
            SenderTypeEventArgs args = (SenderTypeEventArgs)e;

            if (string.Equals(args.Name, "save"))
            {
                if (Product.IsValid(
                name: ProductEditorStatus.Name,
                gramms: ProductEditorStatus.Gramms,
                protein: ProductEditorStatus.Proteins,
                fats: ProductEditorStatus.Fats,
                carbs: ProductEditorStatus.Carbs,
                calories: ProductEditorStatus.Calories))
                {
                    if (addProductIsPressed)
                    {
                        addProductIsPressed = false;
                        MainWindowModel.DataExchanger.AddProductToCategory(SelectedIndex, ProductEditorStatus.GetProduct());
                    }
                    else
                    {
                        MainWindowModel.DataExchanger.FindAndReplaceProductInCategory(
                            SelectedCategory.GetCategory(), ProductEditorStatus.CopyOfTheDisplayedProduct, ProductEditorStatus.GetProduct());
                    }
                    ProductEditorStatus.Clear();
                    CategoriesListBoxIsEnabled = true;
                    MealTimesFocus();
                }
                else
                {
                    MessageBox_Show(null, "Invalid parameters! Saving was failed!", "Error", MessageBoxButton.OK,
                        MessageBoxImage.Error, MessageBoxResult.OK);
                }

            }
            else if (string.Equals(args.Name, "return"))
            {
                ProductEditorStatus.Clear();
                CategoriesListBoxIsEnabled = true;
                MealTimesFocus();
            }
            SearchText = string.Empty;
        }


        private void CategoryEditorStatus_EditEnded(object sender, EventArgs e)
        {
            SenderTypeEventArgs args = (SenderTypeEventArgs)e;

            if (string.Equals(args.Name, "save"))
            {
                if (Category.IsValid(
                    name: CategoryEditorStatus.Name, description: CategoryEditorStatus.Description))
                {
                    if (MealTimeAdd && !MealTimeEdit)
                    {
                        MealTimeAdd = false;
                        MealTimeStatus.AddMealTime(new BusinessLayer.MealPlanner.MealTime(CategoryEditorStatus.GetCategory()));
                    }
                    else if (!MealTimeAdd && MealTimeEdit)
                    {
                        MealTimeEdit = false;
                        MealTimeStatus.EditMealTime(MealTimeStatus.SelectedIndex, new BusinessLayer.MealPlanner.MealTime(CategoryEditorStatus.GetCategory()));
                    }
                    if (addCategoryIsPressed && !MealTimeAdd && !MealTimeEdit)
                    {
                        addCategoryIsPressed = false;
                        RemoveSubscriptionFromEvents();
                        MainWindowModel.DataExchanger.AddCategory(CategoryEditorStatus.GetCategory());
                        AddSubscriptionToEvents();
                    }
                    else if (!MealTimeAdd && !MealTimeEdit)
                    {
                        RemoveSubscriptionFromEvents();
                        MainWindowModel.DataExchanger.FindAndReplaceCategory(CategoryEditorStatus.CopyOfTheDisplayedCategory, CategoryEditorStatus.GetCategory());
                        AddSubscriptionToEvents();
                    }
                    CategoryEditorStatus.Clear();
                    CategoriesListBoxIsEnabled = true;
                    MealTimesFocus();

                }
                else
                {
                    MessageBox_Show(null, "Invalid parameters! Saving was failed!", "Error", MessageBoxButton.OK,
                        MessageBoxImage.Error, MessageBoxResult.OK);
                }
            }
            else if (string.Equals(args.Name, "return"))
            {
                CategoryEditorStatus.Clear();
                CategoriesListBoxIsEnabled = true;
                MealTimesFocus();
            }
            SearchText = string.Empty;
        }


        private void UserDataExchanger_UserChanged(object sender, EventArgs e)
        {
            Age = UserDataExchanger.GetAge;
            Height = UserDataExchanger.GetHeight;
            Weight = UserDataExchanger.GetWeight;
            DailyActivityComboBoxSelectedItem = UserDataExchanger.GetDailyActivity;
            ARM = UserDataExchanger.GetARM;
            BMR = UserDataExchanger.GetBMR;
            DailyCaloriesRate = UserDataExchanger.GetDailyCaloriesRate;
            MealTimeStatus.ProgressBarValueMaximum = UserDataExchanger.GetDailyCaloriesRate + 1000;
        }


        private bool MealTimeAdd = false;

        private void MealTimeStatus_AddEventRaise(object sender, EventArgs e)
        {
            MealTimeAdd = true;
            CategoryEditorStatus.ShowCategory(new Category("Name", "Description"));
            CategoriesListBoxIsEnabled = false;
            CategoryEditorFocus();
        }

        private bool MealTimeEdit = false;

        private void MealTimeStatus_EditEventRaise(object sender, EventArgs e)
        {
            MealTimeEdit = true;
            CategoryEditorStatus.ShowCategory(MealTimeStatus.MealTimeModel[MealTimeStatus.SelectedIndex].GetCategory());
            CategoriesListBoxIsEnabled = false;
            CategoryEditorFocus();
        }


        private void MealTimeStatus_SaveAsPDFRaise(object sender, EventArgs e)
        {
            var desktop = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);

            MainWindowModel.DataExchanger.ExportAsPDF($"{desktop}\\Meal times.pdf", UserDataExchanger.GetUser(), MealTimeStatus.MealTimeModel.GetMealTimes(), MealTimeStatus.CalculateCommonCalories());
        }


        private void MainWindowModel_PropertyChanged(object sender, EventArgs e)
        {
            AddSubscriptionToEvents();
        }


        private void MainWindowViewModel_AddEventRaise(object sender, EventArgs e)
        {
            PushProductToMealTime.Execute(null);
        }


        private void MainWindowViewModel_EditEventRaise(object sender, EventArgs e)
        {
            CategoriesListBoxIsEnabled = false;
            ProductEditorFocus();
            Product product = MainWindowModel.DataExchanger[SelectedIndex, NestedSelectedIndex];
            ProductEditorStatus.ShowProduct(product);
        }


        private void MainWindowViewModel_RemoveEventRaise(object sender, EventArgs e)
        {
            MainWindowModel.DataExchanger.DeleteProductFromCategory(SelectedIndex, NestedSelectedIndex);

            SearchText = string.Empty;
        }


        private void MainWindowViewModel_SelectedIndexChanged(object sender, EventArgs e)
        {
            NestedSelectedIndex = MainWindowModel.ListBoxItems[SelectedIndex].SelectedIndex;
        }
    }
}