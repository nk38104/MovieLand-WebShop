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


namespace MovieLand.Web.Pages.Admin.Movies
{
    [Authorize(Roles = "Admin,SuperAdmin")]
    public class CreateModel : PageModel
    {
        private readonly IMoviePageService _moviePageService;
        private readonly IIndexPageService _indexPageService;
        [BindProperty]
        public CreateMovieViewModel Movie { get; set; }

        [BindProperty, DisplayName("Directors")]
        public List<int> DirectorIds { get; set; }
        public IEnumerable<SelectListItem> DirectorOptions { get; set; } = new List<SelectListItem>();

        [BindProperty, DisplayName("Genres")]
        public List<int> GenreIds { get; set; }
        public IEnumerable<SelectListItem> GenreOptions { get; set; } = new List<SelectListItem>();

        public CreateModel(IIndexPageService indexPageService, IMoviePageService moviePageService)
        {
            _indexPageService = indexPageService ?? throw new ArgumentNullException(nameof(indexPageService));
            _moviePageService = moviePageService ?? throw new ArgumentNullException(nameof(moviePageService));
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
            
            await _moviePageService.AddMovie(Movie, GenreIds, DirectorIds);

            return RedirectToPage("../../Index");
        }


        private async Task SetDirectorOptions()
        {
            var directors = await _indexPageService.GetDirectors();

            DirectorOptions = directors.Select(d =>
                new SelectListItem
                {
                    Value = d.Id.ToString(),
                    Text = d.Name,
                }).ToList();
        }


        private async Task SetGenreOptions()
        {
            var genres = await _indexPageService.GetGenres();

            GenreOptions = genres.Select(g =>
                new SelectListItem
                {
                    Value = g.Id.ToString(),
                    Text = g.Name,
                }).ToList();
        }
    }
}
