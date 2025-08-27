using KafeAPI.Application.Interfaces;
using KafeAPI.Persistence.AppContext;
using Microsoft.EntityFrameworkCore;

namespace KafeAPI.Persistence.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly AppDbContext _context;
        private readonly DbSet<T> _table;
        public GenericRepository(AppDbContext context)
        {
            _context = context;
            _table = _context.Set<T>();
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _table.ToListAsync();            
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _table.FindAsync(id);
        }
        public async Task AddAsync(T entity)
        {
            await _table.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(T entity)
        {
            _table.Update(entity);  
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(T entity)
        {
            _table.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}
