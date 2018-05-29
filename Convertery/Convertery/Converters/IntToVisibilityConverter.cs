using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Convertery.Converters
{
    public class IntToVisibilityConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType != typeof(Visibility))
                return null;
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
                return Visibility.Collapsed;

            return Visibility.Visible;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}