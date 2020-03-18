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

        public int Age
        {
            get => age;
            set
            {
                if (value > 0)
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
                if (value > 0)
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
                if (value > 0)
                {
                    height = value;
                }
            }
        }

        public double ARM
        {
            get
            {
                switch (DailyActivity)
                {
                    case DailyActivity.Low: return 1.2;
                    case DailyActivity.Normal: return 1.375;
                    case DailyActivity.Average: return 1.55;
                    case DailyActivity.High: return 1.725;
                }
                return 0;
            }
        }
        public double BMR => 447.593 + 9.247 * Weight + 3.098 * Height - 4.330 * Age;
        public double DailyCaloriesRate => BMR + ARM;
        public DailyActivity DailyActivity { get; set; }
    }
}
