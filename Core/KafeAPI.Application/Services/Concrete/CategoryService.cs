using KafeAPI.Application.Dtos.CategoryDtos;
using KafeAPI.Application.Interfaces;
using KafeAPI.Application.Services.Abstract;
using KafeAPI.Domain.Entities;

namespace KafeAPI.Application.Services.Concrete
{
    public class CategoryService : ICategoryService
    {
        private readonly IGenericRepository<Category> _genericRepository;

        public CategoryService(IGenericRepository<Category> genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public Task<List<ResultCategoryDto>> GetAllCategories()
        {
            throw new NotImplementedException();
        }

        public Task<DetailCategoryDto> GetByIdCategory(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateCategory(UpdateCategoryDto dto)
        {
            throw new NotImplementedException();
        }

        public Task AddCategory(CreateCategoryDto dto)
        {
            throw new NotImplementedException();
        }

        public Task DeleteCategory(int id)
        {
            throw new NotImplementedException();
        }
    }
}
