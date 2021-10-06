using MovieLand.Application.DTOs.Base;
using System.ComponentModel.DataAnnotations;


namespace MovieLand.Application.DTOs
{
    public class RegisterDTO : BaseDTO
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }

        [Required]
        public string ConfirmPassword { get; set; }
    }
}
