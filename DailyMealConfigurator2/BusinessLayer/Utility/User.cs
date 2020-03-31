using BusinessLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Utility
{
    public class User
    {
        private int age;
        private int weight;
        private int height;

        //private double aRM;
        //private double bMR;
        //private double ailyCaloriesRate;

        private DailyActivity dailyActivity;

        public User(int age, int weight, int height, DailyActivity dailyActivity)
        {
            Age = age;
            Weight = weight;
            Height = height;
            DailyActivity = DailyActivity;
        }

        public int Age
        {
            get => age;
            set
            {
                if (value >= 0)
                {
                    age = value;
                }
            }
        }
        
        public int Weight
        {
            get => weight;
            set
            {
                if (value >= 0)
                {
                    weight = value;
                }
            }
        }
        
        public int Height
        {
            get => height;
            set
            {
                if (value >= 0)
                {
                    height = value;
                }
            }
        }

        public double GetARM()
        {
            switch (DailyActivity)
            {
                case DailyActivity.Low: return 1.2;
                case DailyActivity.Normal: return 1.375;
                case DailyActivity.Average: return 1.55;
                case DailyActivity.High: return 1.725;
                default: return 1.2;
            }
        }

        public double GetBMR()
        {
            return 447.593 + 9.247 * Weight + 3.098 * Height - 4.330 * Age;
        }

        public double GetDailyCaloriesRate()
        {
            return GetBMR() + GetARM();
        }
        
        public DailyActivity DailyActivity { get; set; }
    }
}
