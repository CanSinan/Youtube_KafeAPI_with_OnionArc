using AutoMapper;
using FluentValidation;
using KafeAPI.Application.Dtos.OrderDtos;
using KafeAPI.Application.Dtos.ResponseDtos;
using KafeAPI.Application.Interfaces;
using KafeAPI.Application.Services.Abstract;
using KafeAPI.Domain.Entities;
using System.Net.Http.Headers;

namespace KafeAPI.Application.Services.Concrete
{
    public class OrderService : IOrderService
    {
        private readonly IGenericRepository<Order> _genericOrderRepository;
        private readonly IGenericRepository<OrderItem> _genericOrderItemRepository;
        private readonly IGenericRepository<MenuItem> _genericMenuItemRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateOrderDto> _createOrderValidator;
        private readonly IValidator<UpdateOrderDto> _updateOrderValidator;
        public OrderService(IGenericRepository<Order> genericOrderRepository, IMapper mapper, IValidator<CreateOrderDto> createOrderValidator, IValidator<UpdateOrderDto> updateOrderValidator, IGenericRepository<OrderItem> genericOrderItemRepository, IOrderRepository orderRepository, IGenericRepository<MenuItem> genericMenuItemRepository)
        {
            _genericOrderRepository = genericOrderRepository;
            _mapper = mapper;
            _createOrderValidator = createOrderValidator;
            _updateOrderValidator = updateOrderValidator;
            _genericOrderItemRepository = genericOrderItemRepository;
            _orderRepository = orderRepository;
            _genericMenuItemRepository = genericMenuItemRepository;
        }
        public async Task<ResponseDto<List<ResultOrderDto>>> GetAllOrder()
        {
            try
            {
                var orders = await _genericOrderRepository.GetAllAsync();
                var orderItem = await _genericOrderItemRepository.GetAllAsync();
                if (orders.Count == 0)
                {
                    return new ResponseDto<List<ResultOrderDto>>
                    {
                        Success = false,
                        Data = null,
                        Message = "Sipariş bulunamadı.",
                        ErrorCode = ErrorCodes.NotFound
                    };
                }
                var result = _mapper.Map<List<ResultOrderDto>>(orders);
                return new ResponseDto<List<ResultOrderDto>>
                {
                    Success = true,
                    Data = result
                };
            }
            catch (Exception ex)
            {
                return new ResponseDto<List<ResultOrderDto>>
                {
                    Success = false,
                    Data = null,
                    Message = "Bir hata oluştu.",
                    ErrorCode = ErrorCodes.Exception
                };
            }
        }

