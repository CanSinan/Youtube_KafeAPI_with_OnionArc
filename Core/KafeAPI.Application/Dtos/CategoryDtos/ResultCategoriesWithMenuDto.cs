using KafeAPI.Application.Dtos.MenuItemDtos;

namespace KafeAPI.Application.Dtos.CategoryDtos
{
    public class ResultCategoriesWithMenuDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<CategoriesMenuItemDto> MenuItems { get; set; }
    }
}
