using Microsoft.Extensions.Logging;
using MovieLand.Application.DTOs;
using MovieLand.Application.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace MovieLand.Application.Services
{
    public class CheckoutService : ICheckoutService
    {
        private readonly ICartService _cartService;
        private readonly IOrderService _orderService;
        private readonly ILogger<CheckoutService> _logger;

        public CheckoutService(ICartService cartService, IOrderService orderService, ILogger<CheckoutService> logger)
        {
            _cartService = cartService ?? throw new ArgumentNullException(nameof(cartService));
            _orderService = orderService ?? throw new ArgumentNullException(nameof(orderService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }


        public async Task CheckOutOrder(OrderDTO order, string username)
        {
            var cart = await GetCart(username);

            CopyCartItemsToOrderItems(order, cart);
            order.Username = username;

            await _orderService.CheckOutOrder(order);
            await _cartService.ClearCart(username);
        }


        public async Task DeleteOrder(int orderId)
        {
            await _orderService.DeleteOrder(orderId);
        }


        public async Task UpdateOrder(OrderDTO order)
        {
            await _orderService.UpdateOrder(order);
        }


        public async Task<CartDTO> GetCart(string username)
        {
            var cart = await _cartService.GetCartByUsername(username);

            return cart;
        }


        public async Task<OrderDTO> GetOrderById(int orderId)
        {
            var order = await _orderService.GetOrderById(orderId);

            return order;
        }


        public async Task<IEnumerable<OrderDTO>> GetOrders()
        {
            var orders = await _orderService.GetOrders();

            return orders;
        }


        public void CopyCartItemsToOrderItems(OrderDTO order, CartDTO cart)
        {
            foreach (var cartItem in cart.Items)
            {
                order.Items.Add(
                    new OrderItemDTO
                    {
                        Quantity = cartItem.Quantity,
                        UnitPrice = cartItem.UnitPrice,
                        TotalPrice = cartItem.TotalPrice,
                        MovieId = cartItem.MovieId,
                    }
                );
            }

            order.GrandTotal = cart.GrandTotal;
        }
    }
}
