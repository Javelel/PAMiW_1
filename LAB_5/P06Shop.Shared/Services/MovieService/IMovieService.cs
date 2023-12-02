using P06Shop.Shared;
using P06Shop.Shared.MovieRental;

namespace P06Shop.Shared.Services.MovieService
{
	public interface IMovieService
	{
		Task<ServiceResponse<List<Movie>>> GetAllMoviesAsync();
		Task<ServiceResponse<Movie>> GetMovieByIdAsync(int id);
		Task<ServiceResponse<Movie>> CreateMovieAsync(Movie newMovie);
		Task<ServiceResponse<Movie>> UpdateMovieAsync(Movie updatedMovie);
		Task<ServiceResponse<bool>> DeleteMovieAsync(int id);
	}
}
