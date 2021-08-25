using MovieLand.Web.ViewModels;
using System.Threading.Tasks;

namespace MovieLand.Web.Interfaces
{
    public interface IFavoritesPageService
    {
        Task<FavoritesViewModel> GetFavorites(string username);
        Task RemoveMovie(int favoritesId, int movieId);
    }
}
