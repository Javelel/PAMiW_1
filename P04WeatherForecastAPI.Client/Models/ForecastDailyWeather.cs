using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P04WeatherForecastAPI.Client.Models
{
    public class ForecastDailyWeather
    {
        public Headline Headline { get; set; }
    	public DailyForecast[] DailyForecasts { get; set; }
    }
}