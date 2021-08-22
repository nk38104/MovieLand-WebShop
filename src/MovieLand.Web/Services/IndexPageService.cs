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
        private readonly IMapper _mapper;

        public IndexPageService(IMovieService movieService, IMapper mapper)
        {
            _movieService = movieService ?? throw new ArgumentNullException(nameof(movieService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }

        public async Task<IEnumerable<MovieViewModel>> GetMovies()
        {
            var movies = await _movieService.GetMovieList();
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
