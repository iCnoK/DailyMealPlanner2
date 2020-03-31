using BusinessLayer.Enums;
using BusinessLayer.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceLayer
{
    public class UserDataExchanger
    {
        private User User { get; set; }

        public event EventHandler UserChanged;

        protected virtual void OnUserChanged()
        {
            UserChanged?.Invoke(this, EventArgs.Empty);
        }

        public UserDataExchanger()
        {
            User = new User(0, 0, 0, DailyActivity.Low);
            OnUserChanged();
        }

        public void ChangeAge(int newAge)
        {
            User.Age = newAge;
            OnUserChanged();
        }

        public void ChangeHeight(int newHeight)
        {
            User.Height = newHeight;
            OnUserChanged();
        }

        public void ChangeWeight(int newWeight)
        {
            User.Weight = newWeight;
            OnUserChanged();
        }

        public void ChangeDailyActivity(DailyActivity newDailyActivity)
        {
            User.DailyActivity = newDailyActivity;
            OnUserChanged();
        }

        public int GetAge => User.Age;

        public int GetHeight => User.Height;

        public int GetWeight => User.Weight;

        public DailyActivity GetDailyActivity => User.DailyActivity;

        public double GetARM => User.GetARM();

        public double GetBMR => User.GetBMR();

        public double GetDailyCaloriesRate => User.GetDailyCaloriesRate();
    }
}
