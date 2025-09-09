using KafeAPI.Application.Dtos.OrderItemDtos;
using KafeAPI.Application.Dtos.ResponseDtos;

namespace KafeAPI.Application.Services.Abstract
{
    public interface IOrderItemService
    {
        Task<ResponseDto<List<ResultOrderItemDto>>> GetAllOrderItems();
        Task<ResponseDto<DetailOrderItemDto>> GetOrderItemById(int id);
        Task<ResponseDto<object>> AddOrderItem(CreateOrderItemDto dto);
        Task<ResponseDto<object>> UpdateOrderItem(UpdateOrderItemDto dto);
        Task<ResponseDto<object>> DeleteOrderItem(int id);
    }
}
