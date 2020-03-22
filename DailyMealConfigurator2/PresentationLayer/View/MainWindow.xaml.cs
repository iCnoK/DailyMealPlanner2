using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using DataAccessLayer.DataAccess;
using PresentationLayer.ViewModel;
using PresentationLayer.Utility;

namespace PresentationLayer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            this.DataContext = new MainWindowViewModel();
            (this.DataContext as MainWindowViewModel).MessageBoxRequest += new EventHandler<MessageBoxEventArgs>(MainWindow_MessageBoxRequest);
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex(@"\d+");
            e.Handled = !regex.IsMatch(e.Text);
        }

        private void MainWindow_MessageBoxRequest(object sender, MessageBoxEventArgs e)
        {
            e.Show();
        }
    }
}
