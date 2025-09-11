using AutoMapper;
using FluentValidation;
using KafeAPI.Application.Dtos.CategoryDtos;
using KafeAPI.Application.Dtos.MenuItemDtos;
using KafeAPI.Application.Dtos.ResponseDtos;
using KafeAPI.Application.Interfaces;
using KafeAPI.Application.Services.Abstract;
using KafeAPI.Domain.Entities;

namespace KafeAPI.Application.Services.Concrete
{
    public class CategoryService : ICategoryService
    {
        private readonly IGenericRepository<Category> _genericCategoryRepository;
        private readonly IMenuItemRepository _menuItemRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateCategoryDto> _createValidator;
        private readonly IValidator<UpdateCategoryDto> _updateValidator;
        public CategoryService(IGenericRepository<Category> genericRepository, IMapper mapper, IValidator<CreateCategoryDto> createValidator, IValidator<UpdateCategoryDto> updateValidator, IMenuItemRepository menuItemRepository)
        {
            _genericCategoryRepository = genericRepository;
            _mapper = mapper;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
            _menuItemRepository = menuItemRepository;
        }

        public async Task<ResponseDto<List<ResultCategoryDto>>> GetAllCategories()
        {
            try
            {
                var categories = await _genericCategoryRepository.GetAllAsync();
                if (categories.Count == 0)
                {
                    return new ResponseDto<List<ResultCategoryDto>>
                    {
                        Success = false,
                        ErrorCode = ErrorCodes.NotFound,
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
                    ErrorCode = ErrorCodes.Exception,
                    Message = "Bir hata oluştu"
                };
            }

        }
        public async Task<ResponseDto<List<ResultCategoriesWithMenuDto>>> GetCategoriesWithMenuItems()
        {
            try
            {
                var categories = await _genericCategoryRepository.GetAllAsync();
                if (categories.Count == 0)
                {
                    return new ResponseDto<List<ResultCategoriesWithMenuDto>>
                    {
                        Success = false,
                        ErrorCode = ErrorCodes.NotFound,
                        Message = "Kategori bulunamadı"
                    };
                }
                var result = _mapper.Map<List<ResultCategoriesWithMenuDto>>(categories);

                foreach (var item in result)
                {
                    var listMenuItems = await _menuItemRepository.GetMenuItemsByCategoryIdAsync(item.Id);
                    var newList = _mapper.Map<List<CategoriesMenuItemDto>>(listMenuItems);
                    item.MenuItems = newList;
                }

                return new ResponseDto<List<ResultCategoriesWithMenuDto>>
                {
                    Success = true,
                    Data = result,
                };
            }
            catch (Exception ex)
            {

                return new ResponseDto<List<ResultCategoriesWithMenuDto>>
                {
                    Success = false,
                    ErrorCode = ErrorCodes.Exception,
                    Message = "Bir hata oluştu"
                };
            }
        }

        public async Task<ResponseDto<DetailCategoryDto>> GetByIdCategory(int id)
        {
            try
            {
                var category = await _genericCategoryRepository.GetByIdAsync(id);
                if (category is null)
                {
                    return new ResponseDto<DetailCategoryDto>
                    {
                        Success = false,
                        Data = null,
                        ErrorCode = ErrorCodes.NotFound,
                        Message = "Kategori bulunamadı"
                    };
                }
                var menuItems = await _menuItemRepository.GetMenuItemsByCategoryIdAsync(id);
                var result = _mapper.Map<DetailCategoryDto>(category);
                var newList = _mapper.Map<List<CategoriesMenuItemDto>>(menuItems);
                result.MenuItems = newList;
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
                    ErrorCode = ErrorCodes.Exception,
                    Message = "Bir hata oluştu"
                };
            }

        }
        public async Task<ResponseDto<object>> AddCategory(CreateCategoryDto dto)
        {
            try
            {
                var validate = await _createValidator.ValidateAsync(dto);
                if (!validate.IsValid)
                {
                    return new ResponseDto<object>
                    {
                        Success = false,
                        Data = null,
                        ErrorCode = ErrorCodes.ValidationError,
                        Message = string.Join(" | ", validate.Errors.Select(e => e.ErrorMessage))
                    };
                }
                var category = _mapper.Map<Category>(dto);
                await _genericCategoryRepository.AddAsync(category);
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
                    ErrorCode = ErrorCodes.Exception,
                    Message = "Bir hata oluştu"
                };
            }
        }

        public async Task<ResponseDto<object>> UpdateCategory(UpdateCategoryDto dto)
        {
            try
            {
                var validate = await _updateValidator.ValidateAsync(dto);
                if (!validate.IsValid)
                {
                    return new ResponseDto<object>
                    {
                        Success = false,
                        Data = null,
                        ErrorCode = ErrorCodes.ValidationError,
                        Message = string.Join(" | ", validate.Errors.Select(e => e.ErrorMessage))
                    };
                }
                var category = await _genericCategoryRepository.GetByIdAsync(dto.Id);
                if (category is null)
                {
                    return new ResponseDto<object>
                    {
                        Success = false,
                        Data = null,
                        ErrorCode = ErrorCodes.NotFound,
                        Message = "Kategori bulunamadı"
                    };
                }
                var categoryMap = _mapper.Map(dto, category);

                await _genericCategoryRepository.UpdateAsync(categoryMap);
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
                    ErrorCode = ErrorCodes.Exception,
                    Message = "Bir hata oluştu"
                };
            }

        }


        public async Task<ResponseDto<object>> DeleteCategory(int id)
        {
            try
            {
                var category = await _genericCategoryRepository.GetByIdAsync(id);
                if (category is null)
                {
                    return new ResponseDto<object>
                    {
                        Success = false,
                        Data = null,
                        ErrorCode = ErrorCodes.NotFound,
                        Message = "Kategori bulunamadı"
                    };
                }
                await _genericCategoryRepository.DeleteAsync(category);
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
                    ErrorCode = ErrorCodes.Exception,
                    Message = "Bir hata oluştu"
                };
            }

        }

     
    }
}
