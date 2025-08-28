using KafeAPI.Application.Dtos.CategoryDtos;
using KafeAPI.Application.Dtos.ResponseDtos;
using KafeAPI.Application.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KafeAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            var result = await _categoryService.GetAllCategories();
            if (!result.Success)
            {
                if (result.ErrorCodes == ErrorCodes.NotFound)
                    return Ok(result);

                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdCategory(int id)
        {
            var result = await _categoryService.GetByIdCategory(id);

            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> AddCategory([FromBody] CreateCategoryDto dto)
        {
            await _categoryService.AddCategory(dto);
            return Ok("Kategori Oluşturuldu");
        }
        [HttpPut]
        public async Task<IActionResult> UpdateCategory([FromBody] UpdateCategoryDto dto)
        {
            await _categoryService.UpdateCategory(dto);
            return Ok("Kategori Güncellendi");
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            await _categoryService.DeleteCategory(id);
            return Ok("Kategori Silindi");
        }
    }
}
