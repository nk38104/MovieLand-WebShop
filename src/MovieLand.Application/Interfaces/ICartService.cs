using MovieLand.Application.DTOs;
using System.Threading.Tasks;


namespace MovieLand.Application.Interfaces
{
    public interface ICartService
    {
        Task<CartDTO> GetCartByUsername(string username);
        Task AddItem(string username, int movieId);
        Task RemoveItem(int cartId, int cartItemId);
        Task ClearCart(string username);
    }
}
