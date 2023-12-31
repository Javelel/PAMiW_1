using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.VisualBasic.FileIO;
using Newtonsoft.Json;
using P04WeatherForecastAPI.Client.Configuration;
using P06Shop.Shared;
using P06Shop.Shared.Services.MovieService;
using P06Shop.Shared.MovieRental;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace P04WeatherForecastAPI.Client.Services.MovieServices
{
    internal class MovieService : IMovieService
    {
        private readonly HttpClient _httpClient;
        private readonly AppSettings _appSettings;
        public MovieService(HttpClient httpClient, IOptions<AppSettings> appSettings)
        {
            _httpClient= httpClient;
            _appSettings= appSettings.Value;
        }


        //// skopiowane z postmana 
        //public async Task<ServiceResponse<List<Product>>> GetProductsAsync()
        //{
        //    //var client = new HttpClient();   
        //    var request = new HttpRequestMessage(HttpMethod.Get, _appSettings.BaseProductEndpoint.GetAllProductsEndpoint);
        //    var response = await _httpClient.SendAsync(request);
        //    response.EnsureSuccessStatusCode();        
        //    var json = await response.Content.ReadAsStringAsync();
        //    var result = JsonConvert.DeserializeObject<ServiceResponse<List<Product>>>(json);
        //    return result;
        //}


        // alternatywny sposób pobierania danych 
        public async Task<ServiceResponse<List<Movie>>> GetAllMoviesAsync()
        {
            var response = await _httpClient.GetAsync(_appSettings.BaseMovieEndpoint.GetAllMoviesEndpoint);
            var json = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<ServiceResponse<List<Movie>>>(json);
            return result;
        }

		Task<ServiceResponse<Movie>> IMovieService.CreateMovieAsync(Movie newMovie)
		{
			throw new NotImplementedException();
		}

		Task<ServiceResponse<bool>> IMovieService.DeleteMovieAsync(int id)
		{
			throw new NotImplementedException();
		}

		Task<ServiceResponse<Movie>> IMovieService.GetMovieByIdAsync(int id)
		{
			throw new NotImplementedException();
		}

		Task<ServiceResponse<Movie>> IMovieService.UpdateMovieAsync(Movie updatedMovie)
		{
			throw new NotImplementedException();
		}
	}
}
