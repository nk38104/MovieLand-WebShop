using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieLand.Web.Interfaces;
using MovieLand.Web.ViewModels;


namespace MovieLand.Web.Pages.Cart
{
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
            //if (!User.Identity.IsAuthenticated)
            //{
            //    return View(cartViewModel);
            //}
            var username = "bg123";

            CartViewModel = await _cartPageService.GetCart(username);
        }


        public async Task<IActionResult> OnPostRemoveFromCartAsync(int cartId, int cartItemId)
        {
            await _cartPageService.RemoveItem(cartId, cartItemId);

            return RedirectToPage();
        }
    }
}
