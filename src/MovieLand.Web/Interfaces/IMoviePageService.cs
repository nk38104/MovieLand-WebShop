using MovieLand.Web.ViewModels;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace MovieLand.Web.Interfaces
{
    public interface IMoviePageService
    {
        Task<MovieViewModel> GetMovieById(int movieId);
        Task<MovieViewModel> GetMovieBySlug(string slug);

        Task<IEnumerable<MovieViewModel>> GetMovies();
        Task<IEnumerable<MovieViewModel>> GetMovies(string movieTitle);
        Task<IEnumerable<MovieViewModel>> GetMoviesByDecade(string decade);
        Task<IEnumerable<MovieViewModel>> GetMoviesByDirector(string director);
        Task<IEnumerable<MovieViewModel>> GetMoviesByGenre(string genre);
        Task<IEnumerable<MovieViewModel>> GetMoviesByPrice(double priceFrom, double priceTo);
        Task<IEnumerable<MovieViewModel>> GetMoviesByRating(double rating);
        Task<IEnumerable<MovieViewModel>> GetMoviesByTitle(string moviteTitle);
    }
}
