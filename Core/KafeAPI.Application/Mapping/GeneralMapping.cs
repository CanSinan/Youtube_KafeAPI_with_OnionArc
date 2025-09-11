using AutoMapper;
using KafeAPI.Application.Dtos.CategoryDtos;
using KafeAPI.Application.Dtos.MenuItemDtos;
using KafeAPI.Application.Dtos.OrderDtos;
using KafeAPI.Application.Dtos.OrderItemDtos;
using KafeAPI.Application.Dtos.TableDtos;
using KafeAPI.Domain.Entities;

namespace KafeAPI.Application.Mapping
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
            #region Category
            CreateMap<Category, ResultCategoryDto>().ReverseMap();
            CreateMap<Category, DetailCategoryDto>().ReverseMap();
            CreateMap<Category, CreateCategoryDto>().ReverseMap();
            CreateMap<Category, UpdateCategoryDto>().ReverseMap();
            CreateMap<Category, ResultCategoriesWithMenuDto>().ReverseMap();
            #endregion

            #region MenuItem
            CreateMap<MenuItem, ResultMenuItemDto>().ReverseMap();
            CreateMap<MenuItem, DetailMenuItemDto>().ReverseMap();
            CreateMap<MenuItem, CreateMenuItemDto>().ReverseMap();
            CreateMap<MenuItem, UpdateMenuItemDto>().ReverseMap();
            CreateMap<MenuItem, CategoriesMenuItemDto>().ReverseMap();
            #endregion

            #region Table
            CreateMap<Table, ResultTableDto>().ReverseMap();
            CreateMap<Table, DetailTableDto>().ReverseMap();
            CreateMap<Table, CreateTableDto>().ReverseMap();
            CreateMap<Table, UpdateTableDto>().ReverseMap();
            #endregion

            #region OrderItem
            CreateMap<OrderItem, ResultOrderItemDto>().ReverseMap();
            CreateMap<OrderItem, DetailOrderItemDto>().ReverseMap();
            CreateMap<OrderItem, CreateOrderItemDto>().ReverseMap();
            CreateMap<OrderItem, UpdateOrderItemDto>().ReverseMap();
            #endregion

            #region Order
            CreateMap<Order, ResultOrderDto>().ReverseMap();
            CreateMap<Order, DetailOrderDto>().ReverseMap();
            CreateMap<Order, CreateOrderDto>().ReverseMap();
            CreateMap<Order, UpdateOrderDto>().ReverseMap();
            #endregion
        }
    }
}
