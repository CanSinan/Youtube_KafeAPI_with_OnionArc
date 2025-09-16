using KafeAPI.Application.Dtos.CategoryDtos;
using KafeAPI.Application.Dtos.ResponseDtos;
using KafeAPI.Application.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KafeAPI.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoryController : BaseController
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
            return CreateResponse(result);
        }

        [HttpGet("getAllCategoriesWithMenuItems")]
        public async Task<IActionResult> GetAllCategoriesWithMenuItems()
        {
            var result = await _categoryService.GetCategoriesWithMenuItems();
            return CreateResponse(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdCategory(int id)
        {
            var result = await _categoryService.GetByIdCategory(id);
            return CreateResponse(result);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<IActionResult> AddCategory([FromBody] CreateCategoryDto dto)
        {
            var result = await _categoryService.AddCategory(dto);
            return CreateResponse(result);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateCategory([FromBody] UpdateCategoryDto dto)
        {
            var result = await _categoryService.UpdateCategory(dto);
            return CreateResponse(result);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var result = await _categoryService.DeleteCategory(id);
            return CreateResponse(result);
        }
    }
}
