using System;
using System.Globalization;
using System.Net;
using System.Windows;
using System.Windows.Data;

namespace PlayerApp.Converts
{
    public class IpToStringConverter:IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if ((targetType != typeof(string) ||value is null ))return null;
            IPAddress ip = value as IPAddress;
            return ip.ToString();
           

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (targetType != typeof(IPAddress)) return null;
            string adres = value as string;
            IPAddress ip;
            try
            {
               ip = IPAddress.Parse(adres);
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return null;
            }

            return ip;
        }
    }
}