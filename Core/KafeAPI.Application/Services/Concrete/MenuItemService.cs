using KafeAPI.Application.Dtos.MenuItemDtos;
using KafeAPI.Application.Interfaces;
using KafeAPI.Application.Services.Abstract;
using KafeAPI.Domain.Entities;

namespace KafeAPI.Application.Services.Concrete
{
    public class MenuItemService : IMenuItemService
    {
        private readonly IGenericRepository<MenuItem> _genericRepository;

        public MenuItemService(IGenericRepository<MenuItem> genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public Task AddMenuItem(CreateMenuItemDto dto)
        {
            throw new NotImplementedException();
        }

        public Task DeleteMenuItem(int id)
        {
            throw new NotImplementedException();
        }

        public Task<List<ResultMenuItemDto>> GetAllCategories()
        {
            throw new NotImplementedException();
        }

        public Task<DetailMenuItemDto> GetByIdMenuItem(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateMenuItem(UpdateMenuItemDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
