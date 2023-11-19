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
using System.Net.Http.Json;

namespace P04WeatherForecastAPI.Client.Services.MovieServices
{
	internal class MovieService : IMovieService
	{
		private readonly HttpClient _httpClient;
		private readonly AppSettings _appSettings;
		public MovieService(HttpClient httpClient, IOptions<AppSettings> appSettings)
		{
			_httpClient = httpClient;
			_appSettings = appSettings.Value;
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
			var url = _appSettings.BaseMovieEndpoint.GetAllMoviesEndpoint;

			var response = await _httpClient.GetAsync(url);
			var result = await response.Content.ReadFromJsonAsync<ServiceResponse<List<Movie>>>();
			return result;
		}

		public async Task<ServiceResponse<Movie>> CreateMovieAsync(Movie newMovie)
		{
			var url = _appSettings.BaseMovieEndpoint.CreateMovieEndpoint;

			var response = await _httpClient.PostAsJsonAsync(url, newMovie);
			var result = await response.Content.ReadFromJsonAsync<ServiceResponse<Movie>>();
			return result;
		}

		public async Task<ServiceResponse<bool>> DeleteMovieAsync(int id)
		{
			string uri = _appSettings.BaseAPIUrl + "/" + _appSettings.BaseMovieEndpoint.Base_url;
			uri += string.Format(_appSettings.BaseMovieEndpoint.DeleteMovieEndpoint, id);

			var response = await _httpClient.DeleteAsync(uri);
			var json = await response.Content.ReadAsStringAsync();
			var result = JsonConvert.DeserializeObject<ServiceResponse<bool>>(json);


			return result;
		}

		public async Task<ServiceResponse<Movie>> GetMovieByIdAsync(int id)
		{
			var url = _appSettings.BaseMovieEndpoint.GetMovieByIdEndpoint.Replace("{id}", id.ToString());

			var response = await _httpClient.GetAsync(url);
			var result = await response.Content.ReadFromJsonAsync<ServiceResponse<Movie>>();
			return result;
		}

		public async Task<ServiceResponse<Movie>> UpdateMovieAsync(Movie updatedMovie)
		{
			string uri = _appSettings.BaseMovieEndpoint.UpdateMovieEndpoint;
			Console.WriteLine("uri = " + uri);
			Console.WriteLine("Id = " + updatedMovie.Id);

			var response = await _httpClient.PutAsJsonAsync(uri, updatedMovie);
			var result = await response.Content.ReadFromJsonAsync<ServiceResponse<Movie>>();
			return result;
		}
	}
}
