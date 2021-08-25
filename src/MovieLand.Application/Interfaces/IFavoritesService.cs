using MovieLand.Application.DTOs;
using System.Threading.Tasks;


namespace MovieLand.Application.Interfaces
{
    public interface IFavoritesService
    {
        Task<FavoritesDTO> GetFavoritesByUsername(string username);
        Task AddItem(string username, int movieId);
        Task RemoveItem(int favoritesId, int movieId);
    }
}
