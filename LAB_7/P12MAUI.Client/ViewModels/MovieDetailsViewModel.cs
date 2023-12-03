using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.ApplicationModel;
using P04WeatherForecastAPI.Client.ViewModels;
using P06Shop.Shared.MessageBox;
using P06Shop.Shared.Services.MovieService;
using P06Shop.Shared.MovieRental;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P12MAUI.Client.ViewModels
{
    [QueryProperty(nameof(Movie), nameof(Movie))]
    [QueryProperty(nameof(MoviesViewModel), nameof(MoviesViewModel))]
    public partial class MovieDetailsViewModel : ObservableObject
    {
        private readonly IMovieService _movieService;
        private readonly IMessageDialogService _messageDialogService;
        private readonly IGeolocation _geolocation;
        private readonly IMap _map;
        private MoviesViewModel _movieViewModel;

        public MovieDetailsViewModel(IMovieService movieService, IMessageDialogService messageDialogService, IGeolocation geolocation, IMap map)
        {
            _map = map;
            _movieService = movieService;
            _messageDialogService = messageDialogService;
            _geolocation = geolocation;

            
        }

        public MoviesViewModel MoviesViewModel
        {
            get
            {
                return _movieViewModel;
            }
            set
            {
                _movieViewModel = value;
            }
        }


        [ObservableProperty]
        Movie movie;

        public async Task DeleteMovie()
        {
            await _movieService.DeleteMovieAsync(movie.Id);
            await _movieViewModel.GetMovies();
        }

        public async Task CreateMovie()
        {
            var newMovie = new Movie()
            {
                Title = movie.Title,
                Year = movie.Year,
                Director = movie.Director,
                Rating = movie.Rating,
            };

            var result = await _movieService.CreateMovieAsync(newMovie);
            if (result.Success)
                await _movieViewModel.GetMovies();
            else
                _messageDialogService.ShowMessage(result.Message);
        }

        public async Task UpdateMovie()
        {
            var movieToUpdate = new Movie()
            {
                Id = movie.Id,
                Title = movie.Title,
                Year = movie.Year,
                Director = movie.Director,
                Rating = movie.Rating,
            };

            await _movieService.UpdateMovieAsync(movieToUpdate);
            await _movieViewModel.GetMovies();
        }


        [RelayCommand]
        public async Task Save()
        {
            if (movie.Id == 0)
            {
                CreateMovie();
                await Shell.Current.GoToAsync("../", true);

            }
            else
            {
                UpdateMovie();
                await Shell.Current.GoToAsync("../", true);
            }

        }

        [RelayCommand]
        public async Task Delete()
        {
            DeleteMovie();
            await Shell.Current.GoToAsync("../", true);
        }
    }
}
