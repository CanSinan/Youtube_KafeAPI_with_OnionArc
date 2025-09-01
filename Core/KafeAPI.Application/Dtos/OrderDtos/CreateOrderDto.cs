using KafeAPI.Application.Dtos.OrderItemDtos;

namespace KafeAPI.Application.Dtos.OrderDtos
{
    public class CreateOrderDto
    {
        public int TableId { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdateAt { get; set; }
        public string Status { get; set; }
        public List<CreateOrderItemDto> OrderItems { get; set; }
    }
}
