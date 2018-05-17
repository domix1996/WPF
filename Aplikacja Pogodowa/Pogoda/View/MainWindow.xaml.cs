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
using Weather.ModelNamespace;
using Weather.ViewModelNamespace;

namespace Pogoda
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var vm = this.Resources["WeatherVM"] as WeatherViewModel;
            vm?.RefreshData(vm.CityName);
        }
        
    }
}
