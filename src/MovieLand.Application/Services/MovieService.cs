using MovieLand.Application.DTOs;
using MovieLand.Application.Interfaces;
using MovieLand.Application.Mapper;
using MovieLand.Domain.Interfaces;
using MovieLand.Domain.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieLand.Application.Services
{
    public class MovieService : IMovieService
    {
        private readonly IMovieRepository _movieRepository;
        private readonly IAppLogger<MovieService> _logger;

        public MovieService(IMovieRepository movieRepository, IAppLogger<MovieService> logger)
        {
            _movieRepository = movieRepository ?? throw new ArgumentNullException(nameof(movieRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<IEnumerable<MovieDTO>> GetMovieList()
        {
            var movies = await _movieRepository.GetProductListAsync();
            var moviesMapped = ObjectMapper.Mapper.Map<IEnumerable<MovieDTO>>(movies);

            return moviesMapped;
        }
    }
}
