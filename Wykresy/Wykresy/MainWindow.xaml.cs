using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
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
using OxyPlot;
using OxyPlot.Series;
using Wykresy.Annotations;

namespace Wykresy
{
    //jeśli rok który dodajemy istnieje to podmieniamy,a jeśli nie to dodajemy
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainViewModel _model;

        public MainWindow()
        {

            InitializeComponent();
            _model = new MainViewModel();
            DataContext = _model;
        }

        public class MyPoint : INotifyPropertyChanged
        {
            public double X { get; set; }
            public double Y { get; set; }
            public event PropertyChangedEventHandler PropertyChanged;

            public MyPoint(double x, double y)
            {
                X = x;
                Y = y;
            }
        }
        public class MainViewModel : INotifyPropertyChanged
        {
            public string Title { get; set; } = "Matches";

            public ObservableCollection<MyPoint> Points { get; } = new ObservableCollection<MyPoint>
            {
                new MyPoint(2012, 14),
                new MyPoint(2013, 13),
                new MyPoint(2014, 15),
                new MyPoint(2015, 16),
                new MyPoint(2016, 12),
                new MyPoint(2017, 23)
            };

            public ObservableCollection<DataPoint> Data
            {
                get { return Points.Select(x => new DataPoint(x.X, x.Y)).ToList(); }
            }

            //public ObservableCollection<DataPoint> Data { get; } = new ObservableCollection<DataPoint>
            //{
            //    new DataPoint(2012,14),
            //    new DataPoint(2013,13),
            //    new DataPoint(2014,15),
            //    new DataPoint(2015,16),
            //    new DataPoint(2016,12),
            //    new DataPoint(2017,23)
            //};
            public event PropertyChangedEventHandler PropertyChanged;

        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            _model.Points.Add(new MyPoint(

                double.Parse(NewValue.Text),
                double.Parse(NewYear.Text)
            ));
        }
    }
}
