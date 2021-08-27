using MovieLand.Application.DTOs;
using MovieLand.Application.Interfaces;
using MovieLand.Application.Mapper;
using MovieLand.Domain.Entities;
using MovieLand.Domain.Interfaces;
using MovieLand.Domain.Interfaces.Repositories;
using System;
using System.Threading.Tasks;


namespace MovieLand.Application.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IAppLogger<OrderService> _logger;

        public OrderService(IOrderRepository orderRepository, IAppLogger<OrderService> logger)
        {
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }


        public async Task<OrderDTO> CheckOutOrder(OrderDTO orderDTO)
        {
            ValidateOrder(orderDTO);

            var mappedOrder = ObjectMapper.Mapper.Map<Order>(orderDTO);

            if (mappedOrder == null)
                throw new ApplicationException("Order DTO could not be mapped.");

            var newOrder = await _orderRepository.AddAsync(mappedOrder);
            _logger.LogInformation("Order entity successfully added");

            var newMappedOrder = ObjectMapper.Mapper.Map<OrderDTO>(newOrder);

            return newMappedOrder;
        }


        public void ValidateOrder(OrderDTO orderDTO)
        {
            if (string.IsNullOrWhiteSpace(orderDTO.Username))
                throw new ApplicationException("Order username must be defined. Can not be empty or white space!!!");

            if (orderDTO.Items.Count == 0)
                throw new ApplicationException("Order must contain at least one item.");
        }
    }
}
