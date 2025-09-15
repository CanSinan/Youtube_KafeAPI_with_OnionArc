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
    }
}
