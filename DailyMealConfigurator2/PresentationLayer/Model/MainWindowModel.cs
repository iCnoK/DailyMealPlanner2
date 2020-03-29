using BusinessLayer.Utility;
using PresentationLayer.ViewModel;
using ServiceLayer;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.Model
{
    public class MainWindowModel
    {
        public DataExchanger DataExchanger { get; set; }

        public ObservableCollection<MainListBoxItem> ListBoxItems { get; set; }

        public event EventHandler PropertyChanged;

        protected virtual void OnPropertyChanged()
        {
            PropertyChanged?.Invoke(this, EventArgs.Empty);
        }

        public MainWindowModel()
        {
            DataExchanger = new DataExchanger();
            ListBoxItems = GetMainListBoxItems();

            DataExchanger.DatabaseChanged += DataExchanger_DatabaseChanged;

            //ListBoxItems.CollectionChanged += ListBoxItems_CollectionChanged;
            //foreach (var item in ListBoxItems)
            //{
            //    item.Products.CollectionChanged += Products_CollectionChanged;
            //}
        }

        public void SearchProducts(string name)
        {
            if (!string.IsNullOrEmpty(name))
            {
                ListBoxItems.Clear();

                var categories = DataExchanger.GetCategories();
                foreach (var item in categories)
                {
                    ListBoxItems.Add(new MainListBoxItem(item));
                }
                OnPropertyChanged();
            }
            else
            {
                DataExchanger_DatabaseChanged(null, EventArgs.Empty);
            }
        }

        private void DataExchanger_DatabaseChanged(object sender, EventArgs e)
        {
            ListBoxItems.Clear();

            var categories = DataExchanger.GetCategories();
            foreach (var item in categories)
            {
                ListBoxItems.Add(new MainListBoxItem(item));
            }

            //ListBoxItems = GetMainListBoxItems();
            OnPropertyChanged();
        }

        private ObservableCollection<MainListBoxItem> GetMainListBoxItems()
        {
            var result = new ObservableCollection<MainListBoxItem>();
            var categories = DataExchanger.GetCategories();
            foreach (var item in categories)
            {
                result.Add(new MainListBoxItem(item));
            }
            return result;
        }

        //private void ChangeDatabase()
        //{
        //    List<Category> categories = new List<Category>();
        //    foreach (var item in ListBoxItems)
        //    {
        //        categories.Add(item.GetCategory());
        //    }
        //    DataExchanger.SetNewCategories(categories);
        //}





        //private void ListBoxItems_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        //{
        //    //ChangeDatabase();
        //}

        //private void Products_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        //{
        //    //ChangeDatabase();
        //}
    }
}
