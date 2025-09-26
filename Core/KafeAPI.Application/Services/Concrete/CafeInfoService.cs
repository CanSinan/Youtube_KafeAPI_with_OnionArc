using AutoMapper;
using FluentValidation;
using KafeAPI.Application.Dtos.CafeInfoDtos;
using KafeAPI.Application.Dtos.ResponseDtos;
using KafeAPI.Application.Interfaces;
using KafeAPI.Application.Services.Abstract;
using KafeAPI.Domain.Entities;

namespace KafeAPI.Application.Services.Concrete
{
    public class CafeInfoService : ICafeInfoService
    {
        private readonly IGenericRepository<CafeInfo> _genericCafeInfoRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateCafeInfoDto> _createCafeInfoValidator;
        private readonly IValidator<UpdateCafeInfoDto> _updateCafeInfoValidator;

        public CafeInfoService(IGenericRepository<CafeInfo> genericCafeInfoRepository, IMapper mapper, IValidator<CreateCafeInfoDto> createCafeInfoValidator, IValidator<UpdateCafeInfoDto> updateCafeInfoValidator)
        {
            _genericCafeInfoRepository = genericCafeInfoRepository;
            _mapper = mapper;
            _createCafeInfoValidator = createCafeInfoValidator;
            _updateCafeInfoValidator = updateCafeInfoValidator;
        }


        public async Task<ResponseDto<List<ResultCafeInfoDto>>> GetAllCafeInfo()
        {
            try
            {
                var cafeInfos = await _genericCafeInfoRepository.GetAllAsync();
                if (cafeInfos == null || !cafeInfos.Any())
                {
                    return new ResponseDto<List<ResultCafeInfoDto>>
                    {
                        Success = false,
                        Data = null,
                        ErrorCode = ErrorCodes.NotFound,
                        Message = "Kafe bilgisi bulunamadı"
                    };
                }
                var result = _mapper.Map<List<ResultCafeInfoDto>>(cafeInfos);
                return new ResponseDto<List<ResultCafeInfoDto>>
                {
                    Success = true,
                    Data = result
                };

            }
            catch (Exception ex)
            {
                return new ResponseDto<List<ResultCafeInfoDto>>
                {
                    Success = false,
                    Data = null,
                    ErrorCode = ErrorCodes.Exception,
                    Message = "Bir hata oluştu"
                };
            }
        }

        public async Task<ResponseDto<DetailCafeInfoDto>> GetByIdCafeInfo(int id)
        {
            try
            {
                var cafeInfo = await _genericCafeInfoRepository.GetByIdAsync(id);
                if (cafeInfo == null)
                {
                    return new ResponseDto<DetailCafeInfoDto>
                    {
                        Success = false,
                        Data = null,
                        ErrorCode = ErrorCodes.NotFound,
                        Message = "Kafe bilgisi bulunamadı"
                    };
                }
                var result = _mapper.Map<DetailCafeInfoDto>(cafeInfo);
                return new ResponseDto<DetailCafeInfoDto>
                {
                    Success = true,
                    Data = result
                };
            }
            catch (Exception ex)
            {
                return new ResponseDto<DetailCafeInfoDto>
                {
                    Success = false,
                    Data = null,
                    ErrorCode = ErrorCodes.Exception,
                    Message = "Bir hata oluştu"
                };
            }
        }
        public async Task<ResponseDto<object>> AddCafeInfo(CreateCafeInfoDto dto)
        {
            try
            {
                var validate = await _createCafeInfoValidator.ValidateAsync(dto);
                if (!validate.IsValid)
                {
                    return new ResponseDto<object>
                    {
                        Success = false,
                        Data = null,
                        ErrorCode = ErrorCodes.ValidationError,
                        Message = string.Join(" | ", validate.Errors.Select(e => e.ErrorMessage))
                    };
                }
                var cafeInfo = _mapper.Map<CafeInfo>(dto);
                await _genericCafeInfoRepository.AddAsync(cafeInfo);
                return new ResponseDto<object>
                {
                    Success = true,
                    Data = null,
                    Message = "Kafe bilgisi başarıyla eklendi"
                };
            }
            catch (Exception ex)
            {
                return new ResponseDto<object>
                {
                    Success = false,
                    Data = null,
                    ErrorCode = ErrorCodes.Exception,
                    Message = "Bir hata oluştu"
                };
            }
        }

        public async Task<ResponseDto<object>> UpdateCafeInfo(UpdateCafeInfoDto dto)
        {
            try
            {
                var validate = await _updateCafeInfoValidator.ValidateAsync(dto);
                if (!validate.IsValid)
                {
                    return new ResponseDto<object>
                    {
                        Success = false,
                        Data = null,
                        ErrorCode = ErrorCodes.ValidationError,
                        Message = string.Join(" | ", validate.Errors.Select(e => e.ErrorMessage))
                    };
                }
                var cafeInfo = await _genericCafeInfoRepository.GetByIdAsync(dto.Id);
                if (cafeInfo == null)
                {
                    return new ResponseDto<object>
                    {
                        Success = false,
                        Data = null,
                        ErrorCode = ErrorCodes.NotFound,
                        Message = "Kafe bilgisi bulunamadı"
                    };
                }
                var result = _mapper.Map(dto, cafeInfo);
                await _genericCafeInfoRepository.UpdateAsync(result);
                return new ResponseDto<object>
                {
                    Success = true,
                    Data = null,
                    Message = "Kafe bilgisi başarıyla güncellendi"
                };
            }
            catch (Exception ex)
            {
                return new ResponseDto<object>
                {
                    Success = false,
                    Data = null,
                    ErrorCode = ErrorCodes.Exception,
                    Message = "Bir hata oluştu"
                };
            }
        }
        public async Task<ResponseDto<object>> DeleteCafeInfo(int id)
        {
            try
            {
                var cafeInfo = await _genericCafeInfoRepository.GetByIdAsync(id);
                if (cafeInfo == null)
                {
                    return new ResponseDto<object>
                    {
                        Success = false,
                        Data = null,
                        ErrorCode = ErrorCodes.NotFound,
                        Message = "Kafe bilgisi bulunamadı"
                    };
                }

                await _genericCafeInfoRepository.DeleteAsync(cafeInfo);
                return new ResponseDto<object>
                {
                    Success = true,
                    Data = null,
                    Message = "Kafe bilgisi başarıyla silindi"
                };
            }
            catch (Exception ex)
            {
                return new ResponseDto<object>
                {
                    Success = false,
                    Data = null,
                    ErrorCode = ErrorCodes.Exception,
                    Message = "Bir hata oluştu"
                };
            }
        }
    }
}
