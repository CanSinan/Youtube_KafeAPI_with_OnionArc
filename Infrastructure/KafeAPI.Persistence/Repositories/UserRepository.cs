using KafeAPI.Application.Dtos.UserDtos;
using KafeAPI.Application.Interfaces;
using KafeAPI.Persistence.AppContext.Identity;
using Microsoft.AspNetCore.Identity;

namespace KafeAPI.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserManager<AppIdentityUser> _userManager;
        private readonly SignInManager<AppIdentityUser> _signInManager;

        public UserRepository(UserManager<AppIdentityUser> userManager, SignInManager<AppIdentityUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<UserDto> ChechkUser(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null)
            {
                return new UserDto { Id = user.Id, Email = user.Email };
            }
            return new UserDto();
        }

        public async Task<SignInResult> ChechkUserWithPassword(LoginDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            var result = await _signInManager.PasswordSignInAsync(user, dto.Password, false, false);
            return result;
        }

        public async Task<SignInResult> LoginAsync(LoginDto dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            var result = await _signInManager.PasswordSignInAsync(user, dto.Password, true, false);
            return result;
        }

        public async Task LogoutAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<IdentityResult> RegisterAsync(RegisterDto dto)
        {
            var user = new AppIdentityUser
            {
                Name = dto.Name,
                Surname = dto.Surname,
                Email = dto.Email,
                PhoneNumber = dto.Phone,
                UserName = dto.Email,
            };
            var result = await _userManager.CreateAsync(user, dto.Password);
            return result;
        }
    }
}
