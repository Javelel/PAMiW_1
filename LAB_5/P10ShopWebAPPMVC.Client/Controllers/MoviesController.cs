using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using P06Shop.Shared.MovieRental;
using P06Shop.Shared.Services.MovieService;

namespace P10ShopWebAPPMVC.Client.Controllers
{
    public class MoviesController : Controller
    {
      	private readonly IMovieService _movieService;

        public MoviesController(IMovieService movieService)
        {
            _movieService = movieService;
          
        }

        // GET: Movies
        public async Task<IActionResult> Index()
        {
              var movies = await _movieService.GetAllMoviesAsync();
			  return movies != null ? View(movies.Data.AsEnumerable()) : Problem("Entity set 'MovieRentalContext.Library'  is null.");
        }

        // GET: Movies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _movieService.GetMovieByIdAsync((int)id);
            if (movie.Data == null)
            {
                return NotFound();
            }

            return View(movie.Data);
        }

        // GET: Movies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Year,Director,Rating")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                await _movieService.CreateMovieAsync(movie);
				return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: Movies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _movieService.GetMovieByIdAsync((int)id);
            if (movie.Data == null)
            {
                return NotFound();
            }
            return View(movie.Data);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Year,Director,Rating")] Movie movie)
        {
            if (id != movie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var productResult = await _movieService.UpdateMovieAsync(movie);
                }
                catch (Exception)
                {
                    return NotFound();
                }
                return RedirectToAction(nameof(Index));
            }
            return View(movie);
        }

        // GET: Movies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = await _movieService.GetMovieByIdAsync((int)id);
            if (movie == null)
            {
                return NotFound();
            }

            return View(movie.Data);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movie = await _movieService.DeleteMovieAsync((int)id);
			if (movie.Success)
				return RedirectToAction(nameof(Index));
			else
				return NotFound();
        }
    }
}
