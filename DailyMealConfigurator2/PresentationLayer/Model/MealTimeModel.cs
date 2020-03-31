using BusinessLayer.MealPlanner;
using BusinessLayer.Utility;
using PresentationLayer.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PresentationLayer.Model
{
    public class MealTimeModel
    {
        public MealTime this[int index]
        {
            get => MealTimes[index].Clone();
        }

        public int MealTimesCount => MealTimes.Count;

        public ObservableCollection<MainListBoxItem> ListBoxItems { get; set; }

        private List<MealTime> MealTimes { get; set; }

        private List<MealTime> StandardMealTimes { get; set; }

        public MealTimeModel()
        {
            MealTimes = new List<MealTime>(0);
            ListBoxItems = new ObservableCollection<MainListBoxItem>();
            StandardMealTimes = new List<MealTime>(0);

            AddDefaultMealTimes();
        }

        public void AddMealTime(MealTime mealTime)
        {
            ListBoxItems.Add(new MainListBoxItem(mealTime.Clone()));
            MealTimes.Add(mealTime.Clone());
            StandardMealTimes.Add(mealTime.Clone());
        }

        public void EditMealTime(int mealTimeIndex, MealTime newMealTime)
        {
            ListBoxItems[mealTimeIndex] = new MainListBoxItem(newMealTime.Clone());
            MealTimes[mealTimeIndex] = newMealTime.Clone();
            StandardMealTimes[mealTimeIndex] = newMealTime.Clone();
        }

        public void DeleteMealTime(int mealTimeIndex)
        {
            ListBoxItems.RemoveAt(mealTimeIndex);
            MealTimes.RemoveAt(mealTimeIndex);
            StandardMealTimes.RemoveAt(mealTimeIndex);
        }

        public void AddProductToMealTime(int mealTimeIndex, Product product)
        {
            ListBoxItems[mealTimeIndex].Products.Add(product.Clone());
            MealTimes[mealTimeIndex].Add(product);
            StandardMealTimes[mealTimeIndex].Add(product.Clone());
        }

        private void EditProductInMealTime(int mealTimeIndex, int productIndex, Product newProduct)
        {
            //ListBoxItems[mealTimeIndex].Products[productIndex] = newProduct.Clone();
            MealTimes[mealTimeIndex].Products[productIndex] = newProduct.Clone();
            StandardMealTimes[mealTimeIndex].Products[productIndex] = newProduct.Clone();
        }

        public void DeleteProductFromMealTime(int mealTimeIndex, int productIndex)
        {
            ListBoxItems[mealTimeIndex].Products.RemoveAt(productIndex);
            MealTimes[mealTimeIndex].Products.RemoveAt(productIndex);
            StandardMealTimes[mealTimeIndex].Products.RemoveAt(productIndex);
        }

        public void ChangeMassOfProduct(int mealTimeIndex, int productIndex, int newMass)
        {
            var editedProduct = StandardMealTimes[mealTimeIndex].Products[productIndex];
            EditProductInMealTime(mealTimeIndex, productIndex, Product.RecalculateProduct(editedProduct, newMass));
        }

        public void ChangeMassOfAllProductsInMealTime(int mealTimeIndex, int newMass)
        {
            for (int i = 0; i < StandardMealTimes[mealTimeIndex].Products.Count; i++)
            {
                ChangeMassOfProduct(mealTimeIndex, i, newMass);
            }
        }

        private void AddDefaultMealTimes()
        {
            AddMealTime(new MealTime("Breakfast", "The first meal time"));
            AddMealTime(new MealTime("Lunch", "The second meal time"));
            AddMealTime(new MealTime("Dinner", "The third meal time"));
        }
    }
}
