using System;
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

namespace Walidacja
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            ViewModel vm = this.Resources["VM"] as ViewModel;

            if (!string.IsNullOrEmpty(vm.Error)) MessageBox.Show(vm.Error);
            else MessageBox.Show("Zarejestrowano");

        }

        private void FBButtonBase_OnClick(object sender, RoutedEventArgs e)
        {

        }
    }
}
