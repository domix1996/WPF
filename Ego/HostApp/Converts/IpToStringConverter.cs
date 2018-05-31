using System;
using System.Globalization;
using System.Net;
using System.Windows.Data;

namespace HostApp.Converts
{
    public class IpToStringConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType != typeof(string) ||value is null) return null;
            IPAddress ip = value as IPAddress;
            return ip.ToString();

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType != typeof(IPAddress)) return null;
            string adres = value as string;

            return IPAddress.Parse(adres);
        }
    }
}