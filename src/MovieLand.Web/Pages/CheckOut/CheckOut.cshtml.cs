using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieLand.Web.Interfaces;
using MovieLand.Web.ViewModels;


namespace MovieLand.Web.Pages.CheckOut
{
    //[Authorize]
    public class CheckOutModel : PageModel
    {
        private readonly ICheckOutPageService _checkOutPageService;
        public CartViewModel CartViewModel { get; set; } = new CartViewModel();
        [BindProperty]
        public OrderViewModel Order { get; set; }

        public CheckOutModel(ICheckOutPageService checkOutPageService)
        {
            _checkOutPageService = checkOutPageService ?? throw new ArgumentNullException(nameof(checkOutPageService));
        }


        public async Task OnGetAsync()
        {
            var username = "bg123";
            CartViewModel = await _checkOutPageService.GetCart(username);
        }


        public async Task<IActionResult> OnPostAsync()
        {
            var username = "bg123";

            if (!ModelState.IsValid)
            {
                CartViewModel = await _checkOutPageService.GetCart(username);
                return Page();
            }

            await _checkOutPageService.CheckOutOrder(Order, username);
            return RedirectToPage("./OrderSubmitted");
        }
    }
}
