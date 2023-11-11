using P06Shop.Shared;
using P06Shop.Shared.Services.MovieService;
using P06Shop.Shared.MovieRental;
using P07Shop.DataSeeder;
using P05Shop.API.Exceptions;

namespace P05Shop.API.Services.MovieService
{
	public class MovieService : IMovieService
    {
        private List<Movie> _movies { get; set; }

        public MovieService()
        {
            _movies = new List<Movie>();
            _movies = MovieSeeder.GenerateMovieData();
        }

        public async Task<ServiceResponse<List<Movie>>> GetAllMoviesAsync()
        {
            try
            {
                var response = new ServiceResponse<List<Movie>>()
                {
                    Data = _movies,
                    Message = "Ok",
                    Success = true
                };

                return response;
            }
            catch (Exception)
            {
                return new  ServiceResponse<List<Movie>>()
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
                var movie = _movies.Where(x => x.Id == id).First();
                if (movie == null)
                {
                    return new ServiceResponse<Movie>()
                    {
                        Data = null,
                        Message = "Movie not found",
                        Success = false
                    };
                }
                else {
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
                return new  ServiceResponse<Movie>()
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
            	bool exists = _movies.Select(data => data.Id).Contains(newMovie.Id);
            	if (exists) {
                	throw new MovieAlreadyExistsException();
            	}
				
                _movies.Add(newMovie);

                var response = new ServiceResponse<Movie>()
                {
                    Data = newMovie,
                    Message = "Ok",
                    Success = true
                };

                return response;
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
            	bool exists = await Task.FromResult(_movies.Select(data => data.Id).Contains(id));
            	if (!exists) {
                	throw new MovieDoesNotExistException();
            	}
                _movies = _movies.Where(x => x.Id != id).ToList();

                var response = new ServiceResponse<bool>()
                {
                    Data = true,
                    Message = "Ok",
                    Success = true
                };

                return response;
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
                bool exists = await Task.FromResult(_movies.Select(data => data.Id).Contains(updatedMovie.Id));
            	if (!exists) {
                	throw new MovieDoesNotExistException();
            	}
                _movies = _movies.Where(x => x.Id != updatedMovie.Id).ToList();
                _movies.Add(updatedMovie);

                var response = new ServiceResponse<Movie>()
                {
                    Data = updatedMovie,
                    Message = "Ok",
                    Success = true
                };

                return response;
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
    }
}