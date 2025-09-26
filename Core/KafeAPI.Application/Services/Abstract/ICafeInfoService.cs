using KafeAPI.Application.Dtos.CafeInfoDtos;
using KafeAPI.Application.Dtos.ResponseDtos;

namespace KafeAPI.Application.Services.Abstract
{
    public interface ICafeInfoService
    {
        Task<ResponseDto<List<ResultCafeInfoDto>>> GetAllCafeInfo();
        Task<ResponseDto<DetailCafeInfoDto>> GetByIdCafeInfo(int id);
        Task<ResponseDto<object>> AddCafeInfo(CreateCafeInfoDto dto);
        Task<ResponseDto<object>> UpdateCafeInfo(UpdateCafeInfoDto dto);
        Task<ResponseDto<object>> DeleteCafeInfo(int id);
    }
}
