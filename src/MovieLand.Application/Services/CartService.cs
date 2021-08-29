using MovieLand.Application.DTOs;
using MovieLand.Application.Interfaces;
using MovieLand.Application.Mapper;
using MovieLand.Domain.Entities;
using MovieLand.Domain.Interfaces;
using MovieLand.Domain.Interfaces.Repositories;
using MovieLand.Domain.Specifications;
using System;
using System.Linq;
using System.Threading.Tasks;


namespace MovieLand.Application.Services
{
    public class CartService : ICartService
    {
        private readonly IRepository _cartRepository;
        private readonly IMovieRepository _movieRepository;
        private readonly IAppLogger<CartService> _logger;

        public CartService(IRepository cartRepository, IMovieRepository movieRepository, IAppLogger<CartService> logger)
        {
            _cartRepository = cartRepository ?? throw new ArgumentNullException(nameof(cartRepository));
            _movieRepository = movieRepository ?? throw new ArgumentNullException(nameof(movieRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }


        public async Task AddItem(string username, int movieId)
        {
            var cart = await GetExistingOrCreateNewCart(username);
            var movie = await _movieRepository.GetByIdAsync(movieId);

            cart.AddItem(movieId, unitPrice: movie.UnitPrice);

            await _cartRepository.UpdateAsync(cart);
        }


        public async Task RemoveItem(int cartId, int cartItemId)
        {
            var spec = new CartWithItemsSpecification(cartId);

            var cart = (await _cartRepository.GetAsync(spec)).FirstOrDefault();
            cart.RemoveItem(cartItemId);

            await _cartRepository.UpdateAsync(cart);
        }


        public async Task ClearCart(string username)
        {
            var cart = await _cartRepository.GetByUsernameAsync(username);

            if (cart == null)
                throw new ApplicationException("Submitted order should have cart!!!");

            cart.ClearItems();

            await _cartRepository.UpdateAsync(cart);
        }


        public async Task<CartDTO> GetCartByUsername(string username)
        {
            var cart = await GetExistingOrCreateNewCart(username);
            var cartDTO = ObjectMapper.Mapper.Map<CartDTO>(cart);

            // If movie can't be loaded from razor page, we than manual map it
            if (cart.Items.Any(c => c.Movie == null))
            {
                cartDTO.Items.Clear();
                
                foreach (var item in cart.Items)
                {
                    var cartItemDTO = ObjectMapper.Mapper.Map<CartItemDTO>(item);
                    var movie = await _movieRepository.GetMovieByIdWithGenresAsync(item.MovieId);
                    var movieDTO = ObjectMapper.Mapper.Map<MovieDTO>(movie);
                    cartItemDTO.Movie = movieDTO;
                    cartDTO.Items.Add(cartItemDTO);
                }
            }

            return cartDTO;
        }


        private async Task<Cart> GetExistingOrCreateNewCart(string username)
        {
            var cart = await _cartRepository.GetByUsernameAsync(username);

            if (cart != null)
                return cart;

            // If it's first time create new cart
            var newCart = new Cart
            {
                Username = username
            };

            await _cartRepository.AddAsync(newCart);
            
            return newCart;
        }
    }
}
