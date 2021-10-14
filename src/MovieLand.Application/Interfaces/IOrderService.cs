using MovieLand.Application.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace MovieLand.Application.Interfaces
{
    public interface IOrderService
    {
        Task DeleteOrder(int orderId);
        Task<OrderDTO> CheckOutOrder(OrderDTO orderDTO);
        Task<OrderDTO> GetOrderById(int orderId);
        
        Task<IEnumerable<OrderDTO>> GetOrders();
    }
}
