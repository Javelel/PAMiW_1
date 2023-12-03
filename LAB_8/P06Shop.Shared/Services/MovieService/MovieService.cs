using Microsoft.Extensions.Options;
using Microsoft.VisualBasic.FileIO;
using Newtonsoft.Json;
using P06Shop.Shared;
using P06Shop.Shared.Configuration;
using P06Shop.Shared.MovieRental;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;

namespace P06Shop.Shared.Services.MovieService
{
	public class MovieService : IMovieService
	{
		private readonly HttpClient _httpClient;
		private readonly AppSettings _appSettings;

		public MovieService(HttpClient httpClient, AppSettings appSettings)
		{
			_httpClient = httpClient;
			_appSettings = appSettings;
		}

		public async Task<ServiceResponse<List<Movie>>> GetAllMoviesAsync()
		{
			var url = _appSettings.BaseAPIUrl + "/" + _appSettings.MovieEndpoint.GetAllMoviesEndpoint;
			var response = await _httpClient.GetAsync(url);
			if (!response.IsSuccessStatusCode)
				return new ServiceResponse<List<Movie>>
				{
					Success = false,
					Message = "HTTP request failed"
				};

			var json = await response.Content.ReadAsStringAsync();
			var result = JsonConvert.DeserializeObject<ServiceResponse<List<Movie>>>(json)
				?? new ServiceResponse<List<Movie>> { Success = false, Message = "Deserialization failed" };

			result.Success = result.Success && result.Data != null;

			return result;

		}

		public async Task<ServiceResponse<Movie>> GetMovieByIdAsync(int id)
		{
			var url = _appSettings.BaseAPIUrl + "/" + _appSettings.MovieEndpoint.GetMovieByIdEndpoint.Replace("{id}", id.ToString());
			var response = await _httpClient.GetAsync(url);
			if (!response.IsSuccessStatusCode)
				return new ServiceResponse<Movie>
				{
					Success = false,
					Message = "HTTP request failed"
				};

			var json = await response.Content.ReadAsStringAsync();
			var result = JsonConvert.DeserializeObject<ServiceResponse<Movie>>(json)
				?? new ServiceResponse<Movie> { Success = false, Message = "Deserialization failed" };

			result.Success = result.Success && result.Data != null;

			return result;

		}

		public async Task<ServiceResponse<Movie>> CreateMovieAsync(Movie newMovie)
		{
			var url = _appSettings.BaseAPIUrl + "/" + _appSettings.MovieEndpoint.CreateMovieEndpoint;
			var response = await _httpClient.PostAsJsonAsync(url, newMovie);
			var result = await response.Content.ReadFromJsonAsync<ServiceResponse<Movie>>();
			return result;
		}

		public async Task<ServiceResponse<bool>> DeleteMovieAsync(int id)
		{
			string uri = _appSettings.BaseAPIUrl + "/" + _appSettings.MovieEndpoint.DeleteMovieEndpoint.Replace("{id}", id.ToString());
			Console.WriteLine("uri!!!!!");
			Console.WriteLine(uri);

			var response = await _httpClient.DeleteAsync(uri);
			var result = await response.Content.ReadFromJsonAsync<ServiceResponse<bool>>();
			return result;
		}

		public async Task<ServiceResponse<Movie>> UpdateMovieAsync(Movie updatedMovie)
		{
			string uri = _appSettings.BaseAPIUrl + "/" + _appSettings.MovieEndpoint.UpdateMovieEndpoint;

			var response = await _httpClient.PutAsJsonAsync(uri, updatedMovie);
			var result = await response.Content.ReadFromJsonAsync<ServiceResponse<Movie>>();
			return result;
		}

		public async Task<ServiceResponse<List<Movie>>> SearchMoviesAsync(string title, int page, int pageSize)
		{
			Console.WriteLine("SearchMoviesAsync");
			try
			{
				string searchUrl = string.IsNullOrWhiteSpace(title) ? "" : $"/{title}";
				string uri = _appSettings.BaseAPIUrl + "/" + _appSettings.MovieEndpoint.SearchMoviesEndpoint + searchUrl + $"/{page}/{pageSize}";
				Console.WriteLine("uri!!!!!");
				Console.WriteLine(uri);
				var response = await _httpClient.GetAsync(uri);
				if (!response.IsSuccessStatusCode)
					return new ServiceResponse<List<Movie>>
					{
						Success = false,
						Message = "HTTP request failed"
					};

				var json = await response.Content.ReadAsStringAsync();
				var result = JsonConvert.DeserializeObject<ServiceResponse<List<Movie>>>(json)
					?? new ServiceResponse<List<Movie>> { Success = false, Message = "Deserialization failed" };

				result.Success = result.Success && result.Data != null;

				return result;

			}
			catch (JsonException)
			{
				return new ServiceResponse<List<Movie>>
				{
					Success = false,
					Message = "Deserialization failed"
				};
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				return new ServiceResponse<List<Movie>>
				{
					Success = false,
					Message = "Cannot deserialize data"
				};
			}

		}

	}
}