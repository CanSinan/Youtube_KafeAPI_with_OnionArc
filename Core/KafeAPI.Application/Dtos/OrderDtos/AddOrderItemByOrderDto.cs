using KafeAPI.Application.Dtos.OrderItemDtos;

namespace KafeAPI.Application.Dtos.OrderDtos
{
    public class AddOrderItemByOrderDto
    {
        public int OrderId { get; set; }
        public CreateOrderItemDto OrderItem { get; set; }
    }
}
