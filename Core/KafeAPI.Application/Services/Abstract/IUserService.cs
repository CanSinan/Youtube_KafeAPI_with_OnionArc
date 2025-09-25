using KafeAPI.Application.Dtos.ResponseDtos;
using KafeAPI.Application.Dtos.UserDtos;

namespace KafeAPI.Application.Services.Abstract
{
    public interface IUserService
    {
        Task<ResponseDto<object>> Register(RegisterDto dto);
        Task<ResponseDto<object>> RegisterDefault(RegisterDto dto);
        Task<ResponseDto<object>> CreateRole(string roleName);
        Task<ResponseDto<object>> AddRoleToUser(string email, string role);

    }
}
