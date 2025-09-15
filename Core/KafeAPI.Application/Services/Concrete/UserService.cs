using FluentValidation;
using KafeAPI.Application.Dtos.ResponseDtos;
using KafeAPI.Application.Dtos.UserDtos;
using KafeAPI.Application.Interfaces;
using KafeAPI.Application.Services.Abstract;
using KafeAPI.Application.Validators.User;

namespace KafeAPI.Application.Services.Concrete
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IValidator<RegisterDto> _registerValidator;
        public UserService(IUserRepository userRepository, IValidator<RegisterDto> registerValidator)
        {
            _userRepository = userRepository;
            _registerValidator = registerValidator;
        }

        public async Task<ResponseDto<object>> Register(RegisterDto dto)
        {
            try
            {
                var validate = await _registerValidator.ValidateAsync(dto);
                if (!validate.IsValid)
                {
                    return new ResponseDto<object>
                    {
                        Success = false,
                        Data = null,
                        Message = string.Join("|", validate.Errors.Select(e => e.ErrorMessage)),
                        ErrorCode = ErrorCodes.ValidationError
                    };
                }
                var result = await _userRepository.RegisterAsync(dto);
                if (result.Succeeded)
                {
                    return new ResponseDto<object>
                    {
                        Success = true,
                        Data = null,
                        Message="Kayıt işlemi başarılı bir şekilde gerçekleştirildi.",
                    };
                }
                else
                {
                    return new ResponseDto<object>
                    {
                        Success = false,
                        Data = null,
                        Message = string.Join("|", result.Errors.Select(e => e.Description))
                    };
                }
            }
            catch (Exception ex)
            {
                return new ResponseDto<object>
                {
                    Success = false,
                    Data = null,
                    Message = "Bir hata oluştu.",
                    ErrorCode = ErrorCodes.Exception
                };
            }
        }
    }
}
