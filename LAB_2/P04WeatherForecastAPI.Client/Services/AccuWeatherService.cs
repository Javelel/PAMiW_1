using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using P04WeatherForecastAPI.Client.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace P04WeatherForecastAPI.Client.Services
{
    internal class AccuWeatherService : IAccuWeatherService
    {
        private const string base_url = "http://dataservice.accuweather.com";
        private const string autocomplete_endpoint = "locations/v1/cities/autocomplete?apikey={0}&q={1}&language={2}";
        private const string current_conditions_endpoint = "currentconditions/v1/{0}?apikey={1}&language={2}";


		private const string historical_current_conditions_24_endpoint = "currentconditions/v1/{0}/historical/24?apikey={1}&language={2}";
		private const string forecast_1_hour_endpoint = "forecasts/v1/hourly/1hour/{0}?apikey={1}&language={2}&metric=true";
		private const string forecast_daily_endpoint = "forecasts/v1/daily/1day/{0}?apikey={1}&language={2}&metric=true";
		private const string indices_daily_endpoint = "indices/v1/daily/1day/{0}/18?apikey={1}&language={2}";
		private const string forcast_5_days = "forecasts/v1/daily/5day/{0}?apikey={1}&language={2}&metric=true";

        // private const string api_key = "5hFl75dja3ZuKSLpXFxUzSc9vXdtnwG5";
        string api_key;
        //private const string language = "pl";
        string language;

        public AccuWeatherService()
        {
            var builder = new ConfigurationBuilder()
                .AddUserSecrets<App>()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsetings.json"); 

            var configuration = builder.Build();
            api_key = configuration["api_key"];
            language = configuration["default_language"];
        }


        public async Task<City[]> GetLocations(string locationName)
        {
            string uri = base_url + "/" + string.Format(autocomplete_endpoint, api_key, locationName, language);
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(uri);
                string json = await response.Content.ReadAsStringAsync();
                City[] cities = JsonConvert.DeserializeObject<City[]>(json);
                return cities;

            }
        }

        public async Task<Weather> GetCurrentConditions(string cityKey)
        {
            string uri = base_url + "/" + string.Format(current_conditions_endpoint, cityKey, api_key,language);
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(uri);
                string json = await response.Content.ReadAsStringAsync();
                Weather[] weathers= JsonConvert.DeserializeObject<Weather[]>(json);
                return weathers.FirstOrDefault();
            }
        }

		// ---*new*---

		public async Task<Weather> GetHistoricalCurrentConditions24(string cityKey)
        {
            string uri = base_url + "/" + string.Format(historical_current_conditions_24_endpoint, cityKey, api_key,language);
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(uri);
                string json = await response.Content.ReadAsStringAsync();
                Weather[] weathers= JsonConvert.DeserializeObject<Weather[]>(json);
                return weathers.FirstOrDefault();
            }
        }
		
		public async Task<ForecastHourWeather> GetForecast1Hour(string cityKey)
        {
            string uri = base_url + "/" + string.Format(forecast_1_hour_endpoint, cityKey, api_key,language);
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(uri);
                string json = await response.Content.ReadAsStringAsync();
                ForecastHourWeather[] weathers= JsonConvert.DeserializeObject<ForecastHourWeather[]>(json);
                return weathers.FirstOrDefault();
            }
        }

		public async Task<ForecastDailyWeather> GetForecastDaily(string cityKey)
        {
            string uri = base_url + "/" + string.Format(forecast_daily_endpoint, cityKey, api_key,language);
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(uri);
                string json = await response.Content.ReadAsStringAsync();
                ForecastDailyWeather weathers= JsonConvert.DeserializeObject<ForecastDailyWeather>(json);
                return weathers;
            }
        }

		public async Task<PollenAndAllergensForecast> GetIndicesDaily(string cityKey)
        {
            string uri = base_url + "/" + string.Format(indices_daily_endpoint, cityKey, api_key,language);
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(uri);
                string json = await response.Content.ReadAsStringAsync();
                PollenAndAllergensForecast[] weathers= JsonConvert.DeserializeObject<PollenAndAllergensForecast[]>(json);
                return weathers.FirstOrDefault();
            }
        }

		public async Task<ForecastDailyWeather> Get5DaysForecast(string cityKey)
        {
            string uri = base_url + "/" + string.Format(forcast_5_days, cityKey, api_key,language);
            using (HttpClient client = new HttpClient())
            {
                var response = await client.GetAsync(uri);
                string json = await response.Content.ReadAsStringAsync();
                ForecastDailyWeather weathers= JsonConvert.DeserializeObject<ForecastDailyWeather>(json);
                return weathers;
            }
        }

    }
}
