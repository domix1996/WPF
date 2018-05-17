using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using ModelNamespace;
namespace ViewModelNamespace
{
    public class WeatherViewModel : INotifyPropertyChanged
    {
        private WeatherModel _model { get; set; } = DAL.GetDataByCity("London");

        public double Longitude
        {
            get => _model.Longitude;
            set
            {
                if (_model.Longitude != value)
                {
                    _model.Longitude = value;
                    OnPropertyChanged();

                }
            }
        }
        public double Latitude
        {
            get => _model.Latitude;
            set
            {
                if (_model.Latitude != value)
                {
                    _model.Latitude = value;
                    OnPropertyChanged();

                }
            }
        }
        public string CityName
        {
            get => _model.CityName;
            set
            {
                if (_model.CityName != value)
                {
                    _model.CityName = value;
                    OnPropertyChanged();
                }
            }
        }
        public string Country
        {
            get => _model.Country;
            set
            {
                if (_model.Country != value)
                {
                    _model.Country = value;
                    OnPropertyChanged();
                }
            }
        }
        public string ShortDescription
        {
            get => _model.ShortDescription;
            set
            {
                if (_model.ShortDescription != value)
                {
                    _model.ShortDescription = value;
                    OnPropertyChanged();

                }
            }
        }
        public string LongDescription
        {
            get => _model.LongDescription;
            set
            {
                if (_model.LongDescription != value)
                {
                    _model.LongDescription = value;
                    OnPropertyChanged();

                }
            }
        }
        public string IconName
        {
            get => _model.IconName;
            set
            {
                if (_model.IconName != value)
                {
                    _model.IconName = value;
                    OnPropertyChanged();

                }
            }
        }
        public string IconUrl
        {
            get => _model.IconUrl;
            set
            {
                if (_model.IconUrl != value)
                {
                    _model.IconUrl = value;
                    OnPropertyChanged();

                }
            }
        }
        public double Temperature
        {
            get => _model.Temperature;
            set
            {
                if (Math.Abs(_model.Temperature - value) > 0.000001)
                {
                    _model.Temperature = value;
                    OnPropertyChanged();

                }
            }
        }
        public int Pressure
        {
            get => _model.Pressure;
            set
            {
                if (_model.Pressure != value)
                {
                    _model.Pressure = value;
                    OnPropertyChanged();

                }
            }
        }
        public int Humidity
        {
            get => _model.Humidity;
            set
            {
                if (_model.Humidity == value) return;
                _model.Humidity = value;
                OnPropertyChanged();
            }
        }
        public double TempMin
        {
            get => _model.TempMin;
            set
            {
                if (Math.Abs(_model.TempMin - value) > 0.000001)
                {
                    _model.TempMin = value;
                    OnPropertyChanged();

                }
            }
        }
        public double TempMax
        {
            get => _model.TempMax;
            set
            {
                if (_model.TempMax != value)
                {
                    _model.TempMax = value;
                    OnPropertyChanged();

                }
            }
        }
        public double Speed
        {
            get => _model.Speed;
            set
            {
                if (_model.Speed != value)
                {
                    _model.Speed = value;
                    OnPropertyChanged();

                }
            }
        }
        public int Degree
        {
            get => _model.Degree;
            set
            {
                if (_model.Degree != value)
                {
                    _model.Degree = value;
                    OnPropertyChanged();

                }
            }
        }
        public string Direct
        {
            get => _model.Direct;
            set
            {
                if (_model.Direct != value)
                {
                    _model.Direct = value;
                    OnPropertyChanged();

                }
            }
        }
        public string DirectIcon
        {
            get => _model.DirectIcon;
            set
            {
                if (_model.DirectIcon != value)
                {
                    _model.DirectIcon = value;
                    OnPropertyChanged();

                }
            }
        }
        public int Sunrise
        {
            get => _model.Sunrise;
            set
            {
                if (_model.Sunrise != value)
                {
                    _model.Sunrise = value;
                    OnPropertyChanged();

                }
            }
        }
        public int Sunset
        {
            get => _model.Sunset;
            set
            {
                if (_model.Sunset != value)
                {
                    _model.Sunset = value;
                    OnPropertyChanged();

                }
            }
        }
        public int Visibility
        {
            get => _model.Visibility;
            set
            {
                if (_model.Visibility != value)
                {
                    _model.Visibility = value;
                    OnPropertyChanged();

                }
            }
        }
        public DateTime Date
        {
            get => _model.Date;
            set
            {
                if (_model.Date != value)
                {
                    _model.Date = value;
                    OnPropertyChanged();

                }
            }
        }

        public void RefreshData(string city)
        {
            _model = ModelNamespace.DAL.GetDataByCity(city);
            OnPropertyChanged();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}