using Microsoft.AspNetCore.Mvc.RazorPages;


namespace MovieLand.Web.Pages.CheckOut
{
    public class OrderNotSubmittedModel : PageModel
    {
        public string Exception { get; set; }

        public void OnGet(string exception)
        {
            Exception = exception;
        }
    }
}
