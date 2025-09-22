using KafeAPI.Application.Dtos.CategoryDtos;
using KafeAPI.Application.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KafeAPI.API.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoryController : BaseController
    {
        private readonly ICategoryService _categoryService;
        private readonly Serilog.ILogger _log;
        public CategoryController(ICategoryService categoryService, Serilog.ILogger log)
        {
            _categoryService = categoryService;
            _log = log;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllCategories()
        {
            _log.Information("get-categories");
            var result = await _categoryService.GetAllCategories();
            _log.Information("iget-categories: " + result.Success);
            _log.Warning("wget-categories: " + result.Success);
            _log.Error("eget-categories: " + result.Success);
            _log.Debug("dget-categories: " + result.Success);
            return CreateResponse(result);
        }

        [HttpGet("withmenuitems")]
        public async Task<IActionResult> GetAllCategoriesWithMenuItems()
        {
            var result = await _categoryService.GetCategoriesWithMenuItems();
            return CreateResponse(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdCategory([FromRoute]int id)
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

        [Authorize(Roles = "admin")]
        [HttpPut]
        public async Task<IActionResult> UpdateCategory([FromBody] UpdateCategoryDto dto)
        {
            var result = await _categoryService.UpdateCategory(dto);
            return CreateResponse(result);
        }

        [Authorize(Roles = "admin")]
        [HttpDelete]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var result = await _categoryService.DeleteCategory(id);
            return CreateResponse(result);
        }
    }
}
