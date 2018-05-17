using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Text.RegularExpressions;
using System.Windows.Navigation;
using RestSharp;

namespace Weather.ModelNamespace
{
    public class DAL
    {
        

        public static WeatherModel GetDataByCity(string cityName = "London")
        {
            var client = new RestClient("https://api.openweathermap.org");

            var request = new RestRequest("/data/2.5/weather");
            request.AddParameter("q", cityName);
            request.AddParameter("appid", "e535d78e2276a36aede2983e0aa08f44");
            var response = client.Execute<Global>(request);
            DateTime date;
            DateTime.TryParseExact(response.Data.dt.ToString(), "yyyyMMdd", CultureInfo.InvariantCulture,
                DateTimeStyles.None, out date);
            if (response.StatusCode == HttpStatusCode.OK)

                return new WeatherModel()
                {


                    Longitude = response.Data.coord.lon,
                    Latitude = response.Data.coord.lat,
                    Country = response.Data.sys.country,
                    CityName = response.Data.name,

                    ShortDescription = response.Data.weather[0].main,
                    LongDescription = response.Data.weather[0].description,
                    Icon = response.Data.weather[0].icon,
                    Temperature = response.Data.main.temp,
                    Pressure = response.Data.main.pressure,
                    Humidity = response.Data.main.humidity,
                    TempMax = response.Data.main.temp_max,
                    TempMin = response.Data.main.temp_min,

                    Speed = response.Data.wind.speed,
                    Degree = response.Data.wind.deg,

                    Sunrise = response.Data.sys.sunrise,
                    Sunset = response.Data.sys.sunset,

                    Visibility = response.Data.visibility,
                    Date = date

                };
            else return new WeatherModel();
        }

        private class Coord
        {
            public double lon { get; set; }
            public double lat { get; set; }
        }
        private class Weather
        {
            public int id { get; set; }
            public string main { get; set; }
            public string description { get; set; }
            public string icon { get; set; }
        }
        private class Main
        {
            public double temp { get; set; }
            public int pressure { get; set; }
            public int humidity { get; set; }
            public double temp_min { get; set; }
            public double temp_max { get; set; }
        }
        private class Wind
        {
            public double speed { get; set; }
            public int deg { get; set; }
        }
        private class Clouds
        {
            public int all { get; set; }
        }
        private class Sys
        {
            public int type { get; set; }
            public int id { get; set; }
            public double message { get; set; }
            public string country { get; set; }
            public int sunrise { get; set; }
            public int sunset { get; set; }
        }
        private class Global
        {
            public Coord coord { get; set; }
            public List<Weather> weather { get; set; }
            public string @base { get; set; }
            public Main main { get; set; }
            public int visibility { get; set; }
            public Wind wind { get; set; }
            public Clouds clouds { get; set; }
            public int dt { get; set; }
            public Sys sys { get; set; }
            public int id { get; set; }
            public string name { get; set; }
            public int cod { get; set; }
        }
    }


}