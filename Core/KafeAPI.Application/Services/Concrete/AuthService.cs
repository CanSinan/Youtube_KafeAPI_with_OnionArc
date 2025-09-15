using KafeAPI.Application.Dtos.AuthDtos;
using KafeAPI.Application.Dtos.ResponseDtos;
using KafeAPI.Application.Helpers;
using KafeAPI.Application.Services.Abstract;

namespace KafeAPI.Application.Services.Concrete
{
    public class AuthService : IAuthService
    {
        private readonly TokenHelpers _tokenHelpers;

        public AuthService(TokenHelpers tokenHelpers)
        {
            _tokenHelpers = tokenHelpers;
        }

        public async Task<ResponseDto<object>> GenerateToken(TokenDto dto)
        {
            try
            {
                var checkUser = dto.Email == "sinan51can@gmail.com" ? true : false;
                if (checkUser)
                {
                    string token = _tokenHelpers.GenerateToken(dto);
                    return new ResponseDto<object>
                    {
                        Success = true,
                        Data = token
                    };
                }
                return new ResponseDto<object>
                {
                    Success = false,
                    Data = null,
                    Message = "Kullanıcı bulunamadı",
                    ErrorCode = ErrorCodes.Unauthorized
                };
            }
            catch (Exception ex)
            {
                return new ResponseDto<object>
                {
                    Success = false,
                    Data = null,
                    Message = " bir hata oluştu",
                    ErrorCode = ErrorCodes.Exception
                };
            }
        }
    }
}
