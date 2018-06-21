using System;
using System.Globalization;
using System.Windows.Controls;
using System.Windows.Data;

namespace Client.Converts
{
    public class AnswerCharToBool : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType != typeof(bool)||parameter is null) return null;
            if (string.IsNullOrEmpty(value.ToString())) return true;
            if (parameter is null) return false;
            if (parameter.Equals(value)) return true;
            return false;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}