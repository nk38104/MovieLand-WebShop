using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieLand.Web.Services;
using MovieLand.Web.ViewModels;


namespace MovieLand.Web.Pages.Order
{
    public class DetailsModel : PageModel
    {
        private readonly CheckOutPageService _checkOutPageService;

        public DetailsModel(CheckOutPageService checkOutPageService)
        {
            _checkOutPageService = checkOutPageService;
        }

        public OrderViewModel Order { get; set; }


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
