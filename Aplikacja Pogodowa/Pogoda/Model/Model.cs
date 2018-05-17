using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Pogoda.Annotations;

namespace Weather.ModelNamespace
{
    public class WeatherModel : INotifyPropertyChanged
    {

        //GeoInfo
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string Country { get; set; }
        public string CityName { get; set; }
        //WeatherInfo
        public string ShortDescription { get; set; }
        public string LongDescription { get; set; }
        public string Icon { get; set; }
        public double Temperature { get; set; }
        public int Pressure { get; set; }
        public int Humidity { get; set; }
        public double TempMin { get; set; }
        public double TempMax { get; set; }
        //WindInfo
        public double Speed { get; set; }
        public int Degree { get; set; }
        //SunInfo
        public int Sunrise { get; set; }
        public int Sunset { get; set; }
        //OtherInfo
        public int Visibility { get; set; }
        public DateTime Date { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}