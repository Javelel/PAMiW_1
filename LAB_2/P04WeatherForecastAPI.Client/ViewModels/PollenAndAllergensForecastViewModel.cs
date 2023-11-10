using P04WeatherForecastAPI.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P04WeatherForecastAPI.Client.ViewModels
{
    public class PollenAndAllergensForecastViewModel
    {
        public PollenAndAllergensForecastViewModel(PollenAndAllergensForecast indices)
        {
            Category = indices.Category;
        }
        public string Category { get; set; }
    }
}