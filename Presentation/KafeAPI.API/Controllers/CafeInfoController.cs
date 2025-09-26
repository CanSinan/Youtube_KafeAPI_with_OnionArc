using KafeAPI.Application.Dtos.CafeInfoDtos;
using KafeAPI.Application.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace KafeAPI.API.Controllers
{
    [Route("api/cafeinfo")]
    [ApiController]
    public class CafeInfoController : BaseController
    {
        private readonly ICafeInfoService _cafeInfoService;

        public CafeInfoController(ICafeInfoService cafeInfoService)
        {
            _cafeInfoService = cafeInfoService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCafeInfos()
        {
            var result = await _cafeInfoService.GetAllCafeInfo();
            return CreateResponse(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdCafeInfo(int id)
        {
            var result = await _cafeInfoService.GetByIdCafeInfo(id);
            return CreateResponse(result);
        }
        [HttpPost]
        public async Task<IActionResult> AddCafeInfo(CreateCafeInfoDto dto)
        {
            var result = await _cafeInfoService.AddCafeInfo(dto);
            return CreateResponse(result);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateCafeInfo(UpdateCafeInfoDto dto)
        {
            var result = await _cafeInfoService.UpdateCafeInfo(dto);
            return CreateResponse(result);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCafeInfo(int id)
        {
            var result = await _cafeInfoService.DeleteCafeInfo(id);
            return CreateResponse(result);
        }
    }
}
