using KafeAPI.Application.Dtos.AuthDtos;
using KafeAPI.Application.Dtos.ResponseDtos;
using KafeAPI.Application.Dtos.UserDtos;
using KafeAPI.Application.Helpers;
using KafeAPI.Application.Interfaces;
using KafeAPI.Application.Services.Abstract;

namespace KafeAPI.Application.Services.Concrete
{
    public class AuthService : IAuthService
    {
        private readonly TokenHelpers _tokenHelpers;
        private readonly IUserRepository _userRepository;
        public AuthService(TokenHelpers tokenHelpers, IUserRepository userRepository)
        {
            _tokenHelpers = tokenHelpers;
            _userRepository = userRepository;
        }

        public async Task<ResponseDto<object>> GenerateToken(LoginDto dto)
        {
            try
            {
                var checkUser = await _userRepository.ChechkUser(dto.Email);
                if (checkUser.Id != null)
                {
                    var user = await _userRepository.ChechkUserWithPassword(dto);
                    if (user.Succeeded)
                    {
                        var tokenDto = new TokenDto
                        {
                            Email = dto.Email,
                            Id = dto.Email,
                            Role = checkUser.Role
                        };
                        string token = _tokenHelpers.GenerateToken(tokenDto);
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
