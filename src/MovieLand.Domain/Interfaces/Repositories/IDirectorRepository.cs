using MovieLand.Domain.Entities;
using MovieLand.Domain.Interfaces.Repositories.Base;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace MovieLand.Domain.Interfaces.Repositories
{
    public interface IDirectorRepository : IRepository<Director>
    {
        Task AddDirectorAsync(Director director);
        Task DeleteDirectorAsync(Director director);
        Task UpdateDirectorAsync(Director director);

        Task<Director> GetDirectorByIdAsync(int directorId);

        Task<IEnumerable<Director>> GetDirectorListAsync();
    }
}
