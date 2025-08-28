using KafeAPI.Application.Dtos.MenuItemDtos;
using KafeAPI.Application.Dtos.ResponseDtos;

namespace KafeAPI.Application.Services.Abstract
{
    public interface IMenuItemService
    {
        Task<ResponseDto<List<ResultMenuItemDto>>> GetAllMenuItems();
        Task<ResponseDto<DetailMenuItemDto>> GetByIdMenuItem(int id);
        Task<ResponseDto<object>> AddMenuItem(CreateMenuItemDto dto);
        Task<ResponseDto<object>> UpdateMenuItem(UpdateMenuItemDto dto);
        Task<ResponseDto<object>> DeleteMenuItem(int id);
    }
}
