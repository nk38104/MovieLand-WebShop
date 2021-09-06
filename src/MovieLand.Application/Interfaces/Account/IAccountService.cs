using Microsoft.AspNetCore.Identity;
using MovieLand.Application.DTOs;
using MovieLand.Application.DTOs.Account;
using System.Threading.Tasks;


namespace MovieLand.Application.Interfaces.Account
{
    public interface IAccountService
    {
        Task<SignInResult> LoginUserAsync(LoginDTO userLoginData);
        Task LogoutUserAsync();
        Task<bool> RegisterUserAsync(RegisterDTO userRegistrationData);
    }
}
