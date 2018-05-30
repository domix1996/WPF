using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using ViewModelNamespace;

namespace WeatherApplicationNamespace
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
            var vm = this.Resources["WeatherVM"] as WeatherViewModel;
            vm?.RefreshData(vm.CityName);
        }

        private void UIElement_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                var vm = this.Resources["WeatherVM"] as WeatherViewModel;
                TextBox tb = sender as TextBox;
                vm.CityName = tb.Text;
                vm?.RefreshData(vm.CityName);
          }
        }
    }
}
