using KafeAPI.Application.Dtos.OrderItemDtos;

namespace KafeAPI.Application.Dtos.OrderDtos
{
    public class UpdateOrderDto
    {
        public int Id { get; set; }
        public int TableId { get; set; }
        //public decimal TotalPrice { get; set; }
        //public DateTime CreatedAt { get; set; }
        //public DateTime? UpdateAt { get; set; }
        //public string Status { get; set; }
        public List<UpdateOrderItemDto> OrderItems { get; set; }
    }
}
