using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieLand.Web.Interfaces;
using MovieLand.Web.ViewModels;


namespace MovieLand.Web.Pages.Cart
{
    [Authorize]
    public class CartModel : PageModel
    {
        private readonly ICartPageService _cartPageService;
        public CartViewModel CartViewModel { get; set; } = new CartViewModel();

        public CartModel(ICartPageService cartPageService)
        {
            _cartPageService = cartPageService ?? throw new ArgumentNullException(nameof(cartPageService));
        }


        public async Task OnGetAsync()
        {
            var user = User.Identity;

            if (user != null)
            {
                CartViewModel = await _cartPageService.GetCart(user.Name);
            }
        }


        public async Task<IActionResult> OnPostRemoveFromCartAsync(int cartId, int cartItemId)
        {
            await _cartPageService.RemoveItem(cartId, cartItemId);

            return RedirectToPage();
        }
    }
}
