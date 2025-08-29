using KafeAPI.Application.Dtos.TableDtos;
using KafeAPI.Application.Dtos.ResponseDtos;
using KafeAPI.Application.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace KafeAPI.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class TableController : BaseController
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
            return CreateResponse(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdTable(int id)
        {
            var result = await _tableService.GetByIdTable(id);
            return CreateResponse(result);
        }
        [HttpGet("getbytablenumber/{TableNumber}")]
        public async Task<IActionResult> GetByTableNumber(int TableNumber)
        {
            var result = await _tableService.GetByTableNumber(TableNumber);
            return CreateResponse(result);
        }
        [HttpGet("getallactivetablesgeneric")]
        public async Task<IActionResult> GetAllActiveTablesGeneric()
        {
            var result = await _tableService.GetAllActiveTablesGeneric();
            return CreateResponse(result);
        }
        [HttpGet("getallactivetables")]
        public async Task<IActionResult> GetAllActiveTables()
        {
            var result = await _tableService.GetAllActiveTables();
            return CreateResponse(result);
        }
        [HttpPost]
        public async Task<IActionResult> AddTable([FromBody] CreateTableDto dto)
        {
            var result = await _tableService.AddTable(dto);
            return CreateResponse(result);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateTable([FromBody] UpdateTableDto dto)
        {
            var result = await _tableService.UpdateTable(dto);
            return CreateResponse(result);
        }
        [HttpPut("updatetablestatusbyid/{id}")]
        public async Task<IActionResult> UpdateTableStatusById(int id)
        {
            var result = await _tableService.UpdateTableStatusById(id);
            return CreateResponse(result);
        }
        [HttpPut("updatetablestatusbytublenumber/{tableNumber}")]
        public async Task<IActionResult> UpdateTableStatusByTableNumber(int tableNumber)
        {
            var result = await _tableService.UpdateTableStatusByTableNumber(tableNumber);
            return CreateResponse(result);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteTable(int id)
        {
            var result = await _tableService.DeleteTable(id);
            return CreateResponse(result);
        }
    }
}
