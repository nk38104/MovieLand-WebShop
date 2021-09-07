using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieLand.Web.Interfaces;
using MovieLand.Web.ViewModels;


namespace MovieLand.Web.Pages.CheckOut
{
    [Authorize]
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
            var user = User.Identity;

            if (user != null)
                CartViewModel = await _checkOutPageService.GetCart(user.Name);
        }


        public async Task<IActionResult> OnPostAsync()
        {
            var user = User.Identity;

            if (ModelState.IsValid)
            {
                try
                {
                    await _checkOutPageService.CheckOutOrder(Order, user.Name);
                    return RedirectToPage("./OrderSubmitted");
                }
                catch(Exception ex)
                {
                    return RedirectToPage("./OrderNotSubmitted", ex.ToString());
                }
            }
            return RedirectToPage();
        }
    }
}
