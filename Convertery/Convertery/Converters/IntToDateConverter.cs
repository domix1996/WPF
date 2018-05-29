using System;
using System.Globalization;
using System.Windows.Data;

namespace Convertery.Converters
{
    public class IntToDateConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType!=typeof(string)||value==null)
            {
                return null;
            }

            if (int.TryParse(value.ToString(),out int year))
            {
                return DateTime.Now.AddYears(-year);
            }

            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}