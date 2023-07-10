using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using eBoxOffice.Domain.Domain_models;
using eBoxOffice.Repository.Interface;
using eBoxOffice.Domain.Enumeration;

namespace homework.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IRepository<Movie> _moviesRepository;

        public MoviesController(IRepository<Movie> moviesRepository)
        {
            _moviesRepository = moviesRepository;
        }

        // GET: Movies
        public async Task<IActionResult> Index()
        {
            return View(_moviesRepository.GetAll().ToList());
        }

        // GET: Movies/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = _moviesRepository.Get(id ?? 0);

            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // GET: Movies/Create
        public IActionResult Create()
        {
            ViewData["Category"] = new SelectList(Enum.GetValues(typeof(Category)).Cast<Category>().ToList());

            return View();
        }

        // POST: Movies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Movie movie)
        {
            if (ModelState.IsValid)
            {
                _moviesRepository.Insert(movie);
                return RedirectToAction(nameof(Index));
            }

            ViewData["Category"] = new SelectList(Enum.GetValues(typeof(Category)).Cast<Category>().ToList());
            return View(movie);
        }

        // GET: Movies/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = _moviesRepository.Get(id ?? 0);
            if (movie == null)
            {
                return NotFound();
            }

            ViewData["Category"] = new SelectList(Enum.GetValues(typeof(Category)).Cast<Category>().ToList());
            return View(movie);
        }

        // POST: Movies/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("MovieId,MovieName,MovieImage")] Movie movie)
        {
            if (id != movie.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _moviesRepository.Update(movie);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MovieExists(movie.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }

            ViewData["Category"] = new SelectList(Enum.GetValues(typeof(Category)).Cast<Category>().ToList());
            return View(movie);
        }

        // GET: Movies/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var movie = _moviesRepository.Get(id ?? 0);

            if (movie == null)
            {
                return NotFound();
            }

            return View(movie);
        }

        // POST: Movies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var movieToRemove = _moviesRepository.Get(id);
            _moviesRepository.Delete(movieToRemove);

            return RedirectToAction(nameof(Index));
        }

        private bool MovieExists(int id)
        {
            return _moviesRepository.Get(id) != null;
        }
    }
}
