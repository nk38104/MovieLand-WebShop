using MovieLand.Domain.Entities;
using MovieLand.Domain.Interfaces.Repositories;
using MovieLand.Domain.Specifications;
using MovieLand.Infrastructure.Data;
using MovieLand.Infrastructure.Repositories.Base;
using System.Linq;
using System.Threading.Tasks;


namespace MovieLand.Infrastructure.Repositories
{
    public class CompareRepository : Repository<Compare>, ICompareRepository
    {
        public CompareRepository(MovieLandContext movieLandContext)
            : base(movieLandContext) { }


        public async Task<Compare> GetByUsernameAsync(string username)
        {
            var spec = new CompareWithMoviesSpecification(username);

            return (await GetAsync(spec)).FirstOrDefault();
        }
    }
}
