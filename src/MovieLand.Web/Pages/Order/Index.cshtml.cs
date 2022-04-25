using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieLand.Application.DTOs;
using MovieLand.Application.Interfaces;


namespace MovieLand.Web.Pages.Order
{
    [Authorize]
    public class IndexModel : PageModel
    {
        private readonly ICheckoutService _checkoutService;
        [BindProperty]
        public IEnumerable<OrderDTO> Orders { get; set; } = new List<OrderDTO>();

        public IndexModel(ICheckoutService checkoutService)
        {
            _checkoutService = checkoutService ?? throw new ArgumentNullException(nameof(checkoutService));
        }


        public async Task OnGetAsync()
        {
            Orders = await _checkoutService.GetOrders();
        }
    }
}
