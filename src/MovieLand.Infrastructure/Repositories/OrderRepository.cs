using MovieLand.Domain.Entities;
using MovieLand.Domain.Interfaces.Repositories;
using MovieLand.Domain.Specifications;
using MovieLand.Infrastructure.Data;
using MovieLand.Infrastructure.Repositories.Base;
using System.Linq;
using System.Threading.Tasks;


namespace MovieLand.Infrastructure.Repositories
{
    public class OrderRepository : Repository<Order>, IOrderRepository
    {
        public OrderRepository(MovieLandContext movieLandContext)
            : base(movieLandContext) { }


        public async Task<Order> GetOrderByUsernameAsync(string username)
        {
            var spec = new OrderWithItemsSpecification(username);

            return (await GetAsync(spec)).FirstOrDefault();
        }
    }
}
