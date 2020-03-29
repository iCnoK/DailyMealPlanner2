using BusinessLayer.Enums;
using BusinessLayer.Utility;
using DataAccessLayer.DataAccess;
using PresentationLayer.Model;
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

        }));


        public ICommand PushProductToMealTime => pushProductToMealTime ?? (pushProductToMealTime = new DelegateCommand<object>(delegate (object obj)
        {

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
            //CategoryEditorStatus.ShowCategory(MainListBoxItems[SelectedIndex].GetCategory());
            CategoryEditorStatus.ShowCategory(MainWindowModel.DataExchanger[SelectedIndex]);
            CategoriesListBoxIsEnabled = false;
            CategoryEditorFocus();
        }));


        public ICommand DeleteCategory => deleteCategory ?? (deleteCategory = new DelegateCommand<object>(delegate (object obj)
        {
            //MainListBoxItems.RemoveAt(SelectedIndex);
            MainWindowModel.DataExchanger.DeleteCategory(SelectedIndex);
        }));


        public ICommand WindowClosed => windowClosed ?? (windowClosed = new DelegateCommand<object>(delegate (object obj)
        {
            MainWindowModel.DataExchanger.SaveData();
        }));


        public EditorViewModel ProductEditorStatus { get; set; }


        public CategoryEditorViewModel CategoryEditorStatus { get; set; }


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
            ProductEditorStatus.Visibility = System.Windows.Visibility.Visible;
            CategoryEditorStatus.Visibility = System.Windows.Visibility.Collapsed;
            //
            ProductEditorRectangleFill = FocusBrush;
            CategoryEditorRectangleFill = TransparentBrush;
            MealTimesRectangleFill = TransparentBrush;
        }


        private void CategoryEditorFocus()
        {
            ProductEditorStatus.Visibility = System.Windows.Visibility.Collapsed;
            CategoryEditorStatus.Visibility = System.Windows.Visibility.Visible;
            //
            ProductEditorRectangleFill = TransparentBrush;
            CategoryEditorRectangleFill = FocusBrush;
            MealTimesRectangleFill = TransparentBrush;
        }


        private void MealTimesFocus()
        {
            ProductEditorStatus.Visibility = System.Windows.Visibility.Collapsed;
            CategoryEditorStatus.Visibility = System.Windows.Visibility.Collapsed;
            //
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
            
            MealTimesFocus();

            AddSubscriptionToEvents();

            MainWindowModel.PropertyChanged += MainWindowModel_PropertyChanged;

            ProductEditorStatus.EditEnded += ProductEditorStatus_EditEnded;
            CategoryEditorStatus.EditEnded += CategoryEditorStatus_EditEnded;
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

                        //MainWindowModel.DataExchanger[SelectedIndex].Products.Add(ProductEditorStatus.GetProduct());
                        MainWindowModel.DataExchanger.AddProductToCategory(SelectedIndex, ProductEditorStatus.GetProduct());

                        //MainListBoxItems[SelectedIndex].Products.Add(ProductEditorStatus.GetProduct());
                    }
                    else
                    {
                        MainWindowModel.DataExchanger.EditProductInCatogory(SelectedIndex, NestedSelectedIndex, ProductEditorStatus.GetProduct());
                        //MainListBoxItems[SelectedIndex][NestedSelectedIndex] = ProductEditorStatus.GetProduct();
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
        }


        private void CategoryEditorStatus_EditEnded(object sender, EventArgs e)
        {
            SenderTypeEventArgs args = (SenderTypeEventArgs)e;

            if (string.Equals(args.Name, "save"))
            {
                if (Category.IsValid(
                    name: CategoryEditorStatus.Name, description: CategoryEditorStatus.Description))
                {
                    if (addCategoryIsPressed)
                    {
                        addCategoryIsPressed = false;
                        RemoveSubscriptionFromEvents();
                        MainWindowModel.DataExchanger.AddCategory(CategoryEditorStatus.GetCategory());
                        AddSubscriptionToEvents();
                        //MainListBoxItems.Add(new MainListBoxItem(CategoryEditorStatus.GetCategory()));
                        //MainListBoxItems[MainListBoxItems.Count - 1].SelectedIndexChanged += MainWindowViewModel_SelectedIndexChanged;
                        //MainListBoxItems[MainListBoxItems.Count - 1].EditEventRaise += MainWindowViewModel_EditEventRaise;
                    }
                    else
                    {
                        //int selectedIndexCopy = SelectedIndex;
                        //MainListBoxItems[selectedIndexCopy].SelectedIndexChanged -= MainWindowViewModel_SelectedIndexChanged;
                        //MainListBoxItems[selectedIndexCopy].EditEventRaise -= MainWindowViewModel_EditEventRaise;
                        //MainListBoxItems[selectedIndexCopy] = new MainListBoxItem(CategoryEditorStatus.GetCategory());
                        //MainListBoxItems[selectedIndexCopy].SelectedIndexChanged += MainWindowViewModel_SelectedIndexChanged;
                        //MainListBoxItems[selectedIndexCopy].EditEventRaise += MainWindowViewModel_EditEventRaise;
                        //SelectedIndex = selectedIndexCopy;
                        RemoveSubscriptionFromEvents();
                        MainWindowModel.DataExchanger.EditCategory(SelectedIndex, CategoryEditorStatus.GetCategory());
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
            //Product product = MainListBoxItems[SelectedIndex][NestedSelectedIndex];
            Product product = MainWindowModel.DataExchanger[SelectedIndex, NestedSelectedIndex];
            ProductEditorStatus.ShowProduct(product);
        }


        private void MainWindowViewModel_RemoveEventRaise(object sender, EventArgs e)
        {
            MainWindowModel.DataExchanger.DeleteProductFromCategory(SelectedIndex, NestedSelectedIndex);
        }


        private void MainWindowViewModel_SelectedIndexChanged(object sender, EventArgs e)
        {
            //NestedSelectedIndex = MainListBoxItems[SelectedIndex].SelectedIndex;
            NestedSelectedIndex = MainWindowModel.ListBoxItems[SelectedIndex].SelectedIndex;
        }
    }
}