using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using PlayerApp.ViewModel;

namespace PlayerApp.View
{
    /// <summary>
    /// Interaction logic for InGame.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public char answer;
        public MainWindow()
        {
            InitializeComponent();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            QuestionViewModel model = this.Resources["QuestionVm"] as QuestionViewModel;
            Button button = sender as Button;
           
            char sign = button.Name.ToLower()[0];
            if (answer == '\0')
            {
                button.Background = new SolidColorBrush(Colors.Red);
                answer = sign;
            }
            //switch(sign)
            //{
            //    case 'a':
            //        model.AnswerA = button.Name;
            //        break;
            //    case 'b':
            //        model.AnswerB = button.Name;
            //        break;
            //    case 'c':
            //        model.AnswerC = button.Name;
            //        break;
            //    case 'd':
            //        model.AnswerD = button.Name;
            //        break;
            //}
           
        }
    }
}
