using System;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Windows.Data;
using System.Windows.Media;

namespace Convertery.Converters
{
    public class HexStringToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ( string.IsNullOrWhiteSpace(value.ToString()) || targetType != typeof(Brush) )
            {
                return "#fff";
            }

            string hexColor = value.ToString();
            if (hexColor.Length > 0 && hexColor[0] != '#')
                hexColor = '#' + hexColor;
            return new SolidColorBrush
            ((Color) ColorConverter.ConvertFromString(hexColor)
            );
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}