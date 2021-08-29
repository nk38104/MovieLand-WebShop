using AutoMapper;
using MovieLand.Application.Interfaces;
using MovieLand.Web.Interfaces;
using MovieLand.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace MovieLand.Web.Services
{
    public class IndexPageService : IIndexPageService
    {
        private readonly IMovieService _movieService;
        private readonly IGenreService _genreService;
        private readonly IDirectorService _directorService;
        private readonly IFavoritesService _favoritesService;
        private readonly ICompareService _compareService;
        private readonly ICartService _cartService;
        private readonly IMapper _mapper;

        public IndexPageService(IMovieService movieService, IGenreService genreService, IDirectorService directorService,
            IFavoritesService favoritesService, ICompareService compareService, ICartService cartService, IMapper mapper)
        {
            _cartService = cartService ?? throw new ArgumentNullException(nameof(cartService));
            _compareService = compareService ?? throw new ArgumentNullException(nameof(compareService));
            _directorService = directorService ?? throw new ArgumentNullException(nameof(directorService));
            _favoritesService = favoritesService ?? throw new ArgumentNullException(nameof(favoritesService));
            _genreService = genreService ?? throw new ArgumentNullException(nameof(genreService));
            _movieService = movieService ?? throw new ArgumentNullException(nameof(movieService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }


        public async Task AddToCart(string username, int movieId)
        {
            await _cartService.AddItem(username, movieId);
        }


        public async Task AddToCompare(string username, int movieId)
        {
            await _compareService.AddItem(username, movieId);
        }


        public async Task AddToFavorites(string username, int movieId)
        {
            await _favoritesService.AddItem(username, movieId);
        }


        public async Task<IEnumerable<DirectorViewModel>> GetDirectors()
        {
            var directors = await _directorService.GetDirectorList();
            var directorsMapped = _mapper.Map<IEnumerable<DirectorViewModel>>(directors);

            return directorsMapped;
        }


        public async Task<IEnumerable<GenreViewModel>> GetGenres()
        {
            var genres = await _genreService.GetGenreList();
            var genresMapped = _mapper.Map<IEnumerable<GenreViewModel>>(genres);

            return genresMapped;
        }


        public async Task<IEnumerable<MovieViewModel>> GetMovies()
        {
            var movies = await _movieService.GetMovieList();
            var moviesMapped = _mapper.Map<IEnumerable<MovieViewModel>>(movies);

            return moviesMapped;
        }
    }
}
