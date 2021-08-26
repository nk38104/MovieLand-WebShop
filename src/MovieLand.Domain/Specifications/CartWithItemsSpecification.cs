using MovieLand.Domain.Entities;
using MovieLand.Domain.Specifications.Base;


namespace MovieLand.Domain.Specifications
{
    public class CartWithItemsSpecification : BaseSpecification<Cart>
    {
        public CartWithItemsSpecification(string username)
            : base(c => c.Username.ToLower() == username.ToLower())
        {
            AddInclude(c => c.Items);
        }


        public CartWithItemsSpecification(int cartId)
            : base(c => c.Id == cartId)
        {
            AddInclude(c => c.Items);
        }
    }
}
