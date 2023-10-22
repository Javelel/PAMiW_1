using P04WeatherForecastAPI.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P04WeatherForecastAPI.Client.ViewModels
{
    public class ForecastHourWeatherViewModel
    {
        public ForecastHourWeatherViewModel(ForecastHourWeather weather)
        {
            Temperature = weather.Temperature.Value;
            HasPrecipitation = weather.HasPrecipitation;
        }
        public double Temperature { get; set; }
        public bool HasPrecipitation { get; set; }
    }
}
