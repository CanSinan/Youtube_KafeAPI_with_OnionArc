using AutoMapper;
using KafeAPI.Application.Dtos.MenuItemDtos;
using KafeAPI.Application.Interfaces;
using KafeAPI.Application.Services.Abstract;
using KafeAPI.Domain.Entities;

namespace KafeAPI.Application.Services.Concrete
{
    public class MenuItemService : IMenuItemService
    {
        private readonly IGenericRepository<MenuItem> _genericRepository;
        private readonly IMapper _mapper;

        public MenuItemService(IGenericRepository<MenuItem> genericRepository, IMapper mapper)
        {
            _genericRepository = genericRepository;
            _mapper = mapper;
        }

        public async Task<List<ResultMenuItemDto>> GetAllMenuItems()
        {
            var menuItems = await _genericRepository.GetAllAsync();
            var result = _mapper.Map<List<ResultMenuItemDto>>(menuItems);
            return result;
        }

        public async Task<DetailMenuItemDto> GetByIdMenuItem(int id)
        {
            var menuItem = await _genericRepository.GetByIdAsync(id);
            var result = _mapper.Map<DetailMenuItemDto>(menuItem);
            return result;
        }
        public async Task AddMenuItem(CreateMenuItemDto dto)
        {
            var menuItem = _mapper.Map<MenuItem>(dto);
            await _genericRepository.AddAsync(menuItem);
        }

        public async Task UpdateMenuItem(UpdateMenuItemDto dto)
        {
            var menuItem = _mapper.Map<MenuItem>(dto);
            await _genericRepository.UpdateAsync(menuItem);
        }

        public async Task DeleteMenuItem(int id)
        {
            var menuItem = await _genericRepository.GetByIdAsync(id);
            await _genericRepository.DeleteAsync(menuItem);
        }
    }
}
