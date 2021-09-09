using AutoMapper;
using MovieLand.Application.DTOs;
using MovieLand.Application.Interfaces;
using MovieLand.Application.Mapper;
using MovieLand.Domain.Entities;
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
        private readonly IMapper _mapper;

        public GenreService(IGenreRepository genreRepository, IAppLogger<GenreService> logger, IMapper mapper)
        {
            _genreRepository = genreRepository ?? throw new ArgumentNullException(nameof(genreRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }


        public async Task AddGenre(GenreDTO genre)
        {
            var genreMapped = ObjectMapper.Mapper.Map<Genre>(genre);

            await _genreRepository.AddGenreAsync(genreMapped);
        }


        public async Task DeleteGenre(int genreId)
        {
            var genre = await _genreRepository.GetByIdAsync(genreId);

            await _genreRepository.DeleteGenreAsync(genre);
        }


        public async Task UpdateGenre(GenreDTO genre)
        {
            var genreMapped = ObjectMapper.Mapper.Map<Genre>(genre);

            await _genreRepository.UpdateGenreAsync(genreMapped);
        }


        public async Task<GenreDTO> GetGenreById(int genreId)
        {
            var genre = await _genreRepository.GetGenreByIdAsync(genreId);
            var genreMapped = ObjectMapper.Mapper.Map<GenreDTO>(genre);

            return genreMapped;
        }


        public async Task<IEnumerable<GenreDTO>> GetGenreList()
        {
            var genres = await _genreRepository.GetGenreListAsync();
            var genresMapped = ObjectMapper.Mapper.Map<IEnumerable<GenreDTO>>(genres);

            return genresMapped;
        }
    }
}
