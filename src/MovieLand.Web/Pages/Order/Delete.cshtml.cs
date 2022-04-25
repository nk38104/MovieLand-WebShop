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
    public class DeleteModel : PageModel
    {
        private readonly ICheckoutService _checkoutService;
        
        [BindProperty]
        public OrderDTO Order { get; set; }

        public DeleteModel(ICheckoutService checkoutService)
        {
            _checkoutService = checkoutService ?? throw new ArgumentNullException(nameof(checkoutService));
        }


        public async Task<IActionResult> OnGetAsync(int? orderId)
        {
            if (orderId == null)
            {
                return NotFound();
            }

            Order = await _checkoutService.GetOrderById((int)orderId);

            return (Order == null) ? NotFound() : Page();
        }


        public async Task<IActionResult> OnPostAsync(int orderId)
        {
            // Change delete to cascade from restricted
            // Add delete validation later
            await _checkoutService.DeleteOrder(orderId);

            return RedirectToPage("../Index");
        }
    }
}
