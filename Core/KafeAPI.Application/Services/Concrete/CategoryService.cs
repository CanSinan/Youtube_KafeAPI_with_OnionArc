using AutoMapper;
using KafeAPI.Application.Dtos.CategoryDtos;
using KafeAPI.Application.Dtos.ResponseDtos;
using KafeAPI.Application.Interfaces;
using KafeAPI.Application.Services.Abstract;
using KafeAPI.Domain.Entities;

namespace KafeAPI.Application.Services.Concrete
{
    public class CategoryService : ICategoryService
    {
        private readonly IGenericRepository<Category> _genericRepository;
        private readonly IMapper _mapper;
        public CategoryService(IGenericRepository<Category> genericRepository, IMapper mapper)
        {
            _genericRepository = genericRepository;
            _mapper = mapper;
        }

        public async Task<ResponseDto<List<ResultCategoryDto>>> GetAllCategories()
        {
            try
            {
                var categories = await _genericRepository.GetAllAsync();
                if (categories.Count == 0)
                {
                    return new ResponseDto<List<ResultCategoryDto>>
                    {
                        Success = false,
                        ErrorCodes = ErrorCodes.NotFound,
                        Message = "Kategori bulunamadı"
                    };
                }
                var result = _mapper.Map<List<ResultCategoryDto>>(categories);
                return new ResponseDto<List<ResultCategoryDto>>
                {
                    Success = true,
                    Data = result,
                };
            }
            catch (Exception ex)
            {

                return new ResponseDto<List<ResultCategoryDto>>
                {
                    Success = false,
                    ErrorCodes = ErrorCodes.Exception,
                    Message = "Bir hata oluştu"
                };
            }

        }

        public async Task<ResponseDto<DetailCategoryDto>> GetByIdCategory(int id)
        {
            try
            {
                var category = await _genericRepository.GetByIdAsync(id);
                if (category is null)
                {
                    return new ResponseDto<DetailCategoryDto>
                    {
                        Success = false,
                        ErrorCodes = ErrorCodes.NotFound,
                        Message = "Kategori bulunamadı"
                    };
                }
                var result = _mapper.Map<DetailCategoryDto>(category);
                return new ResponseDto<DetailCategoryDto>
                {
                    Success = true,
                    Data = result
                };
            }
            catch (Exception ex)
            {
                return new ResponseDto<DetailCategoryDto>
                {
                    Success = false,
                    ErrorCodes = ErrorCodes.Exception,
                    Message = "Bir hata oluştu"
                };
            }

        }

        public async Task<ResponseDto<object>> UpdateCategory(UpdateCategoryDto dto)
        {
            try
            {
                var category = await _genericRepository.GetByIdAsync(dto.Id);
                if (category is null)
                {
                    return new ResponseDto<object>
                    {
                        Success = false,
                        Data = null,
                        ErrorCodes = ErrorCodes.NotFound,
                        Message = "Kategori bulunamadı"
                    };
                }
                var categoryMap = _mapper.Map(dto, category);

                await _genericRepository.UpdateAsync(categoryMap);
                return new ResponseDto<object>
                {
                    Success = true,
                    Data = null,
                    Message = "Kategori güncellendi"
                };
            }
            catch (Exception ex)
            {
                return new ResponseDto<object>
                {
                    Success = false,
                    Data = null,
                    ErrorCodes = ErrorCodes.Exception,
                    Message = "Bir hata oluştu"
                };
            }

        }

        public async Task<ResponseDto<object>> AddCategory(CreateCategoryDto dto)
        {
            try
            {
                var category = _mapper.Map<Category>(dto);
                await _genericRepository.AddAsync(category);
                return new ResponseDto<object>
                {
                    Success = true,
                    Data = null,
                    Message = "Kategori oluşturuldu"
                };
            }
            catch (Exception ex)
            {
                return new ResponseDto<object>
                {
                    Success = false,
                    Data = null,
                    ErrorCodes = ErrorCodes.Exception,
                    Message = "Bir hata oluştu"
                };
            }

        }

        public async Task<ResponseDto<object>> DeleteCategory(int id)
        {
            try
            {
                var category = await _genericRepository.GetByIdAsync(id);
                if (category is null)
                {
                    return new ResponseDto<object>
                    {
                        Success = false,
                        Data = null,
                        ErrorCodes = ErrorCodes.NotFound,
                        Message = "Kategori bulunamadı"
                    };
                }
                await _genericRepository.DeleteAsync(category);
                return new ResponseDto<object>
                {
                    Success = true,
                    Data = null,
                    Message = "Kategori silindi"
                };
            }
            catch (Exception ex)
            {
                return new ResponseDto<object>
                {
                    Success = false,
                    Data = null,
                    ErrorCodes = ErrorCodes.Exception,
                    Message = "Bir hata oluştu"
                };
            }

        }
    }
}
