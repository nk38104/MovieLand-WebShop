using MovieLand.Web.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace MovieLand.Web.Interfaces
{
    public interface IIndexPageService
    {
        Task<IEnumerable<MovieViewModel>> GetMovies();
        Task<IEnumerable<MovieViewModel>> GetMoviesByTitle(string moviteTitle);
    }
}
