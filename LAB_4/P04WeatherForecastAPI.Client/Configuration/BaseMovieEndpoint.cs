using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P04WeatherForecastAPI.Client.Configuration
{
	internal class BaseMovieEndpoint
	{
		public string Base_url { get; set; }
		public string GetAllMoviesEndpoint { get; set; }
		public string GetMovieByIdEndpoint { get; set; }
        public string CreateMovieEndpoint { get; set; }
        public string DeleteMovieEndpoint { get; set; }
		public string UpdateMovieEndpoint { get; set; }
	}
}
