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
    public class GenreService : IGenreService
    {
        private readonly IGenreRepository _genreRepository;
        private readonly IAppLogger<GenreService> _logger;

        public GenreService(IGenreRepository genreRepository, IAppLogger<GenreService> logger)
        {
            _genreRepository = genreRepository ?? throw new ArgumentNullException(nameof(genreRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }


        public async Task<IEnumerable<GenreDTO>> GetGenreList()
        {
            var genres = await _genreRepository.GetGenreListAsync();
            var genresMapped = ObjectMapper.Mapper.Map<IEnumerable<GenreDTO>>(genres);

            return genresMapped;
        }
    }
}
