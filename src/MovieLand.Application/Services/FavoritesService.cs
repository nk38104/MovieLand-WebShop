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
    public class FavoritesService : IFavoritesService
    {
        private readonly IFavoritesRepository _favoritesRepository;
        private readonly IMovieRepository _movieRepository;
        private readonly IAppLogger<FavoritesService> _logger;

        public FavoritesService(IFavoritesRepository favoritesRepository, IMovieRepository movieRepository, IAppLogger<FavoritesService> logger)
        {
            _favoritesRepository = favoritesRepository ?? throw new ArgumentNullException(nameof(favoritesRepository));
            _movieRepository = movieRepository ?? throw new ArgumentNullException(nameof(movieRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }


        public async Task AddItem(string username, int movieId)
        {
            var favorites = await GetExistingOrCreateNewFavorites(username);
            favorites.AddItem(movieId);

            await _favoritesRepository.UpdateAsync(favorites);
        }


        public async Task RemoveItem(int favoritesId, int movieId)
        {
            var spec = new FavoritesWithMoviesSpecification(favoritesId);
            var favorites = (await _favoritesRepository.GetAsync(spec)).FirstOrDefault();

            favorites.RemoveItem(movieId);

            await _favoritesRepository.UpdateAsync(favorites);
        }


        public async Task<FavoritesDTO> GetFavoritesByUsername(string username)
        {
            var favorites = await GetExistingOrCreateNewFavorites(username);
            var favoritesDTO = ObjectMapper.Mapper.Map<FavoritesDTO>(favorites);

            foreach (var item in favorites.MovieFavorites)
            {
                var movie = await _movieRepository.GetMovieByIdWithGenresAsync(item.MovieId);
                var movieDTO = ObjectMapper.Mapper.Map<MovieDTO>(movie);
                favoritesDTO.Movies.Add(movieDTO);
            }

            return favoritesDTO;
        }


        private async Task<Favorite> GetExistingOrCreateNewFavorites(string username)
        {
            var favorite = await _favoritesRepository.GetByUsernameAsync(username);
            
            if (favorite != null)
                return favorite;

            // Create new in case of first attempt
            var newFavorite = new Favorite
            {
                Username = username
            };

            await _favoritesRepository.AddAsync(newFavorite);

            return newFavorite;
        }
    }
}
