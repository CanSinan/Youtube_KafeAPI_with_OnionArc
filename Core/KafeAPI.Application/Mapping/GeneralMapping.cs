using AutoMapper;
using KafeAPI.Application.Dtos.CategoryDtos;
using KafeAPI.Application.Dtos.MenuItemDtos;
using KafeAPI.Application.Dtos.TableDtos;
using KafeAPI.Domain.Entities;

namespace KafeAPI.Application.Mapping
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
            #region Category
            CreateMap<Category, CreateCategoryDto>().ReverseMap();
            CreateMap<Category, DetailCategoryDto>().ReverseMap();
            CreateMap<Category, ResultCategoryDto>().ReverseMap();
            CreateMap<Category, UpdateCategoryDto>().ReverseMap();
            #endregion

            #region MenuItem
            CreateMap<MenuItem, CreateMenuItemDto>().ReverseMap();
            CreateMap<MenuItem, DetailMenuItemDto>().ReverseMap();
            CreateMap<MenuItem, ResultMenuItemDto>().ReverseMap();
            CreateMap<MenuItem, UpdateMenuItemDto>().ReverseMap();
            #endregion

            #region Table
            CreateMap<Table, CreateTableDto>().ReverseMap();
            CreateMap<Table, DetailTableDto>().ReverseMap();
            CreateMap<Table, ResultTableDto>().ReverseMap();
            CreateMap<Table, UpdateTableDto>().ReverseMap();
            #endregion
        }
    }
}
