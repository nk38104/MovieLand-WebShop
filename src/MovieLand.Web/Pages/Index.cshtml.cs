using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieLand.Web.Interfaces;
using MovieLand.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace MovieLand.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IIndexPageService _indexPageService;
        private readonly IMoviePageService _moviePageService;

        public IndexModel(IIndexPageService indexPageService, IMoviePageService moviePageService)
        {
            _indexPageService = indexPageService ?? throw new ArgumentNullException(nameof(indexPageService));
            _moviePageService = moviePageService ?? throw new ArgumentNullException(nameof(moviePageService));
        }

        public IEnumerable<MovieViewModel> Movies { get; set; } = new List<MovieViewModel>();
        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }


        public async Task OnGetAsync()
        {
            if (!string.IsNullOrEmpty(SearchString))
            {
                Movies = await _indexPageService.GetMoviesByTitle(SearchString);
            }
            else
            {
                Movies = await _indexPageService.GetMovies();
            }
        }

        // Add to Movies folder prob in future(separate index and movies)
        public async Task<IActionResult> OnPostAddToFavoritesAsync(int movieId)
        {
            //if (!User.Identity.IsAuthenticated)
            //    return RedirectToPage("./Account/Login", new { area = "Identity" });

            //await _moviePageService.AddToFavorites(User.Identity.Name, movieId);

            var username = "bg123";

            await _moviePageService.AddToFavorites(username, movieId);
            return RedirectToPage();
        }
    }
}
