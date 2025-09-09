using AutoMapper;
using FluentValidation;
using KafeAPI.Application.Dtos.OrderItemDtos;
using KafeAPI.Application.Dtos.ResponseDtos;
using KafeAPI.Application.Interfaces;
using KafeAPI.Application.Services.Abstract;
using KafeAPI.Domain.Entities;

namespace KafeAPI.Application.Services.Concrete
{
    public class OrderItemService : IOrderItemService
    {
        private readonly IGenericRepository<OrderItem> _genericOrderItemRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateOrderItemDto> _createValidator;
        private readonly IValidator<UpdateOrderItemDto> _updateValidator;
        public OrderItemService(IGenericRepository<OrderItem> genericOrderItemRepository, IMapper mapper, IValidator<CreateOrderItemDto> createValidator, IValidator<UpdateOrderItemDto> updateValidator)
        {
            _genericOrderItemRepository = genericOrderItemRepository;
            _mapper = mapper;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
        }

        public async Task<ResponseDto<List<ResultOrderItemDto>>> GetAllOrderItems()
        {
            try
            {
                var orderItems = await _genericOrderItemRepository.GetAllAsync();
                if (orderItems.Count == 0)
                {
                    return new ResponseDto<List<ResultOrderItemDto>>
                    {
                        Success = false,
                        Data = null,
                        Message = "Siparişler bulunamadı",
                        ErrorCode = ErrorCodes.NotFound
                    };
                }
                var result = _mapper.Map<List<ResultOrderItemDto>>(orderItems);
                return new ResponseDto<List<ResultOrderItemDto>>
                {
                    Success = true,
                    Data = result
                };
            }
            catch (Exception ex)
            {
                return new ResponseDto<List<ResultOrderItemDto>>
                {
                    Success = false,
                    Data = null,
                    Message = "Bir hata oluştu",
                    ErrorCode = ErrorCodes.Exception
                };
            }
        }

        public async Task<ResponseDto<DetailOrderItemDto>> GetOrderItemById(int id)
        {
            try
            {
                var orderItem = await _genericOrderItemRepository.GetByIdAsync(id);
                if (orderItem == null)
                {
                    return new ResponseDto<DetailOrderItemDto>
                    {
                        Success = false,
                        Data = null,
                        Message = "Sipariş itemi bulunamadı",
                        ErrorCode = ErrorCodes.NotFound
                    };
                }
                var result = _mapper.Map<DetailOrderItemDto>(orderItem);
                return new ResponseDto<DetailOrderItemDto>
                {
                    Success = true,
                    Data = result
                };
            }
            catch (Exception ex)
            {
                return new ResponseDto<DetailOrderItemDto>
                {
                    Success = false,
                    Data = null,
                    Message = "Bir hata oluştu",
                    ErrorCode = ErrorCodes.Exception
                };
            }
        }

        public async Task<ResponseDto<object>> AddOrderItem(CreateOrderItemDto dto)
        {
            try
            {
                var validate = await _createValidator.ValidateAsync(dto);
                if (!validate.IsValid)
                {
                    return new ResponseDto<object>
                    {
                        Success = false,
                        Data = null,
                        Message = string.Join("|",validate.Errors.Select(x=>x.ErrorMessage)),
                        ErrorCode = ErrorCodes.ValidationError,
                    };
                }

                var result = _mapper.Map<OrderItem>(dto);
                await _genericOrderItemRepository.AddAsync(result);

                return new ResponseDto<object>
                {
                    Success = true,
                    Data = null,
                    Message = "Sipariş itemi başarıyla oluşturuldu"
                };
            }
            catch (Exception ex)
            {
                return new ResponseDto<object>
                {
                    Success = false,
                    Data = null,
                    Message = "Bir hata oluştu",
                    ErrorCode = ErrorCodes.Exception
                };
            }
        }
        public async Task<ResponseDto<object>> UpdateOrderItem(UpdateOrderItemDto dto)
        {
            try
            {
                var validate = await _updateValidator.ValidateAsync(dto);
                if (!validate.IsValid)
                {
                    return new ResponseDto<object>
                    {
                        Success = false,
                        Data = null,
                        Message = string.Join("|", validate.Errors.Select(x => x.ErrorMessage)),
                        ErrorCode = ErrorCodes.ValidationError,
                    };
                }
                var orderItem = await _genericOrderItemRepository.GetByIdAsync(dto.Id);
                if (orderItem is null)
                {
                    return new ResponseDto<object>
                    {
                        Success = false,
                        Data = null,
                        Message = "Sipariş itemi bulunamadı",
                        ErrorCode = ErrorCodes.NotFound
                    };
                }
                var result = _mapper.Map(dto, orderItem);
                await _genericOrderItemRepository.UpdateAsync(result);

                return new ResponseDto<object>
                {
                    Success = true,
                    Data = null,
                };

            }
            catch (Exception ex)
            {
                return new ResponseDto<object>
                {
                    Success = false,
                    Data = null,
                    Message = "Bir hata oluştu",
                    ErrorCode = ErrorCodes.Exception
                };
            }
        }

        public async Task<ResponseDto<object>> DeleteOrderItem(int id)
        {
            try
            {
                var orderItem = await _genericOrderItemRepository.GetByIdAsync(id);
                if (orderItem is null)
                {
                    return new ResponseDto<object>
                    {
                        Success = false,
                        Data = null,
                        Message = "Sipariş itemi bulunamadı",
                        ErrorCode = ErrorCodes.NotFound
                    };
                }
                await _genericOrderItemRepository.DeleteAsync(orderItem);
                return new ResponseDto<object>
                {
                    Success = true,
                    Data = null,
                    Message= "Sipariş itemi başarıyla silindi"
                };
            }
            catch (Exception ex)
            {
                return new ResponseDto<object>
                {
                    Success = false,
                    Data = null,
                    Message = "Bir hata oluştu",
                    ErrorCode = ErrorCodes.Exception
                };
            }
        }

    }
}
