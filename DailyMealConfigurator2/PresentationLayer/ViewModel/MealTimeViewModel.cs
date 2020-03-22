using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace PresentationLayer.ViewModel
{
    public class MealTimeViewModel : BindableBase
    {
        private Visibility visibility;
        public Visibility Visibility
        {
            get => visibility;
            set { visibility = value; RaisePropertyChanged("Visibility"); }
        }
    }
}
