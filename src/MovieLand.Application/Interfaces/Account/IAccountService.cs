using MovieLand.Application.DTOs;
using System.Threading.Tasks;


namespace MovieLand.Application.Interfaces.Account
{
    public interface IAccountService
    {
        Task<bool> RegisterUserAsync(RegisterDTO userRegistrationData);
    }
}
