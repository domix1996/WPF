using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Windows.Data;
using System.Windows.Media;
using Brush = System.Windows.Media.Brush;

namespace Client.Converts
{
    public class AnswerInfoToBackgroundColor:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType != typeof(Brush)) return null;
            if( value is null ||(String.IsNullOrEmpty(value.ToString()))) return new SolidColorBrush(Colors.DarkCyan);
            if(value == "Good answer!!") return new SolidColorBrush(Colors.Green);
            if(value== "Bad answer!!") return new SolidColorBrush(Colors.Red);
            else return new SolidColorBrush(Colors.DarkCyan);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}