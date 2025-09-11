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
    public class MenuItemService : IMenuItemService
    {
        private readonly IGenericRepository<MenuItem> _genericMenuItemRepository;
        private readonly IGenericRepository<Category> _genericCategoryRepository;
        private readonly IMapper _mapper;
        private readonly IValidator<CreateMenuItemDto> _createValidator;
        private readonly IValidator<UpdateMenuItemDto> _updateValidator;
        public MenuItemService(IGenericRepository<MenuItem> genericRepository, IMapper mapper, IValidator<CreateMenuItemDto> createValidator, IValidator<UpdateMenuItemDto> updateValidator, IGenericRepository<Category> genericCategoryRepository)
        {
            _genericMenuItemRepository = genericRepository;
            _mapper = mapper;
            _createValidator = createValidator;
            _updateValidator = updateValidator;
            _genericCategoryRepository = genericCategoryRepository;
        }

        public async Task<ResponseDto<List<ResultMenuItemDto>>> GetAllMenuItems()
        {
            try
            {
                var menuItems = await _genericMenuItemRepository.GetAllAsync();
                var category = await _genericCategoryRepository.GetAllAsync();
                if (menuItems.Count == 0)
                {
                    return new ResponseDto<List<ResultMenuItemDto>>
                    {
                        Success = false,
                        Data = null,
                        ErrorCode = ErrorCodes.NotFound,
                        Message = "Menu Items bulunamadı"
                    };
                }
                var result = _mapper.Map<List<ResultMenuItemDto>>(menuItems);
                return new ResponseDto<List<ResultMenuItemDto>>
                {
                    Success = true,
                    Data = result,
                };
            }
            catch (Exception ex)
            {
                return new ResponseDto<List<ResultMenuItemDto>>
                {
                    Success = false,
                    Data = null,
                    ErrorCode = ErrorCodes.Exception,
                    Message = "Bir hata oluştu."
                };
            }
        }

        public async Task<ResponseDto<DetailMenuItemDto>> GetByIdMenuItem(int id)
        {
            try
            {
                var menuItem = await _genericMenuItemRepository.GetByIdAsync(id);
                if (menuItem is null)
                {
                    return new ResponseDto<DetailMenuItemDto>
                    {
                        Success = false,
                        Data = null,
                        ErrorCode = ErrorCodes.NotFound,
                        Message = "Menu Item bulunamadı"
                    };
                }
                var category = await _genericCategoryRepository.GetByIdAsync(menuItem.CategoryId);

                var result = _mapper.Map<DetailMenuItemDto>(menuItem);
                return new ResponseDto<DetailMenuItemDto>
                {
                    Success = true,
                    Data = result,
                };
            }
            catch (Exception ex)
            {
                return new ResponseDto<DetailMenuItemDto>
                {
                    Success = false,
                    ErrorCode = ErrorCodes.Exception,
                    Message = "Bir hata oluştu"
                };
            }
        }
        public async Task<ResponseDto<object>> AddMenuItem(CreateMenuItemDto dto)
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
                var chechkCategory = await _genericCategoryRepository.GetByIdAsync(dto.CategoryId);
                if (chechkCategory is null)
                {
                    return new ResponseDto<object>
                    {
                        Success = false,
                        Data = dto,
                        Message = "İlgili kategori bulunamadı",
                        ErrorCode = ErrorCodes.NotFound
                    };
                }
                var menuItem = _mapper.Map<MenuItem>(dto);
                await _genericMenuItemRepository.AddAsync(menuItem);
                return new ResponseDto<object>
                {
                    Success = true,
                    Data = null,
                    Message = "Menu Item oluşturuldu"
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

        public async Task<ResponseDto<object>> UpdateMenuItem(UpdateMenuItemDto dto)
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
                var menuItem = await _genericMenuItemRepository.GetByIdAsync(dto.Id);
                if (menuItem is null)
                {
                    return new ResponseDto<object>
                    {
                        Success = false,
                        Data = null,
                        ErrorCode = ErrorCodes.NotFound,
                        Message = "Menu Item bulunamadı"
                    };
                }
                var chechkCategory = await _genericCategoryRepository.GetByIdAsync(dto.CategoryId);
                if (chechkCategory is null)
                {
                    return new ResponseDto<object>
                    {
                        Success = false,
                        Data = dto,
                        Message = "İlgili kategori bulunamadı",
                        ErrorCode = ErrorCodes.NotFound
                    };
                }
                var menuItemMap = _mapper.Map(dto, menuItem);

                await _genericMenuItemRepository.UpdateAsync(menuItemMap);
                return new ResponseDto<object>
                {
                    Success = true,
                    Data = null,
                    Message = "Menu Item güncellendi"
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

        public async Task<ResponseDto<object>> DeleteMenuItem(int id)
        {
            try
            {
                var menuItem = await _genericMenuItemRepository.GetByIdAsync(id);
                if (menuItem is null)
                {
                    return new ResponseDto<object>
                    {
                        Success = false,
                        Data = null,
                        ErrorCode = ErrorCodes.NotFound,
                        Message = "Menu Item bulunamadı"
                    };
                }
                await _genericMenuItemRepository.DeleteAsync(menuItem);
                return new ResponseDto<object>
                {
                    Success = true,
                    Data = null,
                    Message = "Menu Item silindi"
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
