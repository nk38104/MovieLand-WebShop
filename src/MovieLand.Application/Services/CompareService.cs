using MovieLand.Application.DTOs;
using MovieLand.Application.Interfaces;
using MovieLand.Application.Mapper;
using MovieLand.Domain.Entities;
using MovieLand.Domain.Interfaces;
using MovieLand.Domain.Interfaces.Repositories;
using MovieLand.Domain.Specifications;
using System;
using System.Linq;
using System.Threading.Tasks;


namespace MovieLand.Application.Services
{
    public class CompareService : ICompareService
    {
        private readonly ICompareRepository _compareRepository;
        private readonly IMovieRepository _movieRepository;
        private readonly IAppLogger<CompareService> _logger;

        public CompareService(ICompareRepository compareRepository, IMovieRepository movieRepository, IAppLogger<CompareService> logger)
        {
            _compareRepository = compareRepository ?? throw new ArgumentNullException(nameof(compareRepository));
            _movieRepository = movieRepository ?? throw new ArgumentNullException(nameof(movieRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }


        public async Task AddItem(string username, int movieId)
        {
            var compare = await GetExistingOrCreateNewCompare(username);

            compare.AddItem(movieId);

            await _compareRepository.UpdateAsync(compare);
        }


        public async Task RemoveItem(int compareId, int movieId)
        {
            var spec = new CompareWithMoviesSpecification(compareId);
            var compare = (await _compareRepository.GetAsync(spec)).FirstOrDefault();

            compare.RemoveItem(movieId);

            await _compareRepository.UpdateAsync(compare);
        }


        public async Task<CompareDTO> GetCompareByUsername(string username)
        {
            var compare = await GetExistingOrCreateNewCompare(username);
            var compareDTO = ObjectMapper.Mapper.Map<CompareDTO>(compare);

            foreach (var item in compare.MovieCompares)
            {
                var movie = await _movieRepository.GetMovieByIdWithGenresAsync(item.MovieId);
                var movieDTO = ObjectMapper.Mapper.Map<MovieDTO>(movie);
                compareDTO.Movies.Add(movieDTO);
            }

            return compareDTO;
        }


        private async Task<Compare> GetExistingOrCreateNewCompare(string username)
        {
            var compare = await _compareRepository.GetByUsernameAsync(username);

            if (compare != null)
                return compare;

            var newCompare = new Compare
            {
                Username = username
            };

            await _compareRepository.AddAsync(newCompare);

            return newCompare;
        }
    }
}
