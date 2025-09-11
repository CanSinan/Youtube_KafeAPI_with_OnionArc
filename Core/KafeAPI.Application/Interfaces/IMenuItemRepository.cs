using KafeAPI.Domain.Entities;

namespace KafeAPI.Application.Interfaces
{
    public interface IMenuItemRepository
    {
        Task<List<MenuItem>> GetMenuItemsByCategoryIdAsync(int categoryId);
    }
}
