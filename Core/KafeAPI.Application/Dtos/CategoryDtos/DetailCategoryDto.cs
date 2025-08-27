using KafeAPI.Application.Dtos.MenuItemDtos;

namespace KafeAPI.Application.Dtos.CategoryDtos
{
    public class DetailCategoryDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<ResultMenuItemDto> MenuItems { get; set; } // ?
    }
}
