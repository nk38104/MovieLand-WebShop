using MovieLand.Web.ViewModels;
using System.Threading.Tasks;


namespace MovieLand.Web.Interfaces
{
    public interface ICheckOutPageService
    {
        Task<CartViewModel> GetCart(string username);
        Task CheckOutOrder(OrderViewModel order, string username);
    }
}
