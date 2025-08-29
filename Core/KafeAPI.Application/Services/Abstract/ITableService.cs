using KafeAPI.Application.Dtos.ResponseDtos;
using KafeAPI.Application.Dtos.TableDtos;

namespace KafeAPI.Application.Services.Abstract
{
    public interface ITableService
    {
        Task<ResponseDto<List<ResultTableDto>>> GetAllTables();
        Task<ResponseDto<List<ResultTableDto>>> GetAllActiveTablesGeneric();
        Task<ResponseDto<List<ResultTableDto>>> GetAllActiveTables();
        Task<ResponseDto<DetailTableDto>> GetByIdTable(int id);
        Task<ResponseDto<DetailTableDto>> GetByTableNumber(int tableNumber);
        Task<ResponseDto<object>> AddTable(CreateTableDto dto);
        Task<ResponseDto<object>> UpdateTable(UpdateTableDto dto);
        Task<ResponseDto<object>> UpdateTableStatusByTableNumber(int tableNumber);
        Task<ResponseDto<object>> UpdateTableStatusById(int id);
        Task<ResponseDto<object>> DeleteTable(int id);
    }
}
