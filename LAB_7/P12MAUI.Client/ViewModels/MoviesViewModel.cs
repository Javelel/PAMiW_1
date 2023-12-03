using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using P04WeatherForecastAPI.Client.Models;
using P06Shop.Shared.MessageBox;
using P06Shop.Shared.Services.MovieService;
using P06Shop.Shared.MovieRental;
using P12MAUI.Client;
using P12MAUI.Client.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;


namespace P04WeatherForecastAPI.Client.ViewModels
{
   
 public partial class MoviesViewModel : ObservableObject
    {
        private readonly IMovieService _movieService;
        private readonly MovieDetailsView _movieDetailsView;
        private readonly IMessageDialogService _messageDialogService;
        private readonly IConnectivity _connectivity;
		private int _currentPage = 1;
		public List<Movie> AllMovies { get; set; }
        public ObservableCollection<Movie> PageMovies { get; set; }

        [ObservableProperty]
        private Movie selectedMovie;

        public MoviesViewModel(IMovieService movieService, MovieDetailsView movieDetailsView, IMessageDialogService messageDialogService,
            IConnectivity connectivity)
        {
            _messageDialogService = messageDialogService;
            _movieDetailsView = movieDetailsView;
            _movieService = movieService;
            _connectivity = connectivity; // set the _connectivity field
            PageMovies = new ObservableCollection<Movie>();
			AllMovies = new List<Movie>();
            GetMovies();
        }

        public async Task GetMovies()
        {
            AllMovies.Clear();
            if (_connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                _messageDialogService.ShowMessage("Internet not available!");
                return;
            }

            var moviesResult = await _movieService.GetAllMoviesAsync();
            if (moviesResult.Success)
            {
                foreach (var p in moviesResult.Data)
                {
                    AllMovies.Add(p);
                }
				LoadMoviesOnPage();
            }
            else
            {
                _messageDialogService.ShowMessage(moviesResult.Message);
            }
        }

		private void LoadMoviesOnPage()
		{
			PageMovies.Clear();
			int ItemsPerPage = 10;

			MaxPage = Convert.ToInt32(Math.Ceiling(Convert.ToDouble(AllMovies.Count) / Convert.ToDouble(ItemsPerPage)));
			if (MaxPage == 0)
			{
				MaxPage = 1;
			}

			for (int i = (CurrentPage - 1) * ItemsPerPage; i < (CurrentPage - 1) * ItemsPerPage + ItemsPerPage; i++)
			{
				if (i > AllMovies.Count - 1)
				{
					break;
				}
				PageMovies.Add(AllMovies[i]);
			}
		}

		[ObservableProperty]
        public int maxPage;

		public int CurrentPage
		{
			get => _currentPage;
			set
			{
				_currentPage = value;
				LoadMoviesOnPage();
				OnPropertyChanged();
			}
		}

		[RelayCommand]
		public async void NextPage()
		{
			if (CurrentPage < MaxPage)
			{
				CurrentPage++;
				LoadMoviesOnPage();
				OnPropertyChanged();
			}
		}

		[RelayCommand]
		public async void PreviousPage()
		{
			if (CurrentPage > 1)
			{
				CurrentPage--;
				LoadMoviesOnPage();
				OnPropertyChanged();
			}
		}


        [RelayCommand]
        public async Task ShowDetails(Movie movie)
        {
            if (_connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                _messageDialogService.ShowMessage("Internet not available!");
                return;
            }

            await Shell.Current.GoToAsync(nameof(MovieDetailsView), true, new Dictionary<string, object>
            {
                {"Movie", movie },
                {nameof(MoviesViewModel), this }
            });
            SelectedMovie = movie;
        }

        [RelayCommand]
        public async Task New()
        {
            if (_connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                _messageDialogService.ShowMessage("Internet not available!");
                return;
            }

            SelectedMovie = new Movie();
            await Shell.Current.GoToAsync(nameof(MovieDetailsView), true, new Dictionary<string, object>
            {
                {"Movie", SelectedMovie },
                {nameof(MoviesViewModel), this }
            });
        }

    }
}
