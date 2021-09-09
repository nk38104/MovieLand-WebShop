using MovieLand.Web.ViewModels;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace MovieLand.Web.Interfaces
{
    public interface ICheckOutPageService
    {
        Task CheckOutOrder(OrderViewModel order, string username);

        Task<CartViewModel> GetCart(string username);
        Task<OrderViewModel> GetOrderById(int order);

        Task<IEnumerable<OrderViewModel>> GetOrders();
    }
}
