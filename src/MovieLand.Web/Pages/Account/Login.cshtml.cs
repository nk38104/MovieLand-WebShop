using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieLand.Application.DTOs.Account;
using MovieLand.Application.Interfaces.Account;


namespace MovieLand.Web.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly IAccountService _accountService;
        [BindProperty]
        public LoginDTO LoginViewModel { get; set; } = new LoginDTO();

        public LoginModel(IAccountService accountService)
        {
            _accountService = accountService ?? throw new ArgumentNullException(nameof(accountService));
        }


        public void OnGet()
        {
        }


        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var resultLogin = await _accountService.LoginUserAsync(LoginViewModel);
                
                if (resultLogin.Succeeded)
                    return LocalRedirect("/");

                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }
            return Page();
        }


        public async Task<IActionResult> OnPostLogoutAsync()
        {
            await _accountService.LogoutUserAsync();
            return LocalRedirect("/");
        }
    }
}
