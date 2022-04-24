using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieLand.Application.DTOs;
using MovieLand.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace MovieLand.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly IMovieService _movieService;
        private readonly ICartService _cartService;
        private readonly ICompareService _compareService;
        private readonly IDirectorService _directorService;
        private readonly IFavoritesService _favoritesService;
        private readonly IGenreService _genreService;

        public IndexModel(IMovieService movieService, IGenreService genreService, IDirectorService directorService,
            IFavoritesService favoritesService, ICompareService compareService, ICartService cartService)
        {
            _cartService = cartService ?? throw new ArgumentNullException(nameof(cartService));
            _compareService = compareService ?? throw new ArgumentNullException(nameof(compareService));
            _directorService = directorService ?? throw new ArgumentNullException(nameof(directorService));
            _favoritesService = favoritesService ?? throw new ArgumentNullException(nameof(favoritesService));
            _genreService = genreService ?? throw new ArgumentNullException(nameof(genreService));
            _movieService = movieService ?? throw new ArgumentNullException(nameof(movieService));
        }

        public IEnumerable<MovieDTO> Movies { get; set; } = new List<MovieDTO>();
        public IEnumerable<GenreDTO> Genres { get; set; } = new List<GenreDTO>();
        public IEnumerable<DirectorDTO> Directors { get; set; } = new List<DirectorDTO>();

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
                await _favoritesService.AddItem(user.Name, movieId);

            return Redirect(requestPagePath);
        }


        public async Task<IActionResult> OnPostAddToCompareAsync(int movieId, string requestPagePath)
        {
            var user = User.Identity;

            if (user.IsAuthenticated)
            {
                await _compareService.AddItem(user.Name, movieId);
            }

            return Redirect(requestPagePath);
        }


        public async Task<IActionResult> OnPostAddToCartAsync(int movieId, string requestPagePath)
        {
            var user = User.Identity;

            if (user.IsAuthenticated)
                await _cartService.AddItem(user.Name, movieId);

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
            Directors = await _directorService.GetDirectorList();
            Genres =  await _genreService.GetGenreList();
        }
    }
}
