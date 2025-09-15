using KafeAPI.Application.Dtos.AuthDtos;
using KafeAPI.Application.Dtos.ResponseDtos;

namespace KafeAPI.Application.Services.Abstract
{
    public interface IAuthService
    {
        Task<ResponseDto<object>> GenerateToken(TokenDto dto);
    }
}
