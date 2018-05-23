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
using System.Windows.Shapes;
using Brush = System.Drawing.Brush;

namespace Ego
{
    /// <summary>
    /// Interaction logic for UserQuizWindow.xaml
    /// </summary>
    public partial class UserQuizWindow : Window
    {
        public UserQuizWindow()
        {
            InitializeComponent();
        }

        private void SignC_OnGotFocus(object sender, RoutedEventArgs e)
        {
        }
    }
}
