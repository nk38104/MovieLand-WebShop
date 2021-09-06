using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieLand.Web.Interfaces;
using MovieLand.Web.ViewModels.Account;

namespace MovieLand.Web.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly IAccountPageService _accountPageService;
        [BindProperty]
        public LoginViewModel LoginViewModel { get; set; } = new();

        public LoginModel(IAccountPageService accountPageService)
        {
            _accountPageService = accountPageService ?? throw new ArgumentNullException(nameof(accountPageService));
        }


        public void OnGet()
        {
        }


        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var resultLogin = await _accountPageService.LoginUser(LoginViewModel);
                
                if (resultLogin.Succeeded)
                    return LocalRedirect("/");

                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }
            return Page();
        }


        public async Task<IActionResult> OnPostLogoutAsync()
        {
            await _accountPageService.LogoutUser();
            return LocalRedirect("/");
        }
    }
}
