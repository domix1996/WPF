using System;
using System.Windows.Media;
using System.Globalization;
using System.Windows.Data;

namespace Client.Converts
{
    public class AnswerCharToBackgroundColor:IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //if (targetType != typeof(Brush)) return null;
            //else if (value is null || string.IsNullOrEmpty(value.ToString())) return new SolidColorBrush(Colors.LightSlateGray);
            //else if (value.Equals(parameter)) return new SolidColorBrush(Colors.Chartreuse);
            //return new SolidColorBrush(Colors.Crimson);
            return null;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

    }
}