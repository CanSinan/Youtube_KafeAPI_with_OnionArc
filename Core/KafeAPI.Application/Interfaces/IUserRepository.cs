using KafeAPI.Application.Dtos.UserDtos;
using KafeAPI.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace KafeAPI.Application.Interfaces
{
    public interface IUserRepository
    {
        Task<SignInResult> LoginAsync(LoginDto dto);
        Task LogoutAsync();
        Task<IdentityResult> RegisterAsync(RegisterDto dto);
        Task<UserDto> ChechkUser(string email);
        Task<SignInResult> ChechkUserWithPassword(LoginDto dto);
        Task<bool> CreateRoleAsync(string roleName);
        Task<bool> AddRoleToUserAsync(string email, string role);

    }
}
