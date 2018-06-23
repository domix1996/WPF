using System.Windows;

namespace Client.View.UserControl
{
    public partial class PointsUserControl : System.Windows.Controls.UserControl
    {
        public int Points
        {
            get => (int)GetValue(PointsProperty);
            set => SetValue(PointsProperty, value);
        }

        // Using a DependencyProperty as the backing store for Points.  This enables animation, styling, binding, etc...
        public static readonly DependencyProperty PointsProperty =
            DependencyProperty.Register("Points", typeof(int), typeof(PointsUserControl), new FrameworkPropertyMetadata(0, (s, e) =>
            {
                var source = s as PointsUserControl;
                source.Points = (int)e.NewValue;
            }
                ));


        public PointsUserControl()
        {
            InitializeComponent();
        }
    }
}
