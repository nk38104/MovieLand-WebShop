using AutoMapper;
using Microsoft.Extensions.Logging;
using MovieLand.Application.Interfaces;
using MovieLand.Web.Interfaces;
using MovieLand.Web.ViewModels;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace MovieLand.Web.Services
{
    public class MoviePageService : IMoviePageService
    {
        private readonly IMovieService _movieService;
        private readonly IMapper _mapper;
        private readonly ILogger<MoviePageService> _logger;

        public MoviePageService(IMovieService movieService, IMapper mapper, ILogger<MoviePageService> logger)
        {
            _movieService = movieService ?? throw new ArgumentNullException(nameof(movieService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }


        public async Task<MovieViewModel> GetMovieById(int movieId)
        {
            var movie = await _movieService.GetMovieById(movieId);
            var movieMapped = _mapper.Map<MovieViewModel>(movie);

            return movieMapped;
        }


        public async Task<MovieViewModel> GetMovieBySlug(string slug)
        {
            var movie = await _movieService.GetMovieBySlug(slug);
            var movieMapped = _mapper.Map<MovieViewModel>(movie);

            return movieMapped;
        }


        public async Task<IEnumerable<MovieViewModel>> GetMovies()
        {
            var movies = await _movieService.GetMovieList();
            var moviesMapped = _mapper.Map<IEnumerable<MovieViewModel>>(movies);

            return moviesMapped;
        }


        public async Task<IEnumerable<MovieViewModel>> GetMovies(string movieTitle)
        {
            if (string.IsNullOrWhiteSpace(movieTitle))
            {
                var movies = await _movieService.GetMovieList();
                var moviesMapped = _mapper.Map<IEnumerable<MovieViewModel>>(movies);

                return moviesMapped;
            }

            var moviesByTitle = await _movieService.GetMovieByTitle(movieTitle);
            var mappedByTitle = _mapper.Map<IEnumerable<MovieViewModel>>(moviesByTitle);

            return mappedByTitle;
        }


        public async Task<IEnumerable<MovieViewModel>> GetMoviesByDecade(string decade)
        {
            var movies = await _movieService.GetMoviesByDecade(decade);
            var moviesMapped = _mapper.Map<IEnumerable<MovieViewModel>>(movies);

            return moviesMapped;
        }


        public async Task<IEnumerable<MovieViewModel>> GetMoviesByDirector(string director)
        {
            var movies = await _movieService.GetMoviesByDirector(director);
            var moviesMapped = _mapper.Map<IEnumerable<MovieViewModel>>(movies);

            return moviesMapped;
        }


        public async Task<IEnumerable<MovieViewModel>> GetMoviesByGenre(string genre)
        {
            var movies = await _movieService.GetMoviesByGenre(genre);
            var moviesMapped = _mapper.Map<IEnumerable<MovieViewModel>>(movies);

            return moviesMapped;
        }


        public async Task<IEnumerable<MovieViewModel>> GetMoviesByPrice(double priceFrom, double priceTo)
        {
            var movies = await _movieService.GetMoviesByPrice(priceFrom, priceTo);
            var moviesMapped = _mapper.Map<IEnumerable<MovieViewModel>>(movies);

            return moviesMapped;
        }


        public async Task<IEnumerable<MovieViewModel>> GetMoviesByRating(double rating)
        {
            var movies = await _movieService.GetMoviesByRating(rating);
            var moviesMapped = _mapper.Map<IEnumerable<MovieViewModel>>(movies);

            return moviesMapped;
        }


        public async Task<IEnumerable<MovieViewModel>> GetMoviesByTitle(string movieTitle)
        {
            var movies = await _movieService.GetMovieByTitle(movieTitle);
            var moviesMapped = _mapper.Map<IEnumerable<MovieViewModel>>(movies);

            return moviesMapped;
        }
    }
}
