using KafeAPI.Application.Dtos.TableDtos;
using KafeAPI.Application.Dtos.ResponseDtos;
using KafeAPI.Application.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace KafeAPI.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TableController : ControllerBase
    {
        private readonly ITableService _tableService;

        public TableController(ITableService tableService)
        {
            _tableService = tableService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllTables()
        {
            var result = await _tableService.GetAllTables();
            if (!result.Success)
            {
                if (result.ErrorCodes == ErrorCodes.NotFound)
                    return Ok(result);

                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdTable(int id)
        {
            var result = await _tableService.GetByIdTable(id);
            if (!result.Success)
            {
                if (result.ErrorCodes == ErrorCodes.NotFound)
                    return Ok(result);
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpGet("getbytablenumber/{TableNumber}")]
        public async Task<IActionResult> GetByTableNumber(int TableNumber)
        {
            var result = await _tableService.GetByTableNumber(TableNumber);
            if (!result.Success)
            {
                if (result.ErrorCodes == ErrorCodes.NotFound)
                    return Ok(result);
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpPost]
        public async Task<IActionResult> AddTable([FromBody] CreateTableDto dto)
        {
            var result = await _tableService.AddTable(dto);
            if (!result.Success)
            {
                if (result.ErrorCodes is ErrorCodes.ValidationError or ErrorCodes.DuplicateError)
                {
                    return Ok(result);
                }
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateTable([FromBody] UpdateTableDto dto)
        {
            var result = await _tableService.UpdateTable(dto);
            if (!result.Success)
            {

                if (result.ErrorCodes is ErrorCodes.ValidationError or ErrorCodes.NotFound)
                    return Ok(result);

                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteTable(int id)
        {
            var result = await _tableService.DeleteTable(id);
            if (!result.Success)
            {
                if (result.ErrorCodes == ErrorCodes.NotFound)
                    return Ok(result);
                return BadRequest(result);
            }
            return Ok(result);
        }
    }
}
