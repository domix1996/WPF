using System;
using System.Globalization;
using System.Windows.Data;

namespace Client.Converts
{
    public class ErrorStingToBool:IValueConverter
        {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType != typeof(bool)) return null;
            if (value is null) return true;
            if (String.IsNullOrEmpty(value.ToString())) return true;
            else return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}