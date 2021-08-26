using AutoMapper;
using Microsoft.Extensions.Logging;
using MovieLand.Application.Interfaces;
using MovieLand.Web.Interfaces;
using MovieLand.Web.ViewModels;
using System;
using System.Threading.Tasks;


namespace MovieLand.Web.Services
{
    public class CartPageService : ICartPageService
    {
        private readonly ICartService _cartService;
        private readonly IMapper _mapper;
        private readonly ILogger<CartPageService> _logger;

        public CartPageService(ICartService cartService, IMapper mapper, ILogger<CartPageService> logger)
        {
            _cartService = cartService ?? throw new ArgumentNullException(nameof(cartService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }


        public async Task<CartViewModel> GetCart(string username)
        {
            var cart = await _cartService.GetCartByUsername(username);
            var mappedCart = _mapper.Map<CartViewModel>(cart);

            return mappedCart;
        }


        public async Task AddItem(string username, int movieId)
        {
            await _cartService.AddItem(username, movieId);
        }


        public async Task RemoveItem(int cartId, int cartItemId)
        {
            await _cartService.RemoveItem(cartId, cartItemId);
        }
    }
}
