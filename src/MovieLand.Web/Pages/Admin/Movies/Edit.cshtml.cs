using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using MovieLand.Web.Interfaces;
using MovieLand.Web.ViewModels;
using MovieLand.Web.ViewModels.Directors;
using MovieLand.Web.ViewModels.Genres;


namespace MovieLand.Web.Pages.Admin.Movies
{
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class EditModel : PageModel
    {
        private readonly IMoviePageService _moviePageService;
        private readonly IIndexPageService _indexPageService;
        [BindProperty]
        public EditMovieViewModel Movie { get; set; }

        [BindProperty, DisplayName("Directors")]
        public List<int> DirectorIds { get; set; }
        public IEnumerable<SelectListItem> DirectorOptions { get; set; } = new List<SelectListItem>();

        [BindProperty, DisplayName("Genres")]
        public List<int> GenreIds { get; set; }
        public IEnumerable<SelectListItem> GenreOptions { get; set; } = new List<SelectListItem>();

        public EditModel(IIndexPageService indexPageService, IMoviePageService moviePageService)
        {
            _indexPageService = indexPageService ?? throw new ArgumentNullException(nameof(indexPageService));
            _moviePageService = moviePageService ?? throw new ArgumentNullException(nameof(moviePageService));
        }


        public async Task<IActionResult> OnGetAsync(int? movieId)
        {
            if (movieId == null || movieId < 1)
            {
                return NotFound();
            }

            Movie = await _moviePageService.GetMovieWithGenresAndDirectorsById((int)movieId);
            DirectorIds = Movie.MovieDirectors.Select(md => md.DirectorId).ToList();
            GenreIds = Movie.MovieGenres.Select(mg => mg.GenreId).ToList();
            
            await SetDirectorOptions(DirectorIds);
            await SetGenreOptions(GenreIds);

            return (Movie == null) ? NotFound() : Page();
        }


        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            Movie.MovieDirectors = DirectorIds.Select(id => new MovieDirectorViewModel { MovieId = Movie.Id, DirectorId = id }).ToList();
            Movie.MovieGenres = GenreIds.Select(id => new MovieGenreViewModel { MovieId = Movie.Id, GenreId = id }).ToList();

            try
            {
                if (Movie.MovieDirectors != null && Movie.MovieGenres != null)
                {
                    await _moviePageService.UpdateMovie(Movie);
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

            return RedirectToPage("../../Index");
        }


        private bool MovieExists(int movieId)
        {
            return  _moviePageService.GetMovieById(movieId) != null;
        }


        private async Task SetDirectorOptions(List<int> directorIds)
        {
            var directors = await _indexPageService.GetDirectors();

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
            var genres = await _indexPageService.GetGenres();

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
