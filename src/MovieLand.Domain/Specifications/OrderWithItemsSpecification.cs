using MovieLand.Domain.Entities;
using MovieLand.Domain.Specifications.Base;


namespace MovieLand.Domain.Specifications
{
    public class OrderWithItemsSpecification : BaseSpecification<Order>
    {
        public OrderWithItemsSpecification(string username)
           : base(o => o.Username.ToLower() == username.ToLower())
        {
            AddInclude(o => o.Items);
        }


        public OrderWithItemsSpecification(int orderId)
            : base(o => o.Id == orderId)
        {
            AddInclude(o => o.Items);
        }
    }
}
