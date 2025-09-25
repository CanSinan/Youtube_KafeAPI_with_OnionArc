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

        public async Task<ResponseDto<object>> AddRoleToUser(string email, string role)
        {
            try
            {
                var result = await _userRepository.AddRoleToUserAsync(email, role);
                if (result)
                {
                    return new ResponseDto<object>
                    {
                        Success = true,
                        Data = null,
                        Message = "Rol ataması yapıldı."
                    };
                }
                else
                {
                    return new ResponseDto<object>
                    {
                        Success = false,
                        Data = null,
                        Message = "Rol ataması yapılırken hata oluştu.",
                        ErrorCode = ErrorCodes.BadRequest
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

        public async Task<ResponseDto<object>> CreateRole(string roleName)
        {
            try
            {
                var result = await _userRepository.CreateRoleAsync(roleName);
                if (result)
                {
                    return new ResponseDto<object>
                    {
                        Success = true,
                        Data = null,
                        Message = "Rol başarıyla oluşturuldu."
                    };
                }
                else
                {
                    return new ResponseDto<object>
                    {
                        Success = false,
                        Data = null,
                        Message = "Rol oluşturulurken hata oluştu.",
                        ErrorCode = ErrorCodes.BadRequest
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

        public async Task<ResponseDto<object>> RegisterDefault(RegisterDto dto)
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
                    var roleResult = await _userRepository.AddRoleToUserAsync(dto.Email, "user");
                    if (roleResult)
                    {
                        return new ResponseDto<object>
                        {
                            Success = true,
                            Data = null,
                            Message = "Kayıt işlemi başarılı bir şekilde gerçekleştirildi.",
                        };
                    }
                    else
                    {
                        return new ResponseDto<object>
                        {
                            Success = true,
                            Data = null,
                            Message = "Kullanıcı oluşturuldu rol ataması yaparken bir hata oluştu. Lütfen yetkiliye başvurunuz.",
                            ErrorCode = ErrorCodes.BadRequest
                        };
                    }
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
