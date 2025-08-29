using KafeAPI.Application.Interfaces;
using KafeAPI.Domain.Entities;
using KafeAPI.Persistence.AppContext;
using Microsoft.EntityFrameworkCore;

namespace KafeAPI.Persistence.Repositories
{
    public class TableRepository : ITableRepository
    {
        private readonly AppDbContext _context;
        private DbSet<Table> _table;
        public TableRepository(AppDbContext context)
        {
            _context = context;
            _table = _context.Set<Table>();
        }

        public async Task<Table> GetByTableNumberAsync(int tableNumber)
        {
            var table = await _table.FirstOrDefaultAsync(t => t.TableNumber == tableNumber);
            return table;
        }
    }
}
