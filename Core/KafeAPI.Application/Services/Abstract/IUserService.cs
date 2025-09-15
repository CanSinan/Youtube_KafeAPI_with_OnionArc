using KafeAPI.Application.Dtos.ResponseDtos;
using KafeAPI.Application.Dtos.UserDtos;

namespace KafeAPI.Application.Services.Abstract
{
    public interface IUserService
    {
        Task<ResponseDto<object>> Register(RegisterDto dto);

    }
}
