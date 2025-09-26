using AutoMapper;
using FluentValidation;
using KafeAPI.Application.Dtos.CafeInfoDtos;
using KafeAPI.Application.Dtos.ResponseDtos;
using KafeAPI.Application.Dtos.ReviewDtos;
using KafeAPI.Application.Interfaces;
using KafeAPI.Application.Services.Abstract;
using KafeAPI.Domain.Entities;

namespace KafeAPI.Application.Services.Concrete
{
    public class ReviewService : IReviewService
    {
        private readonly IGenericRepository<Review> _reviewGenericRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateReviewDto> _createReviewValidator;
        private readonly IValidator<UpdateReviewDto> _updateReviewValidator;
        public ReviewService(IGenericRepository<Review> reviewGenericRepository, IMapper mapper, IValidator<CreateReviewDto> createReviewValidator, IValidator<UpdateReviewDto> updateReviewValidator)
        {
            _reviewGenericRepository = reviewGenericRepository;
            _mapper = mapper;
            _createReviewValidator = createReviewValidator;
            _updateReviewValidator = updateReviewValidator;
        }

        public async Task<ResponseDto<List<ResultReviewDto>>> GetAllReviews()
        {
            try
            {
                var reviews = await _reviewGenericRepository.GetAllAsync();
                if (reviews == null || !reviews.Any())
                {
                    return new ResponseDto<List<ResultReviewDto>>
                    {
                        Success = false,
                        Data = null,
                        ErrorCode = ErrorCodes.NotFound,
                        Message = "Yorum bulunamadı"
                    };
                }
                var result = _mapper.Map<List<ResultReviewDto>>(reviews);
                return new ResponseDto<List<ResultReviewDto>>
                {
                    Success = true,
                    Data = result
                };
            }
            catch (Exception ex)
            {
                return new ResponseDto<List<ResultReviewDto>>
                {
                    Success = false,
                    Data = null,
                    Message = "Bir hata oluştu.",
                    ErrorCode = ErrorCodes.Exception
                };
            }
        }

        public async Task<ResponseDto<DetailReviewDto>> GetByIdReview(int id)
        {
            try
            {
                var review = await _reviewGenericRepository.GetByIdAsync(id);
                if (review == null)
                {
                    return new ResponseDto<DetailReviewDto>
                    {
                        Success = false,
                        Data = null,
                        ErrorCode = ErrorCodes.NotFound,
                        Message = "Yorum bulunamadı"
                    };
                }
                var result = _mapper.Map<DetailReviewDto>(review);
                return new ResponseDto<DetailReviewDto>
                {
                    Success = true,
                    Data = result
                };
            }
            catch (Exception ex)
            {
                return new ResponseDto<DetailReviewDto>
                {
                    Success = false,
                    Data = null,
                    Message = "Bir hata oluştu.",
                    ErrorCode = ErrorCodes.Exception
                };
            }
        }

        public async Task<ResponseDto<object>> AddReview(CreateReviewDto dto)
        {
            try
            {
                var validate = await _createReviewValidator.ValidateAsync(dto);
                if (!validate.IsValid)
                {
                    return new ResponseDto<object>
                    {
                        Success = false,
                        Data = null,
                        Message = string.Join(" | ", validate.Errors.Select(e => e.ErrorMessage)),
                        ErrorCode = ErrorCodes.ValidationError
                    };
                }
                var result = _mapper.Map<Review>(dto);
                await _reviewGenericRepository.AddAsync(result);
                return new ResponseDto<object>
                {
                    Success = true,
                    Data = null,
                    Message = "Yorum başarıyla oluşturuldu."
                };
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

        public async Task<ResponseDto<object>> UpdateReview(UpdateReviewDto dto)
        {
            try
            {
                var validate = await _updateReviewValidator.ValidateAsync(dto);
                if (!validate.IsValid)
                {
                    return new ResponseDto<object>
                    {
                        Success = false,
                        Data = null,
                        Message = string.Join(" | ", validate.Errors.Select(e => e.ErrorMessage)),
                        ErrorCode = ErrorCodes.ValidationError
                    };
                }
                var review = await _reviewGenericRepository.GetByIdAsync(dto.Id);
                if (review == null)
                {
                    return new ResponseDto<object>
                    {
                        Success = false,
                        Data = null,
                        Message = "Yorum bulunamadı.",
                        ErrorCode = ErrorCodes.NotFound
                    };
                }
                var result = _mapper.Map(dto, review);
                await _reviewGenericRepository.UpdateAsync(result);
                return new ResponseDto<object>
                {
                    Success = true,
                    Data = null,
                    Message = "Yorum başarıyla güncellendi."
                };
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

        public async Task<ResponseDto<object>> DeleteReview(int id)
        {
            try
            {
                var review = await _reviewGenericRepository.GetByIdAsync(id);
                if (review == null)
                {
                    return new ResponseDto<object>
                    {
                        Success = false,
                        Data = null,
                        Message = "Yorum bulunamadı.",
                        ErrorCode = ErrorCodes.NotFound
                    };
                }
                await _reviewGenericRepository.DeleteAsync(review);
                return new ResponseDto<object>
                {
                    Success = true,
                    Data = null,
                    Message = "Yorum başarıyla silindi."
                };
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
