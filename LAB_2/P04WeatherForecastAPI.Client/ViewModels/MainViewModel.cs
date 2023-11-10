using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;
using P04WeatherForecastAPI.Client.Commands;
using P04WeatherForecastAPI.Client.DataSeeders;
using P04WeatherForecastAPI.Client.Models;
using P04WeatherForecastAPI.Client.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace P04WeatherForecastAPI.Client.ViewModels
{
    // przekazywanie wartosci do innego formularza 
    public partial class MainViewModel : ObservableObject
    {
        private CityViewModel _selectedCity;
        private Weather _weather;
		private Weather _weatherYesterday;
		private ForecastHourWeather _weather1Hour;
		private ForecastDailyWeather _weatherTommorow;
		private PollenAndAllergensForecast _indices;
		private ForecastDailyWeather _weather5Days;
        private readonly IAccuWeatherService _accuWeatherService;
        private readonly FavoriteCitiesView _favoriteCitiesView;
        private readonly FavoriteCityViewModel _favoriteCityViewModel;
        //public ICommand LoadCitiesCommand { get;  }

        //[ObservableProperty]
        //private WeatherViewModel weatherView;
        //public WeatherViewModel WeatherView { 
        //    get { return weatherView; } 
        //    set { 
        //        weatherView = value;
        //        OnPropertyChanged();
        //    }
        //}
        [ObservableProperty]
        private WeatherViewModel weatherView;

		[ObservableProperty]
		private WeatherViewModel weatherYesterdayView;

		[ObservableProperty]
		private ForecastHourWeatherViewModel forecastHourWeatherView;

		[ObservableProperty]
		private ForecastDailyWeatherViewModel forecastDailyWeatherView;

		[ObservableProperty]
		private ForecastDailyWeatherViewModel forecast5DaysWeatherView;

		[ObservableProperty]
		private PollenAndAllergensForecastViewModel pollenAndAllergensForecastView;


		public MainViewModel(IAccuWeatherService accuWeatherService, FavoriteCityViewModel favoriteCityViewModel, FavoriteCitiesView favoriteCitiesView)
        {
            _favoriteCitiesView = favoriteCitiesView;
            _favoriteCityViewModel = favoriteCityViewModel;
            // _serviceProvider= serviceProvider; 
            //LoadCitiesCommand = new RelayCommand(x => LoadCities(x as string));
            _accuWeatherService = accuWeatherService;
            Cities = new ObservableCollection<CityViewModel>(); // podejście nr 2 
        }

        public CityViewModel SelectedCity
        {
            get => _selectedCity;
            set
            {
                _selectedCity = value;
                OnPropertyChanged();
                LoadWeather();
            }
        }

         
        private async void LoadWeather()
        {
            if(SelectedCity != null)
            {
                _weather = await _accuWeatherService.GetCurrentConditions(SelectedCity.Key); 
                WeatherView = new WeatherViewModel(_weather);
				
				_weatherYesterday = await _accuWeatherService.GetHistoricalCurrentConditions24(SelectedCity.Key);
				WeatherYesterdayView = new WeatherViewModel(_weatherYesterday);

				_weather1Hour = await _accuWeatherService.GetForecast1Hour(SelectedCity.Key);
				ForecastHourWeatherView = new ForecastHourWeatherViewModel(_weather1Hour);

				_weatherTommorow = await _accuWeatherService.GetForecastDaily(SelectedCity.Key);
				ForecastDailyWeatherView = new ForecastDailyWeatherViewModel(_weatherTommorow);

				_indices = await _accuWeatherService.GetIndicesDaily(SelectedCity.Key);
				PollenAndAllergensForecastView = new PollenAndAllergensForecastViewModel(_indices);

				_weather5Days = await _accuWeatherService.Get5DaysForecast(SelectedCity.Key);
				Forecast5DaysWeatherView = new ForecastDailyWeatherViewModel(_weather5Days);
				
            }
        } 

        // podajście nr 2 do przechowywania kolekcji obiektów:
        public ObservableCollection<CityViewModel> Cities { get; set; }

        [RelayCommand]
        public async void LoadCities(string locationName)
        {
            // podejście nr 2:
            var cities = await _accuWeatherService.GetLocations(locationName);
            Cities.Clear();
            foreach (var city in cities) 
                Cities.Add(new CityViewModel(city));
        }

        [RelayCommand]
        public void OpenFavotireCities()
        {
            //var favoriteCitiesView = new FavoriteCitiesView();
            _favoriteCityViewModel.SelectedCity = new FavoriteCity() { Name = "Warsaw" };
            _favoriteCitiesView.Show();
        }
    }
}