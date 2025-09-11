using KafeAPI.Application.Dtos.OrderDtos;
using KafeAPI.Application.Dtos.OrderItemDtos;
using KafeAPI.Application.Dtos.ResponseDtos;

namespace KafeAPI.Application.Services.Abstract
{
    public interface IOrderService
    {
        Task<ResponseDto<List<ResultOrderDto>>> GetAllOrder();
        Task<ResponseDto<List<ResultOrderDto>>> GetAllOrderWithDetail();
        Task<ResponseDto<DetailOrderDto>> GetOrderById(int id);
        Task<ResponseDto<object>> AddOrder(CreateOrderDto dto);
        Task<ResponseDto<object>> UpdateOrder(UpdateOrderDto dto);
        Task<ResponseDto<object>> UpdateOrderStatusHazir(int orderId);
        Task<ResponseDto<object>> UpdateOrderStatusTeslimEdildi(int orderId);
        Task<ResponseDto<object>> UpdateOrderStatusIptalEdildi(int orderId);
        Task<ResponseDto<object>> UpdateOrderStatusOdendi(int orderId);
        Task<ResponseDto<object>> DeleteOrder(int id);
        //Task<ResponseDto<object>> AddOrderItemByOrderId(AddOrderItemByOrderDto dto);
    }
}
