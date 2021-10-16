using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieLand.Web.Interfaces;
using MovieLand.Web.ViewModels;


namespace MovieLand.Web.Pages.Order
{
    [Authorize]
    public class DetailsModel : PageModel
    {
        private readonly ICheckOutPageService _checkOutPageService;
        
        public OrderViewModel Order { get; set; }

        public DetailsModel(ICheckOutPageService checkOutPageService)
        {
            _checkOutPageService = checkOutPageService ?? throw new ArgumentNullException(nameof(checkOutPageService));
        }


        public async Task<IActionResult> OnGetAsync(int? orderId)
        {
            if (orderId == null)
            {
                return NotFound();
            }

            Order = await _checkOutPageService.GetOrderById((int)orderId);

            return (Order == null) ? NotFound() : Page();
        }
    }
}
