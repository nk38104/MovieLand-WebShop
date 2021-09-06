using MovieLand.Web.ViewModels;
using System.Threading.Tasks;


namespace MovieLand.Web.Interfaces
{
    public interface IAccountPageService
    {
        Task<bool> RegisterUser(RegisterViewModel userRegistrationData);
    }
}
