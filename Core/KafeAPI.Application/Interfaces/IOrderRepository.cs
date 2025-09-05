using KafeAPI.Domain.Entities;

namespace KafeAPI.Application.Interfaces
{
    public interface IOrderRepository
    {
        Task<List<Order>> GetAllOrderWithDetailAsync();
        Task<Order> GetOrderByIdWithDetailAsync(int id);
    }
}
