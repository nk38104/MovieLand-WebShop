using AutoMapper;
using Microsoft.Extensions.Logging;
using MovieLand.Application.DTOs;
using MovieLand.Application.Interfaces;
using MovieLand.Web.Interfaces;
using MovieLand.Web.ViewModels;
using System;
using System.Threading.Tasks;


namespace MovieLand.Web.Services
{
    public class CheckOutPageService : ICheckOutPageService
    {
        private readonly ICartService _cartService;
        private readonly IOrderService _orderService;
        private readonly IMapper _mapper;
        private readonly ILogger<CheckOutPageService> _logger;

        public CheckOutPageService(ICartService cartService, IOrderService orderService, IMapper mapper, ILogger<CheckOutPageService> logger)
        {
            _cartService = cartService ?? throw new ArgumentNullException(nameof(cartService));
            _orderService= orderService ?? throw new ArgumentNullException(nameof(orderService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }


        public async Task<CartViewModel> GetCart(string username)
        {
            var cart = await _cartService.GetCartByUsername(username);
            var mappedCart = _mapper.Map<CartViewModel>(cart);

            return mappedCart;
        }


        public async Task CheckOutOrder(OrderViewModel order, string username)
        {
            var cart = await GetCart(username);

            CopyCartItemsToOrderItems(order, cart);
            SetUsernameToOrder(order, username);

            var mappedOrderDTO = _mapper.Map<OrderDTO>(order);
            await _orderService.CheckOutOrder(mappedOrderDTO);

            await _cartService.ClearCart(username);
        }


        public void CopyCartItemsToOrderItems(OrderViewModel order, CartViewModel cart)
        {
            foreach (var cartItem in cart.Items)
            {
                order.Items.Add(
                    new OrderItemViewModel
                    {
                        Quantity = cartItem.Quantity,
                        UnitPrice = cartItem.UnitPrice,
                        TotalPrice = cartItem.TotalPrice,
                        MovieId = cartItem.MovieId,
                    }
                );
            }
        }


        public void SetUsernameToOrder(OrderViewModel order, string username)
        {
            order.Username = username;
        }
    }
}
