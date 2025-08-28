using KafeAPI.Application.Dtos.MenuItemDtos;
using KafeAPI.Application.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KafeAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MenuItemController : ControllerBase
    {
        private readonly IMenuItemService _menuItemService;

        public MenuItemController(IMenuItemService menuItemService)
        {
            _menuItemService = menuItemService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var result = await _menuItemService.GetAllMenuItems();
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdMenuItem(int id)
        {
            var result = await _menuItemService.GetByIdMenuItem(id);
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> AddMenuItem([FromBody] CreateMenuItemDto dto)
        {
            await _menuItemService.AddMenuItem(dto);
            return Ok("Menü Item Oluşturuldu");
        }
        [HttpPut]
        public async Task<IActionResult> UpdateMenuItem([FromBody] UpdateMenuItemDto dto)
        {
            await _menuItemService.UpdateMenuItem(dto);
            return Ok("Menü Item Güncellendi");
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteMenuItem(int id)
        {
            await _menuItemService.DeleteMenuItem(id);
            return Ok("Menü Item Silindi");
        }
    }
}
