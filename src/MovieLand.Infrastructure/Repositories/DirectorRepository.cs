using MovieLand.Domain.Entities;
using MovieLand.Domain.Interfaces.Repositories;
using MovieLand.Infrastructure.Data;
using MovieLand.Infrastructure.Repositories.Base;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace MovieLand.Infrastructure.Repositories
{
    public class DirectorRepository : Repository<Director>, IDirectorRepository
    {
        public DirectorRepository(MovieLandContext movieLandContext)
            : base(movieLandContext) { }


        public async Task<IEnumerable<Director>> GetDirectorListAsync()
        {
            return await GetAllAsync();
        }
    }
}
