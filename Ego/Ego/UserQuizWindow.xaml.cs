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
            //this.Player3Info.Visibility = Visibility.Collapsed;
            this.Player4Info.Visibility = Visibility.Collapsed;
        }

      

        

        private void AnswerSign_OnMouseEnter(object sender, MouseEventArgs e)
        {
            TextBlock btn = sender as TextBlock;
            btn.Text = "lol";
            answerA.Background = btn.Background == Brushes.Red ? Brushes.LightGray : Brushes.Red;
            signA.Background = signA.Background == Brushes.Red ? Brushes.LightGray : Brushes.Red;
        }
    }
}
