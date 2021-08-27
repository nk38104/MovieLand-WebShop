using MovieLand.Application.DTOs;
using System.Threading.Tasks;


namespace MovieLand.Application.Interfaces
{
    public interface IOrderService
    {
        Task<OrderDTO> CheckOutOrder(OrderDTO orderDTO);
    }
}
