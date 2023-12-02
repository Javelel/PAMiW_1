
﻿using Bogus;
using P06Shop.Shared.MovieRental;

namespace P07Shop.DataSeeder
{
    public class MovieSeeder
    {
        public static List<Movie> GenerateMovieData()
        {
			int movieId = 1;
            var faker = new Faker<Movie>()
				.UseSeed(123456)
                .RuleFor(m => m.Id, f => movieId++)
                .RuleFor(m => m.Title, f => f.Random.Words(f.Random.Number(1, 3)))
                .RuleFor(m => m.Year, f => f.Date.Past(20).Year)
                .RuleFor(m => m.Director, f => f.Name.FullName())
                .RuleFor(m => m.Rating, f => f.Random.Number(1, 10));

            return faker.Generate(23).ToList();
        }
    }
}
