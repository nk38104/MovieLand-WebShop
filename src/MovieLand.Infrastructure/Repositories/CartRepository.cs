using MovieLand.Domain.Entities;
using MovieLand.Domain.Interfaces.Repositories;
using MovieLand.Domain.Specifications;
using MovieLand.Infrastructure.Data;
using MovieLand.Infrastructure.Repositories.Base;
using System.Linq;
using System.Threading.Tasks;


namespace MovieLand.Infrastructure.Repositories
{
    public class CartRepository : Repository<Cart>, IRepository
    {
        public CartRepository(MovieLandContext movieLandContext)
            : base(movieLandContext) { }


        public async Task<Cart> GetByUsernameAsync(string username)
        {
            var spec = new CartWithItemsSpecification(username);

            return (await GetAsync(spec)).FirstOrDefault();
        }
    }
}
