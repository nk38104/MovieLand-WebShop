using MovieLand.Domain.Entities;
using MovieLand.Domain.Interfaces.Repositories;
using MovieLand.Infrastructure.Data;
using MovieLand.Infrastructure.Repositories.Base;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace MovieLand.Infrastructure.Repositories
{
    public class GenreRepository : Repository<Genre>, IGenreRepository
    {
        public GenreRepository(MovieLandContext movieLandContext)
            : base(movieLandContext) { }


        public async Task<IEnumerable<Genre>> GetGenreListAsync()
        {
            return await GetAllAsync();
        }

    }
}
