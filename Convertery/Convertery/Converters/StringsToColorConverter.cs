using System;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Windows.Data;
using System.Windows.Markup;
using System.Windows.Media;

namespace Convertery.Converters
{
    public class StringsToColorConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType != typeof(Brush)) return null;
            if (values is null || values.Length != 3) return null;
            for (int i = 0; i < 3; i++)
            {
               
                if (values[i].ToString().Length == 0) values[i] = "00";
                if (values[i].ToString().Length == 1) values[i] = $"0{values[i]}";
                if (values[i].ToString().Length > 2) return null;

            }
            string strcolor = $"#FF{values[0]}{values[1]}{values[2]}";
            var color = (Color) ColorConverter.ConvertFromString(strcolor);
            return new SolidColorBrush(color);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}