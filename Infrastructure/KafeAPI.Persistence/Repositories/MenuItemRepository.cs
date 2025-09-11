using KafeAPI.Application.Interfaces;
using KafeAPI.Domain.Entities;
using KafeAPI.Persistence.AppContext;
using Microsoft.EntityFrameworkCore;

namespace KafeAPI.Persistence.Repositories
{
    public class MenuItemRepository : IMenuItemRepository
    {
        private readonly AppDbContext _context;

        public MenuItemRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<MenuItem>> GetMenuItemsByCategoryIdAsync(int categoryId)
        {
            var menuItems = await _context.MenuItems.Where(x => x.CategoryId == categoryId).ToListAsync();
            return menuItems;
        }
    }
}
