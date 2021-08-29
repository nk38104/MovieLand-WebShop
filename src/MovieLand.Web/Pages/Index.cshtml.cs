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
        public IEnumerable<GenreViewModel> Genres { get; set; } = new List<GenreViewModel>();
        public IEnumerable<DirectorViewModel> Directors { get; set; } = new List<DirectorViewModel>();

        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }


        public async Task OnGetAsync()
        {
            if (!string.IsNullOrEmpty(SearchString))
            {
                Movies = await _moviePageService.GetMoviesByTitle(SearchString);
            }
            else
            {
                Movies = await _moviePageService.GetMovies();
            } 
            
            GetFilterData();
        }


        // Add to Movies folder prob in future(separate index and movies)
        public async Task<IActionResult> OnPostAddToFavoritesAsync(int movieId)
        {
            //if (!User.Identity.IsAuthenticated)
            //    return RedirectToPage("./Account/Login", new { area = "Identity" });

            //await _moviePageService.AddToFavorites(User.Identity.Name, movieId);

            var username = "bg123";

            await _indexPageService.AddToFavorites(username, movieId);
            return RedirectToPage();
        }


        public async Task<IActionResult> OnPostAddToCompareAsync(int movieId)
        {
            //if (!User.Identity.IsAuthenticated)
            //    return RedirectToPage("./Account/Login", new { area = "Identity" });

            //await _moviePageService.AddToFavorites(User.Identity.Name, movieId);

            var username = "mz001";

            await _indexPageService.AddToCompare(username, movieId);
            return RedirectToPage();
        }


        public async Task<IActionResult> OnPostAddToCartAsync(int movieId)
        {
            //if (!User.Identity.IsAuthenticated)
            //    return RedirectToPage("./Account/Login", new { area = "Identity" });

            var username = "bg123";

            await _indexPageService.AddToCart(username, movieId);

            return RedirectToPage();
        }


        public async Task OnGetFilterByDecadesAsync(string decade)
        {
            Movies = await _moviePageService.GetMoviesByDecade(decade);
            GetFilterData();
        }


        public async Task OnPostFilterByDirectorAsync(string director)
        {
            Movies = await _moviePageService.GetMoviesByDirector(director);
            GetFilterData();
        }


        public async Task OnPostFilterByGenreAsync(string genre)
        {
            Movies = await _moviePageService.GetMoviesByGenre(genre);
            GetFilterData();
        }


        public async Task OnPostFilterByPriceAsync(double priceFrom, double priceTo)
        {
            Movies = await _moviePageService.GetMoviesByPrice(priceFrom, priceTo);
            GetFilterData();
        }


        public async Task OnPostFilterByRatingAsync(double rating)
        {
            Movies = await _moviePageService.GetMoviesByRating(rating);
            GetFilterData();
        }


        private async void GetFilterData()
        {
            Genres = await _indexPageService.GetGenres();
            Directors = await _indexPageService.GetDirectors();
        }
    }
}
