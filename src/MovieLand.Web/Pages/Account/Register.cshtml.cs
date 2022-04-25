using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieLand.Application.DTOs;
using MovieLand.Application.Interfaces.Account;
using System.Threading.Tasks;


namespace MovieLand.Web.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly IAccountService _accountService;
        [BindProperty]
        public RegisterDTO RegisterViewModel { get; set; } = new RegisterDTO();

        public RegisterModel(IAccountService accountService)
        {
            _accountService = accountService;
        }


        public void OnGet()
        {
        }


        //Set validation on client, password, ...
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var registrationResult = await _accountService.RegisterUserAsync(RegisterViewModel);

                if (registrationResult)
                {
                    return LocalRedirect("/");
                }
            }
            return Page();
        }
    }
}
