using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Client.Converts
{
    public class StringToVisibilityConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType != typeof(Visibility)) return null;
            if (value is null) return Visibility.Hidden;
            string val = value.ToString();
            if (string.IsNullOrEmpty(val)) return Visibility.Hidden;
            return Visibility.Visible;


        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}