        public async Task<ResponseDto<List<ResultOrderDto>>> GetAllOrderWithDetail()
        {
            try
            {
                var orders = await _orderRepository.GetAllOrderWithDetailAsync();
                if (orders.Count == 0)
                {
                    return new ResponseDto<List<ResultOrderDto>>
                    {
                        Success = false,
                        Data = null,
                        Message = "Sipariş bulunamadı.",
                        ErrorCode = ErrorCodes.NotFound
                    };
                }
                var result = _mapper.Map<List<ResultOrderDto>>(orders);
                return new ResponseDto<List<ResultOrderDto>>
                {
                    Success = true,
                    Data = result
                };
            }
            catch (Exception ex)
            {
                return new ResponseDto<List<ResultOrderDto>>
                {
                    Success = false,
                    Data = null,
                    Message = "Bir hata oluştu.",
                    ErrorCode = ErrorCodes.Exception
                };
            }
        }
        public async Task<ResponseDto<DetailOrderDto>> GetOrderById(int id)
        {
            try
            {
                var order = await _orderRepository.GetOrderByIdWithDetailAsync(id);
                if (order is null)
                {
                    return new ResponseDto<DetailOrderDto>
                    {
                        Success = false,
                        Data = null,
                        Message = "Sipariş bulunamadı.",
                        ErrorCode = ErrorCodes.NotFound
                    };
                }
                var result = _mapper.Map<DetailOrderDto>(order);
                return new ResponseDto<DetailOrderDto>
                {
                    Success = true,
                    Data = result
                };
            }

            catch (Exception ex)
            {
                return new ResponseDto<DetailOrderDto>
                {
                    Success = false,
                    Data = null,
                    Message = "Bir hata oluştu.",
                    ErrorCode = ErrorCodes.Exception
                };
            }
        }
        public async Task<ResponseDto<object>> CreateOrder(CreateOrderDto dto)
        {
            try
            {
                var validate = await _createOrderValidator.ValidateAsync(dto);
                if (!validate.IsValid)
                {
                    return new ResponseDto<object>
                    {
                        Success = false,
                        Data = null,
                        Message = string.Join("|", validate.Errors.Select(x => x.ErrorMessage)),
                        ErrorCode = ErrorCodes.ValidationError
                    };
                }
                var order = _mapper.Map<Order>(dto);

                order.Status = OrderStatus.Hazirlanıyor;
                order.CreatedAt = DateTime.Now;
                decimal totalPrice = 0;
                foreach (var item in order.OrderItems)
                {
                    item.MenuItem = await _genericMenuItemRepository.GetByIdAsync(item.MenuItemId);
                    item.Price = item.MenuItem.Price * item.Quantity;
                    totalPrice += item.Price;
                }
                order.TotalPrice = totalPrice;
                await _genericOrderRepository.AddAsync(order);

                return new ResponseDto<object>
                {
                    Success = true,
                    Data = null,
                    Message = "Sipariş başarıyla oluşturuldu."
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
        public async Task<ResponseDto<object>> UpdateOrder(UpdateOrderDto dto)
        {
            try
            {
                var validate = await _updateOrderValidator.ValidateAsync(dto);
                if (!validate.IsValid)
                {
                    return new ResponseDto<object>
                    {
                        Success = false,
                        Data = null,
                        Message = string.Join("|", validate.Errors.Select(x => x.ErrorMessage)),
                        ErrorCode = ErrorCodes.ValidationError
                    };
                }
                var order = await _genericOrderRepository.GetByIdAsync(dto.Id);
                if (order is null)
                {
                    return new ResponseDto<object>
                    {
                        Success = false,
                        Data = null,
                        Message = "Sipariş bulunamadı.",
                        ErrorCode = ErrorCodes.NotFound
                    };
                }
                var result = _mapper.Map(dto, order);
                await _genericOrderRepository.UpdateAsync(result);

                return new ResponseDto<object>
                {
                    Success = true,
                    Data = null,
                    Message = "Sipariş başarıyla güncellendi."
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
        public async Task<ResponseDto<object>> UpdateOrderStatusHazir(int orderId)
        {
            try
            {
                var order = await _genericOrderRepository.GetByIdAsync(orderId);
                if (order is null)
                {
                    return new ResponseDto<object>
                    {
                        Success = false,
                        Data = null,
                        Message = "Sipariş bulunamadı.",
                        ErrorCode = ErrorCodes.NotFound
                    };
                }
                order.Status = OrderStatus.Hazir;
                await _genericOrderRepository.UpdateAsync(order);

                return new ResponseDto<object>
                {
                    Success = true,
                    Data = null,
                    Message = "Sipariş durumu hazır olarak güncellendi."
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
        public async Task<ResponseDto<object>> UpdateOrderStatusTeslimEdildi(int orderId)
        {
            try
            {
                var order = await _genericOrderRepository.GetByIdAsync(orderId);
                if (order is null)
                {
                    return new ResponseDto<object>
                    {
                        Success = false,
                        Data = null,
                        Message = "Sipariş bulunamadı.",
                        ErrorCode = ErrorCodes.NotFound
                    };
                }
                order.Status = OrderStatus.TeslimEdildi;
                await _genericOrderRepository.UpdateAsync(order);

                return new ResponseDto<object>
                {
                    Success = true,
                    Data = null,
                    Message = "Sipariş durumu teslim edildi olarak güncellendi."
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

        public async Task<ResponseDto<object>> UpdateOrderStatusIptalEdildi(int orderId)
        {
            try
            {
                var order = await _genericOrderRepository.GetByIdAsync(orderId);
                if (order is null)
                {
                    return new ResponseDto<object>
                    {
                        Success = false,
                        Data = null,
                        Message = "Sipariş bulunamadı.",
                        ErrorCode = ErrorCodes.NotFound
                    };
                }
                order.Status = OrderStatus.IptalEdildi;
                await _genericOrderRepository.UpdateAsync(order);

                return new ResponseDto<object>
                {
                    Success = true,
                    Data = null,
                    Message = "Sipariş durumu iptal edildi olarak güncellendi."
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
        public async Task<ResponseDto<object>> DeleteOrder(int id)
        {
            try
            {
                var order = await _genericOrderRepository.GetByIdAsync(id);
                if (order is null)
                {
                    return new ResponseDto<object>
                    {
                        Success = false,
                        Data = null,
                        Message = "Sipariş bulunamadı.",
                        ErrorCode = ErrorCodes.NotFound
                    };
                }
                await _genericOrderRepository.DeleteAsync(order);
                return new ResponseDto<object>
                {
                    Success = true,
                    Data = null,
                    Message = "Sipariş başarıyla silindi."
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
