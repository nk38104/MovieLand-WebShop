using MovieLand.Application.DTOs.Base;
using System.ComponentModel.DataAnnotations;


namespace MovieLand.Application.DTOs.Account
{
    public class LoginDTO : BaseDTO
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }

        public bool RememberMe { get; set; }
    }
}
