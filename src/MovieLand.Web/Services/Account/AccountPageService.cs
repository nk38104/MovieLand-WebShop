using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using MovieLand.Application.DTOs;
using MovieLand.Application.DTOs.Account;
using MovieLand.Application.Interfaces.Account;
using MovieLand.Web.Interfaces;
using MovieLand.Web.ViewModels;
using MovieLand.Web.ViewModels.Account;
using System;
using System.Threading.Tasks;


namespace MovieLand.Web.Services
{
    public class AccountPageService : IAccountPageService
    {
        private readonly IAccountService _accountService;
        private readonly IMapper _mapper;
        private readonly ILogger<AccountPageService> _logger;

        public AccountPageService(IAccountService accountService, IMapper mapper, ILogger<AccountPageService> logger)
        {
            _accountService = accountService ?? throw new ArgumentNullException(nameof(accountService));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }


        public async Task<SignInResult> LoginUser(LoginViewModel userLoginData)
        {
            var userLoginDataMapped = _mapper.Map<LoginDTO>(userLoginData);

            return await _accountService.LoginUserAsync(userLoginDataMapped);
        }


        public async Task LogoutUser()
        {
            await _accountService.LogoutUserAsync();
        }


        public async Task<bool> RegisterUser(RegisterViewModel userRegistrationData)
        {
            var userRegistrationDataMapped = _mapper.Map<RegisterDTO>(userRegistrationData);
            return await _accountService.RegisterUserAsync(userRegistrationDataMapped);
        }
    }
}
