using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MovieLand.Application.DTOs;
using MovieLand.Application.DTOs.Director;
using MovieLand.Application.DTOs.Genre;
using MovieLand.Application.Interfaces;


namespace MovieLand.Web.Pages.Admin.Movies
{
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class EditModel : PageModel
    {
        private readonly IDirectorService _directorService;
        private readonly IGenreService _genreService;
        private readonly IMovieService _movieService;

        [BindProperty]
        public EditMovieDTO Movie { get; set; }
        [BindProperty, DisplayName("Directors")]
        public List<int> DirectorIds { get; set; }
        public IEnumerable<SelectListItem> DirectorOptions { get; set; } = new List<SelectListItem>();
        [BindProperty, DisplayName("Genres")]
        public List<int> GenreIds { get; set; }
        public IEnumerable<SelectListItem> GenreOptions { get; set; } = new List<SelectListItem>();
        public string RequestPagePath { get; set; }

        public EditModel(IDirectorService directorService, IGenreService genreService, IMovieService movieService)
        {
            _directorService = directorService ?? throw new ArgumentNullException(nameof(directorService));
            _genreService = genreService ?? throw new ArgumentNullException(nameof(genreService));
            _movieService = movieService ?? throw new ArgumentNullException(nameof(movieService));
        }


        public async Task<IActionResult> OnGetAsync(int? movieId, string requestPagePath)
        {
            if (movieId == null || movieId < 1)
            {
                return NotFound();
            }

            Movie = await _movieService.GetMovieWithGenresAndDirectorsById((int)movieId);
            DirectorIds = Movie.MovieDirectors.Select(md => md.DirectorId).ToList();
            GenreIds = Movie.MovieGenres.Select(mg => mg.GenreId).ToList();
            RequestPagePath = (requestPagePath == "/") ? "/Index" : requestPagePath;
            
            await SetDirectorOptions(DirectorIds);
            await SetGenreOptions(GenreIds);

            return (Movie == null) ? NotFound() : Page();
        }


        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(string requestPagePath)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Movie.MovieDirectors = DirectorIds.Select(id => new MovieDirectorDTO { MovieId = Movie.Id, DirectorId = id }).ToList();
            Movie.MovieGenres = GenreIds.Select(id => new MovieGenreDTO { MovieId = Movie.Id, GenreId = id }).ToList();

            try
            {
                if (Movie.MovieDirectors != null && Movie.MovieGenres != null)
                {
                    await _movieService.UpdateMovie(Movie);
                }
            }
            catch (Exception)
            {
                if (!MovieExists(Movie.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Redirect(requestPagePath);
        }


        private bool MovieExists(int movieId)
        {
            return  _movieService.GetMovieById(movieId) != null;
        }


        private async Task SetDirectorOptions(List<int> directorIds)
        {
            var directors = await _directorService.GetDirectorList();

            DirectorOptions = directors.Select(d =>
                new SelectListItem
                {
                    Value = d.Id.ToString(),
                    Text = d.Name,
                    Selected = directorIds.Contains(d.Id)
                }).ToList();
        }


        private async Task SetGenreOptions(List<int> genreIds)
        {
            var genres = await _genreService.GetGenreList();

            GenreOptions = genres.Select(g =>
                new SelectListItem
                {
                    Value = g.Id.ToString(),
                    Text = g.Name,
                    Selected = (genreIds.Contains(g.Id)) ? true : false
                }).ToList();
        }
    }
}
