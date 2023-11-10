using P04WeatherForecastAPI.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P04WeatherForecastAPI.Client.ViewModels
{
    public class ForecastDailyWeatherViewModel
    {
        public ForecastDailyWeatherViewModel(ForecastDailyWeather weather)
        {
            MinTemperature = weather.DailyForecasts[0].Temperature.Minimum.Value;
            MaxTemperature = weather.DailyForecasts[0].Temperature.Maximum.Value;

			Precipitaions = new string[weather.DailyForecasts.Length];

			for (int i=0; i < weather.DailyForecasts.Length; i++) {
					if (weather.DailyForecasts[i] != null) {
						if (weather.DailyForecasts[i].Day.HasPrecipitation) {
							Precipitaions[i] = Convert.ToString(i+1) + " - yes";
						} else {
							Precipitaions[i] = Convert.ToString(i+1) + " - no";
						}
					}
				}
        }
        public double MinTemperature { get; set; }
        public double MaxTemperature { get; set; }
		public string[] Precipitaions { get; set; }
    }
}