using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MovieLand.Web.ViewModels;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace MovieLand.Web.Pages.ContactUs
{
    public class ContactUsModel : PageModel
    {
        [BindProperty]
        public ContactViewModel Contact { get; set; }
        
        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (Contact != null)
            {
                var client = new SmtpClient("smtp.mailtrap.io", 2525)
                {
                    Credentials = new NetworkCredential("0fab9f4bc8a446", "cc52c1e25a21d4"),
                    EnableSsl = true
                };
                client.Send(Contact.Email, "movieland@gmail.com", Contact.Subject, Contact.Message);
            }

            return RedirectToPage();
        }
    }
}
