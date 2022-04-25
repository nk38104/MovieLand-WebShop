using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieLand.Application.DTOs;
using MovieLand.Application.Interfaces;


namespace MovieLand.Web.Pages.Cart
{
    [Authorize]
    public class CartModel : PageModel
    {
        private readonly ICartService _cartService;
        public CartDTO Cart { get; set; } = new CartDTO();

        public CartModel(ICartService cartService)
        {
            _cartService = cartService ?? throw new ArgumentNullException(nameof(cartService));
        }


        public async Task OnGetAsync()
        {
            var user = User.Identity;

            if (user != null)
            {
                Cart = await _cartService.GetCartByUsername(user.Name);
            }
        }


        public async Task<IActionResult> OnPostRemoveFromCartAsync(int cartId, int cartItemId)
        {
            await _cartService.RemoveItem(cartId, cartItemId);

            return RedirectToPage();
        }
    }
}
