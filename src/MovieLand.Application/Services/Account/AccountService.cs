using Microsoft.AspNetCore.Identity;
using MovieLand.Application.DTOs;
using MovieLand.Application.DTOs.Account;
using MovieLand.Application.Interfaces.Account;
using System.Threading.Tasks;


namespace MovieLand.Application.Services.Account
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountService(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }


        public async Task<SignInResult> LoginUserAsync(LoginDTO userLoginData)
        {
            // To enable password failures to trigger account lockout, set lockoutOnFailure: true
            return await _signInManager.PasswordSignInAsync(userLoginData.Email, userLoginData.Password, userLoginData.RememberMe, lockoutOnFailure: false);
        }


        public async Task LogoutUserAsync()
        {
            await _signInManager.SignOutAsync();
        }

        
        public async Task<bool> RegisterUserAsync(RegisterDTO userRegistrationData)
        {
            var newIdentityUser = new IdentityUser { Email = userRegistrationData.Email, UserName = userRegistrationData.Email };
            var registrationResult = await _userManager.CreateAsync(newIdentityUser, userRegistrationData.Password);

            if (registrationResult.Succeeded)
            {
                var roleResult = await AddDefaultRoleToUserAsync(newIdentityUser, "Client", userRegistrationData);
                
                return roleResult.Succeeded;
            }
            return registrationResult.Succeeded;
        }


        private async Task<IdentityResult> AddDefaultRoleToUserAsync(IdentityUser newUser, string role, RegisterDTO userRegistrationData)
        {
            var roleResult = await _userManager.AddToRoleAsync(newUser, "Client");

            if (roleResult.Succeeded)
            {
                await _signInManager.PasswordSignInAsync(newUser.Email, userRegistrationData.Password, false, lockoutOnFailure: false);
                return roleResult;
            }
            return roleResult;
        }
    }
}
