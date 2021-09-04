using MovieLand.Domain.Entities;
using MovieLand.Domain.Interfaces.Repositories.Base;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace MovieLand.Domain.Interfaces.Repositories
{
    public interface IMovieRepository : IRepository<Movie>
    {
        Task<Movie> GetMovieByIdWithGenresAsync(int movieId);
        Task<Movie> GetMovieBySlugAsync(string slug);

        Task<IEnumerable<Movie>> GetMovieByTitleAsync(string movieTitle);
        
        Task<IEnumerable<Movie>> GetMovieListAsync();
        Task<IEnumerable<Movie>> GetMoviesByDecadeAsync(string decade);
        Task<IEnumerable<Movie>> GetMoviesByDirectorAsync(string director);
        Task<IEnumerable<Movie>> GetMoviesByGenreAsync(string genre);
        Task<IEnumerable<Movie>> GetMoviesByPriceAsync(double priceFrom, double priceTo);
        Task<IEnumerable<Movie>> GetMoviesByRatingAsync(double rating);
    }
}
