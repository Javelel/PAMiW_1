using P04WeatherForecastAPI.Client.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P04WeatherForecastAPI.Client.Services
{
    public interface IAccuWeatherService
    {
        Task<City[]> GetLocations(string locationName);
        Task<Weather> GetCurrentConditions(string cityKey);
        Task<Weather> GetHistoricalCurrentConditions24(string cityKey);
        Task<ForecastHourWeather> GetForecast1Hour(string cityKey);
        Task<ForecastDailyWeather> GetForecastDaily(string cityKey);
        Task<PollenAndAllergensForecast> GetIndicesDaily(string cityKey);
        Task<ForecastDailyWeather> Get5DaysForecast(string cityKey);
    }
}