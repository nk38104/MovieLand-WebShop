using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieLand.Web.Interfaces;
using MovieLand.Web.ViewModels;
using System;


namespace MovieLand.Web.Pages.ContactUs
{
    public class ContactUsModel : PageModel
    {
        private readonly IEmailService _emailService;

        [BindProperty]
        public ContactViewModel Contact { get; set; }

        public ContactUsModel(IEmailService emailWebService)
        {
            _emailService = emailWebService ?? throw new ArgumentNullException(nameof(emailWebService));
        }


        public void OnGet()
        {
        }


        public IActionResult OnPost()
        {
            if (Contact != null)
            {
                _emailService.SendEmail(Contact);
            }

            return RedirectToPage();
        }
    }
}
