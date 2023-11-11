using CommunityToolkit.Mvvm.ComponentModel;
using P04WeatherForecastAPI.Client.Models;
using P04WeatherForecastAPI.Client.Services.WeatherServices;
using P06Shop.Shared.Services.MovieService;
using P06Shop.Shared.MovieRental;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using CommunityToolkit.Mvvm.Input;

namespace P04WeatherForecastAPI.Client.ViewModels
{
    public partial class MoviesViewModel : ObservableObject
    {
        private readonly IMovieService _movieService;
		private int _currentPage = 1;

		public List<Movie> AllMovies { get; set; }

        public ObservableCollection<Movie> MoviesOnPage { get; set; }

        public MoviesViewModel(IMovieService movieService)
        {
            _movieService = movieService;
            MoviesOnPage = new ObservableCollection<Movie>();
			AllMovies = new List<Movie>();
        }

        public async void GetMovies()
        {
            var moviesResult = await _movieService.GetAllMoviesAsync();
            if (moviesResult.Success)
            {
                foreach (var p in moviesResult.Data)
                {
                    AllMovies.Add(p);
                }
				LoadMoviesOnPage();
            }
        }

		private void LoadMoviesOnPage()
		{
			MoviesOnPage.Clear();
			int maxItemsOnPage = 5;
			LastPage = Convert.ToInt32(Math.Ceiling((double)AllMovies.Count / maxItemsOnPage));
			if (LastPage == 0)
			{
				LastPage = 1;
			}
			for (int i = (CurrentPage - 1) * maxItemsOnPage; i < (CurrentPage - 1) * maxItemsOnPage + maxItemsOnPage; i++)
            {
                if (i > AllMovies.Count - 1)
                {
                    break;
                }
                MoviesOnPage.Add(AllMovies[i]);
            }
		}

		[ObservableProperty]
		public int lastPage;

		public int CurrentPage
		{
			get => _currentPage;
			set
			{
				SetProperty(ref _currentPage, value);
				LoadMoviesOnPage();
				OnPropertyChanged();
			}
		}

		[RelayCommand]
		public void NextPage()
		{
			if (CurrentPage < LastPage)
			{
				CurrentPage++;
				LoadMoviesOnPage();
				OnPropertyChanged();
			}
		}

		[RelayCommand]
		public void PreviousPage()
		{
			if (CurrentPage > 1)
			{
				CurrentPage--;
				LoadMoviesOnPage();
				OnPropertyChanged();
			}
		}

	}
}
