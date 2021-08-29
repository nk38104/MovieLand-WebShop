using MovieLand.Web.ViewModels;
using System.Threading.Tasks;


namespace MovieLand.Web.Interfaces
{
    public interface IFavoritesPageService
    {
        Task RemoveMovie(int favoritesId, int movieId);

        Task<FavoritesViewModel> GetFavorites(string username);
    }
}
