using MovieLand.Application.DTOs;
using System.Threading.Tasks;


namespace MovieLand.Application.Interfaces
{
    public interface ICartService
    {
        Task AddItem(string username, int movieId);
        Task RemoveItem(int cartId, int cartItemId);
        Task ClearCart(string username);

        Task<CartDTO> GetCartByUsername(string username);
    }
}
