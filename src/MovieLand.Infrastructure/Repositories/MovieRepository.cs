using MovieLand.Domain.Entities;
using MovieLand.Domain.Interfaces.Repositories;
using MovieLand.Domain.Specifications;
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


        public async Task<IEnumerable<Movie>> GetMovieListAsync()
        {
            return await GetAllAsync();
        }


        public async Task<Movie> GetMovieBySlugAsync(string slug)
        {
            var spec = new MovieSlugSpecification(slug);
            return (await GetAsync(spec)).FirstOrDefault();
        }


        public async Task<IEnumerable<Movie>> GetMovieByTitleAsync(string moviteTitle)
        {
            return await GetAsync(m => m.Title.ToLower().Contains(moviteTitle.ToLower()));
        }
    }
}
