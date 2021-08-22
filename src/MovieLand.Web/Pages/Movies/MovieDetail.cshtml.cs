using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieLand.Web.Interfaces;
using MovieLand.Web.ViewModels;

namespace MovieLand.Web.Pages.Movies
{
    public class MovieDetailModel : PageModel
    {
        private readonly IMoviePageService _moviePageService;

        public MovieDetailModel(IMoviePageService moviePageService)
        {
            _moviePageService = moviePageService ?? throw new ArgumentNullException(nameof(moviePageService));
        }

        public MovieViewModel Movie { get; set; } = new MovieViewModel();

        public async Task OnGetAsync(string slug)
        {
            Movie = await _moviePageService.GetMovieBySlug(slug);
            TempData["Slug"] = slug;
        }
    }
}
