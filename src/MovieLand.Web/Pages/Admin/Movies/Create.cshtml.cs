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
using MovieLand.Application.Interfaces;


namespace MovieLand.Web.Pages.Admin.Movies
{
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class CreateModel : PageModel
    {
        private readonly IDirectorService _directorService;
        private readonly IGenreService _genreService;
        private readonly IMovieService _movieService;

        [BindProperty]
        public CreateMovieDTO Movie { get; set; }

        [BindProperty, DisplayName("Directors")]
        public List<int> DirectorIds { get; set; }
        public IEnumerable<SelectListItem> DirectorOptions { get; set; } = new List<SelectListItem>();

        [BindProperty, DisplayName("Genres")]
        public List<int> GenreIds { get; set; }
        public IEnumerable<SelectListItem> GenreOptions { get; set; } = new List<SelectListItem>();

        public CreateModel(IDirectorService directorService, IGenreService genreService, IMovieService movieService)
        {
            _directorService = directorService ?? throw new ArgumentNullException(nameof(directorService));
            _genreService = genreService ?? throw new ArgumentNullException(nameof(genreService));
            _movieService = movieService ?? throw new ArgumentNullException(nameof(movieService));
        }


        public async Task<IActionResult> OnGet()
        {
            await SetDirectorOptions();
            await SetGenreOptions();

            return Page();
        }


        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            
            await _movieService.AddMovie(Movie, GenreIds, DirectorIds);

            return RedirectToPage("../../Index");
        }


        private async Task SetDirectorOptions()
        {
            var directors = await _directorService.GetDirectorList();

            DirectorOptions = directors.Select(d =>
                new SelectListItem
                {
                    Value = d.Id.ToString(),
                    Text = d.Name,
                }).ToList();
        }


        private async Task SetGenreOptions()
        {
            var genres = await _genreService.GetGenreList();

            GenreOptions = genres.Select(g =>
                new SelectListItem
                {
                    Value = g.Id.ToString(),
                    Text = g.Name,
                }).ToList();
        }
    }
}
