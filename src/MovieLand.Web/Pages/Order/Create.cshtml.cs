using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieLand.Application.DTOs;
using MovieLand.Application.Interfaces;


namespace MovieLand.Web.Pages.Order
{
    [Authorize]
    public class CreateModel : PageModel
    {
        private readonly ICheckoutService _checkoutService;
        public CartDTO CartViewModel { get; set; } = new CartDTO();
        [BindProperty]
        public OrderDTO Order { get; set; }

        public CreateModel(ICheckoutService checkoutService)
        {
            _checkoutService = checkoutService ?? throw new ArgumentNullException(nameof(checkoutService));
        }


        public async Task OnGetAsync()
        {
            var user = User.Identity;

            if (user != null)
                CartViewModel = await _checkoutService.GetCart(user.Name);
        }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            var user = User.Identity;

            if (ModelState.IsValid)
            {
                try
                {
                    await _checkoutService.CheckOutOrder(Order, user.Name);
                    return RedirectToPage("./OrderSubmitted");
                }
                catch (Exception ex)
                {
                    return RedirectToPage("./OrderNotSubmitted", ex.ToString());
                }
            }
            return RedirectToPage();
        }
    }
}
