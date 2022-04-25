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
    public class DetailsModel : PageModel
    {
        private readonly ICheckoutService _checkoutService;
        
        public OrderDTO Order { get; set; }

        public DetailsModel(ICheckoutService checkoutService)
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
    }
}
