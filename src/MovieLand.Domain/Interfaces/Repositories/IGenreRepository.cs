using MovieLand.Domain.Entities;
using MovieLand.Domain.Interfaces.Repositories.Base;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace MovieLand.Domain.Interfaces.Repositories
{
    public interface IGenreRepository : IRepository<Genre>
    {
        Task AddGenreAsync(Genre genre);
        Task DeleteGenreAsync(Genre genre);
        Task UpdateGenreAsync(Genre genre);

        Task<Genre> GetGenreByIdAsync(int genreId);

        Task<IEnumerable<Genre>> GetGenreListAsync();
    }
}
