using MovieLand.Web.ViewModels;
using System.Threading.Tasks;


namespace MovieLand.Web.Interfaces
{
    public interface ICartPageService
    {
        Task<CartViewModel> GetCart(string username);
        Task AddItem(string username, int movieId);
        Task RemoveItem(int cartId, int cartItemId);
    }
}
