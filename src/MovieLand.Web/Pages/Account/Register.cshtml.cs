using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieLand.Web.Interfaces;
using MovieLand.Web.ViewModels;
using System.Threading.Tasks;


namespace MovieLand.Web.Pages.Account
{
    public class RegisterModel : PageModel
    {
        private readonly IAccountPageService _accountPageService;
        [BindProperty]
        public RegisterViewModel RegisterViewModel { get; set; } = new();

        public RegisterModel(IAccountPageService accountPageService)
        {
            _accountPageService = accountPageService;
        }


        public void OnGet()
        {
        }


        //Set validation on client, password, ...
        public async Task<IActionResult> OnPostAsync()
        {
            if (ModelState.IsValid)
            {
                var registrationResult = await _accountPageService.RegisterUser(RegisterViewModel);

                if (registrationResult)
                {
                    return LocalRedirect("/");
                }
            }
            return Page();
        }
    }
}
