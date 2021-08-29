using MovieLand.Web.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace MovieLand.Web.Interfaces
{
    public interface IIndexPageService
    {
        Task AddToCart(string username, int movieId);
        Task AddToCompare(string username, int movieId);
        Task AddToFavorites(string username, int movieId);
        
        Task<IEnumerable<DirectorViewModel>> GetDirectors();
        Task<IEnumerable<GenreViewModel>> GetGenres();
        Task<IEnumerable<MovieViewModel>> GetMovies();
    }
}
