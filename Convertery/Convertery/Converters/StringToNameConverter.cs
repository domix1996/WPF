using System;
using System.Globalization;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Windows.Data;

namespace Convertery.Converters
{
    public class StringToNameConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType != typeof(string))
                return null;

            if (string.IsNullOrWhiteSpace(value?.ToString()))
                return "Nieznajomy";

            return value;
            //if (value==null)
            //{
            //    return null;
            //}
            //if (string.IsNullOrWhiteSpace(value.ToString()))
            //{
            //    return "Nieznajomy";
            //}
            //else return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return null;
        }
    }
}