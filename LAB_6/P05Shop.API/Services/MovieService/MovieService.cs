using P06Shop.Shared;
using P06Shop.Shared.Services.MovieService;
using P06Shop.Shared.MovieRental;
using P07Shop.DataSeeder;
using P05Shop.API.Exceptions;
using P05Shop.API.Models;
using Microsoft.EntityFrameworkCore;

namespace P05Shop.API.Services.MovieService
{
	public class MovieService : IMovieService
	{
		private readonly DataContext _dataContext;

		public MovieService(DataContext context)
		{
			_dataContext = context;
		}

		public async Task<ServiceResponse<List<Movie>>> GetAllMoviesAsync()
		{
			var movies = await _dataContext.Movies.ToListAsync();
			try
			{
				var response = new ServiceResponse<List<Movie>>()
				{
					Data = movies,
					Message = "Ok",
					Success = true
				};

				return response;
			}
			catch (Exception)
			{
				return new ServiceResponse<List<Movie>>()
				{
					Data = null,
					Message = "Problem with database",
					Success = false
				};
			}

		}

		public async Task<ServiceResponse<Movie>> GetMovieByIdAsync(int id)
		{
			try
			{
				var movie = _dataContext.Find<Movie>(id);
				if (movie == null)
				{
					return new ServiceResponse<Movie>()
					{
						Data = null,
						Message = "Movie not found",
						Success = false
					};
				}
				else
				{
					return new ServiceResponse<Movie>()
					{
						Data = movie,
						Message = "Ok",
						Success = true
					};
				}
			}
			catch (Exception)
			{
				return new ServiceResponse<Movie>()
				{
					Data = null,
					Message = "Problem with database",
					Success = false
				};
			}

		}

		public async Task<ServiceResponse<Movie>> CreateMovieAsync(Movie newMovie)
		{
			try
			{
				bool exists = _dataContext.Find<Movie>(newMovie.Id) != null;
				if (exists)
				{
					throw new MovieAlreadyExistsException();
				}

				_dataContext.Movies.Add(newMovie);
				await _dataContext.SaveChangesAsync();

				return new ServiceResponse<Movie>() { Data = newMovie, Success = true };
			}
			catch (MovieAlreadyExistsException)
			{
				return new ServiceResponse<Movie>()
				{
					Data = null,
					Message = "Movie already exists",
					Success = false
				};
			}
			catch (Exception)
			{
				return new ServiceResponse<Movie>()
				{
					Data = null,
					Message = "Problem with database",
					Success = false
				};
			}
		}

		public async Task<ServiceResponse<bool>> DeleteMovieAsync(int id)
		{
			try
			{
				bool exists = _dataContext.Find<Movie>(id) != null;
				if (!exists)
				{
					throw new MovieDoesNotExistException();
				}

				var movieToDelete = await _dataContext.Movies.FindAsync(id);
				if (movieToDelete != null)
				{
					_dataContext.Movies.Remove(movieToDelete);
					await _dataContext.SaveChangesAsync();
				}

				return new ServiceResponse<bool>() { Data = true, Success = true };

			}
			catch (MovieDoesNotExistException)
			{
				return new ServiceResponse<bool>()
				{
					Data = false,
					Message = "Movie does not exists",
					Success = false
				};
			}
			catch (Exception)
			{
				return new ServiceResponse<bool>()
				{
					Data = false,
					Message = "Problem with database",
					Success = false
				};
			}
		}

		public async Task<ServiceResponse<Movie>> UpdateMovieAsync(Movie updatedMovie)
		{
			try
			{
				bool exists = _dataContext.Find<Movie>(updatedMovie.Id) != null;
				if (!exists)
				{
					throw new MovieDoesNotExistException();
				}

				var movieToEdit = await _dataContext.Movies.FindAsync(updatedMovie.Id);
				if (movieToEdit != null)
				{
					movieToEdit.Title = updatedMovie.Title;
					movieToEdit.Year = updatedMovie.Year;
					movieToEdit.Director = updatedMovie.Director;
					movieToEdit.Rating = updatedMovie.Rating;

					await _dataContext.SaveChangesAsync();
					return new ServiceResponse<Movie>() { Data = movieToEdit, Success = true };
				}
				else
				{
					return new ServiceResponse<Movie>()
					{
						Data = null,
						Message = "Movie does not exists",
						Success = false
					};
				}
			}
			catch (MovieDoesNotExistException)
			{
				return new ServiceResponse<Movie>()
				{
					Data = null,
					Message = "Movie does not exists",
					Success = false
				};
			}
			catch (Exception)
			{
				return new ServiceResponse<Movie>()
				{
					Data = null,
					Message = "Problem with database",
					Success = false
				};
			}
		}

		public async Task<ServiceResponse<List<Movie>>> SearchMoviesAsync(string text, int page, int pageSize)
		{
			IQueryable<Movie> query = _dataContext.Movies;

			if (!string.IsNullOrEmpty(text))
				query = query.Where(m => m.Title.Contains(text) || m.Director.Contains(text));

			var movies = await query.Skip(pageSize * (page - 1))
				.Take(pageSize)
				.ToListAsync();

			try
			{
				var response = new ServiceResponse<List<Movie>>()
				{
					Data = movies,
					Message = "Ok",
					Success = true
				};

				return response;
			}
			catch (Exception)
			{
				return new ServiceResponse<List<Movie>>()
				{
					Data = null,
					Message = "Problem with database",
					Success = false
				};
			}
		}

	}
}