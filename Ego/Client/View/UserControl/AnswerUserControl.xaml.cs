using System.Windows;
using System.Windows.Input;

namespace Client.View.UserControl
{
    public partial class AnswerUserControl : System.Windows.Controls.UserControl
    {
        #region Properties

        public char Sign
        {
            get => (char)GetValue(SignProperty);
            set => SetValue(SignProperty, value);
        }

        public string Answer
        {
            get => (string)GetValue(AnswerProperty);
            set => SetValue(AnswerProperty, value);
        }

        public ICommand ButtonCommand
        {
            get => (ICommand)GetValue(ButtonCommandProperty);
            set => SetValue(ButtonCommandProperty, value);
        }

        public bool IsButtonEnabled
        {
            get => (bool)GetValue(IsButtonEnabledProperty);
            set => SetValue(IsButtonEnabledProperty, value);
        }
        #endregion

        #region DependencyProperties

        public static readonly DependencyProperty SignProperty = DependencyProperty.Register("Sign", typeof(char), typeof(AnswerUserControl),
                new FrameworkPropertyMetadata('X', (s, e) =>
                    {
                        var source = s as AnswerUserControl;
                        source.Sign = (char)e.NewValue;
                    }
                ));

        public static readonly DependencyProperty AnswerProperty = DependencyProperty.Register("Answer", typeof(string), typeof(AnswerUserControl),
            new FrameworkPropertyMetadata("Odpowiedź nieznana", (s, e) =>
                {
                    var source = s as AnswerUserControl;
                    source.Answer = (string)e.NewValue;
                }
                ));

        public static readonly DependencyProperty ButtonCommandProperty = DependencyProperty.Register("ButtonCommand", typeof(ICommand), typeof(AnswerUserControl),
                new FrameworkPropertyMetadata(null, new PropertyChangedCallback((s, e) =>
                {
                    var source = s as AnswerUserControl;
                    source.ButtonCommand = (ICommand)e.NewValue;
                })));

        public static readonly DependencyProperty IsButtonEnabledProperty = DependencyProperty.Register("IsButtonEnabled", typeof(bool), typeof(AnswerUserControl),
        new FrameworkPropertyMetadata(true, new PropertyChangedCallback((s, e) =>
                {
            var source = s as AnswerUserControl;
            source.IsButtonEnabled = (bool)e.NewValue;
        })));

        #endregion

        public AnswerUserControl()
        {
            InitializeComponent();
        }
    }
}
