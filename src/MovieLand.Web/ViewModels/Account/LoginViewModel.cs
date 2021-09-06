using MovieLand.Web.ViewModels.Base;
using System.ComponentModel.DataAnnotations;


namespace MovieLand.Web.ViewModels.Account
{
    public class LoginViewModel : BaseViewModel
    {
        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
