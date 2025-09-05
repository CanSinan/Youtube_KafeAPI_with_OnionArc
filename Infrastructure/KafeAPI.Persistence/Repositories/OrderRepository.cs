using KafeAPI.Application.Dtos.OrderDtos;
using KafeAPI.Application.Dtos.ResponseDtos;
using KafeAPI.Application.Interfaces;
using KafeAPI.Application.Services.Abstract;
using KafeAPI.Domain.Entities;
using KafeAPI.Persistence.AppContext;
using Microsoft.EntityFrameworkCore;

namespace KafeAPI.Persistence.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AppDbContext _context;

        public OrderRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Order>> GetAllOrderWithDetailAsync()
        {
            var result = await _context.Orders
                .Include(x => x.OrderItems)
                .ThenInclude(x => x.MenuItem)
                .ThenInclude(x => x.Category)
                .ToListAsync();

            return result;
        }
        public async Task<Order> GetOrderByIdWithDetailAsync(int id)
        {
            var result = await _context.Orders.Where(x => x.Id == id)
                .Include(x => x.OrderItems)
                .ThenInclude(x => x.MenuItem)
                .ThenInclude(x => x.Category).FirstOrDefaultAsync();

            return result;
        }
    }
}
