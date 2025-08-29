using KafeAPI.Application.Dtos.ResponseDtos;
using KafeAPI.Domain.Entities;

namespace KafeAPI.Application.Interfaces
{
    public interface ITableRepository
    {
        Task<Table> GetByTableNumberAsync(int tableNumber);
        Task<List<Table>> GetAllActiveTablesAsync();
    }
}
