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
            if( value is null ||(String.IsNullOrEmpty(value.ToString()))) return new RadialGradientBrush(Colors.Blue, Colors.SlateBlue);
            if(value == "Good answer!!") return new RadialGradientBrush(Colors.Green,Colors.Chartreuse);
            if(value== "Bad answer!!") return new RadialGradientBrush(Colors.Red,Colors.Maroon);
            else return new RadialGradientBrush(Colors.Blue,Colors.SlateBlue);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}