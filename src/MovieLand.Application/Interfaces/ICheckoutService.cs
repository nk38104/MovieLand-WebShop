using MovieLand.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace MovieLand.Application.Interfaces
{
    public interface ICheckoutService
    {
        Task CheckOutOrder(OrderDTO order, string username);
        Task DeleteOrder(int orderId);
        Task UpdateOrder(OrderDTO order);

        Task<CartDTO> GetCart(string username);
        Task<OrderDTO> GetOrderById(int order);

        Task<IEnumerable<OrderDTO>> GetOrders();
    }
}
