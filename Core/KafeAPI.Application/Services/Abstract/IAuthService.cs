using KafeAPI.Application.Dtos.ResponseDtos;

namespace KafeAPI.Application.Services.Abstract
{
    public interface IAuthService
    {
        Task<ResponseDto<object>> GenerateToken(string email);
    }
}
