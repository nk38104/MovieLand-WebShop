using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieLand.Application.DTOs;
using MovieLand.Infrastructure.Interfaces;
using System;


namespace MovieLand.Web.Pages.ContactUs
{
    public class ContactUsModel : PageModel
    {
        private readonly IEmailService _emailService;

        [BindProperty]
        public ContactDTO Contact { get; set; }

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
