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
using P06Shop.Shared.MessageBox;
using System.Diagnostics;

namespace P04WeatherForecastAPI.Client.ViewModels
{
	public partial class MoviesViewModel : ObservableObject
	{
		private readonly IMovieService _movieService;
		private readonly MovieDetailsView _movieDetailsView;
		private readonly IMessageDialogService _messageDialogService;
		private int _currentPage = 1;

		public List<Movie> AllMovies { get; set; }

		public ObservableCollection<Movie> MoviesOnPage { get; set; }

		[ObservableProperty]
		private Movie selectedMovie;

		public MoviesViewModel(IMovieService movieService, MovieDetailsView movieDetailsView, IMessageDialogService messageDialogService)
		{
			_movieService = movieService;
			_messageDialogService = messageDialogService;
			_movieDetailsView = movieDetailsView;
			MoviesOnPage = new ObservableCollection<Movie>();
			AllMovies = new List<Movie>();
		}

		public async Task GetMovies()
		{
			AllMovies.Clear();
			var moviesResult = await _movieService.GetAllMoviesAsync();
			Debug.WriteLine("get movies");
			if (moviesResult.Success)
			{
				Debug.WriteLine("get movies success");
				foreach (var m in moviesResult.Data)
				{
					Debug.WriteLine("getting");
					AllMovies.Add(m);
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
				_currentPage = value;
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

		public async Task<bool> CreateMovie()
		{
			var newMovie = new Movie()
			{
				Title = selectedMovie.Title,
				Year = selectedMovie.Year,
				Director = selectedMovie.Director,
				Rating = selectedMovie.Rating,
			};

			var result = await _movieService.CreateMovieAsync(newMovie);
			if (result.Success)
			{
				await GetMovies();
				return true;
			}
			else
			{
				_messageDialogService.ShowMessage(result.Message);
				return false;
			}
		}

		public async Task<bool> DeleteMovie()
		{
			var res = await _movieService.DeleteMovieAsync(selectedMovie.Id);
			await GetMovies();
			if (!res.Success)
			{
				_messageDialogService.ShowMessage(res.Message);
			}

			return res.Success;
		}

		public async Task<bool> UpdateMovie()
		{
			var movieToUpdate = new Movie()
			{
				Id = selectedMovie.Id,
				Title = selectedMovie.Title,
				Year = selectedMovie.Year,
				Director = selectedMovie.Director,
				Rating = selectedMovie.Rating,
			};

			var res = await _movieService.UpdateMovieAsync(movieToUpdate);
			GetMovies();

			if (!res.Success)
			{
				_messageDialogService.ShowMessage(res.Message);
			}

			return res.Success;
		}

		[RelayCommand]
		public async Task ShowDetails(Movie movie)
		{
			_movieDetailsView.Show();
			_movieDetailsView.DataContext = this;
			SelectedMovie = movie;
		}


		[RelayCommand]
		public async Task Save()
		{
			bool success;
			if (selectedMovie.Id == 0)
			{
				success = await CreateMovie();
			}
			else
			{
				success = await UpdateMovie();
			}
			if (success)
			{
				_movieDetailsView.Hide();
			}

		}

		[RelayCommand]
		public async Task Delete()
		{
			bool success = await DeleteMovie();
			if (success)
			{
				_movieDetailsView.Hide();
			}
		}

		[RelayCommand]
		public async Task New()
		{
			_movieDetailsView.Show();
			_movieDetailsView.DataContext = this;
			SelectedMovie = new Movie();
		}

	}
}
