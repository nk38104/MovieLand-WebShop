using MovieLand.Domain.Entities;
using MovieLand.Domain.Interfaces.Repositories.Base;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace MovieLand.Domain.Interfaces.Repositories
{
    public interface IMovieRepository : IRepository<Movie>
    {
        Task<IEnumerable<Movie>> GetMovieListAsync();
        Task<Movie> GetMovieBySlugAsync(string slug);
        Task<IEnumerable<Movie>> GetMovieByTitleAsync(string movieTitle);
    }
}
