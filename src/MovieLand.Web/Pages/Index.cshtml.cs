using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieLand.Application.DTOs;
using MovieLand.Application.Interfaces;
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
        private readonly IMovieService _movieService;

        public IndexModel(IIndexPageService indexPageService, IMoviePageService moviePageService, IMovieService movieService)
        {
            _indexPageService = indexPageService ?? throw new ArgumentNullException(nameof(indexPageService));
            _movieService = movieService ?? throw new ArgumentNullException(nameof(movieService));
        }

        public IEnumerable<MovieDTO> Movies { get; set; } = new List<MovieDTO>();
        public IEnumerable<GenreViewModel> Genres { get; set; } = new List<GenreViewModel>();
        public IEnumerable<DirectorViewModel> Directors { get; set; } = new List<DirectorViewModel>();

        public string genreSelectedValue { get; set; }
        public string directorSelectedValue { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }


        public async Task OnGetAsync()
        {
            // TODO: set searcha as another function
            if (!string.IsNullOrEmpty(SearchString))
            { 
                Movies = await _movieService.GetMovieByTitle(SearchString);
            }
            else
            {
                Movies = await _movieService.GetMovieList();
            }

            await SetFilterData();
        }


        public async Task<IActionResult> OnPostAddToFavoritesAsync(int movieId, string requestPagePath)
        {
            var user = User.Identity;

            if (user.IsAuthenticated)
                await _indexPageService.AddToFavorites(user.Name, movieId);

            return Redirect(requestPagePath);
        }


        public async Task<IActionResult> OnPostAddToCompareAsync(int movieId, string requestPagePath)
        {
            var user = User.Identity;

            if (user.IsAuthenticated)
                await _indexPageService.AddToCompare(user.Name, movieId);

            return Redirect(requestPagePath);
        }


        public async Task<IActionResult> OnPostAddToCartAsync(int movieId, string requestPagePath)
        {
            var user = User.Identity;

            if (user.IsAuthenticated)
                await _indexPageService.AddToCart(user.Name, movieId);

            return Redirect(requestPagePath);
        }


        public async Task OnGetFilterByDecadesAsync(string decade)
        {
            Movies = await _movieService.GetMoviesByDecade(decade);
            await SetFilterData();
        }


        public async Task OnPostFilterByDirectorAsync(string directorSelectedValue)
        {
            Movies = await _movieService.GetMoviesByDirector(directorSelectedValue);
            await SetFilterData();
        }


        public async Task OnPostFilterByGenreAsync(string genreSelectedValue)
        {
            Movies = await _movieService.GetMoviesByGenre(genreSelectedValue);
            await SetFilterData();
        }


        public async Task OnPostFilterByPriceAsync(double priceFrom, double priceTo)
        {
            Movies = await _movieService.GetMoviesByPrice(priceFrom, priceTo);
            await SetFilterData();
        }


        public async Task OnPostFilterByRatingAsync(double rating)
        {
            Movies = await _movieService.GetMoviesByRating(rating);
            await SetFilterData();
        }


        private async Task SetFilterData()
        {
            Directors = await _indexPageService.GetDirectors();
            Genres =  await _indexPageService.GetGenres();
        }
    }
}
