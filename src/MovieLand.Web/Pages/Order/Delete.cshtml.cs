using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieLand.Web.Interfaces;
using MovieLand.Web.ViewModels;


namespace MovieLand.Web.Pages.Order
{
    public class DeleteModel : PageModel
    {
        private readonly ICheckOutPageService _checkOutPageService;
        
        [BindProperty]
        public OrderViewModel Order { get; set; }

        public DeleteModel(ICheckOutPageService checkOutPageService)
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


        public async Task<IActionResult> OnPostAsync(int orderId)
        {
            // Change delete to cascade from restricted
            // Add delete validation later
            await _checkOutPageService.DeleteOrder(orderId);

            return RedirectToPage("../Index");
        }
    }
}
