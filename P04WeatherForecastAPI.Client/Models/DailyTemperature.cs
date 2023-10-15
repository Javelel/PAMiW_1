using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P04WeatherForecastAPI.Client.Models
{
    internal class DailyTemperature
    {
        public ForecastTemperature Minimum { get; set; }
        public ForecastTemperature Maximum { get; set; }
    }
}