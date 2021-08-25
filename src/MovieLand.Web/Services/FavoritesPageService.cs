using AutoMapper;
using Microsoft.Extensions.Logging;
using MovieLand.Application.Interfaces;
using MovieLand.Web.Interfaces;
using MovieLand.Web.ViewModels;
using System;
using System.Threading.Tasks;


namespace MovieLand.Web.Services
{
    public class FavoritesPageService : IFavoritesPageService
    {
        private readonly IFavoritesService _favoritesService;
        private readonly IMapper _mapper;
        private readonly ILogger<FavoritesPageService> _logger;

        public FavoritesPageService(IFavoritesService favoritesService, IMapper mapper, ILogger<FavoritesPageService> logger)
        {
            _favoritesService = favoritesService ?? throw new ArgumentNullException(nameof(favoritesService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task<FavoritesViewModel> GetFavorites(string username)
        {
            var favorites = await _favoritesService.GetFavoritesByUsername(username);
            var mapped = _mapper.Map<FavoritesViewModel>(favorites);
            
            return mapped;
        }

        public async Task RemoveMovie(int favoritesId, int movieId)
        {
            await _favoritesService.RemoveItem(favoritesId, movieId);
        }
    }
}
