using MovieLand.Domain.Entities;
using MovieLand.Domain.Interfaces.Repositories;
using MovieLand.Domain.Specifications;
using MovieLand.Infrastructure.Data;
using MovieLand.Infrastructure.Repositories.Base;
using System.Linq;
using System.Threading.Tasks;


namespace MovieLand.Infrastructure.Repositories
{
    public class FavoritesRepository : Repository<Favorite>, IFavoritesRepository
    {
        public FavoritesRepository(MovieLandContext movieLandContext)
            : base(movieLandContext) { }


        public async Task<Favorite> GetByUsernameAsync(string username)
        {
            var spec = new FavoritesWithMoviesSpecification(username);

            return (await GetAsync(spec)).FirstOrDefault();
        }
    }
}
