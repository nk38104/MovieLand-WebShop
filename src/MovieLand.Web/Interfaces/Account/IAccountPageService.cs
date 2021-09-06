using Microsoft.AspNetCore.Identity;
using MovieLand.Web.ViewModels;
using MovieLand.Web.ViewModels.Account;
using System.Threading.Tasks;


namespace MovieLand.Web.Interfaces
{
    public interface IAccountPageService
    {
        Task<SignInResult> LoginUser(LoginViewModel userLoginData);
        Task LogoutUser();
        Task<bool> RegisterUser(RegisterViewModel userRegistrationData);
    }
}
