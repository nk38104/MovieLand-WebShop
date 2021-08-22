using MovieLand.Domain.Entities;
using MovieLand.Domain.Interfaces.Repositories;
using MovieLand.Infrastructure.Data;
using MovieLand.Infrastructure.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieLand.Infrastructure.Repositories
{
    public class MovieRepository : Repository<Movie>, IMovieRepository
    {
        public MovieRepository(MovieLandContext dbContext)
            : base(dbContext) { }

        public async Task<IEnumerable<Movie>> GetProductListAsync()
        {
            // another option
            //var spec = new MovietWithCategorySpecification();
            //return await GetAsync(spec);

            return await GetAllAsync();
        }
    }
}
