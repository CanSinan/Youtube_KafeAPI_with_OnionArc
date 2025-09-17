using KafeAPI.Application.Dtos.TableDtos;
using KafeAPI.Application.Dtos.ResponseDtos;
using KafeAPI.Application.Services.Abstract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace KafeAPI.API.Controllers
{
    [Route("api/table")]
    [ApiController]
    public class TableController : BaseController
    {
        private readonly ITableService _tableService;

        public TableController(ITableService tableService)
        {
            _tableService = tableService;
        }
        [Authorize(Roles = "admin,employe")]
        [HttpGet]
        public async Task<IActionResult> GetAllTables()
        {
            var result = await _tableService.GetAllTables();
            return CreateResponse(result);
        }
        [Authorize(Roles = "admin,employe")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdTable(int id)
        {
            var result = await _tableService.GetByIdTable(id);
            return CreateResponse(result);
        }
        [Authorize(Roles = "admin,employe")]
        [HttpGet("tablenumber")]
        public async Task<IActionResult> GetByTableNumber([FromQuery]int TableNumber)
        {
            var result = await _tableService.GetByTableNumber(TableNumber);
            return CreateResponse(result);
        }
        //[Authorize(Roles = "admin,employe")]
        //[HttpGet("isactivetablesgeneric")]
        //public async Task<IActionResult> GetAllActiveTablesGeneric()
        //{
        //    var result = await _tableService.GetAllActiveTablesGeneric();
        //    return CreateResponse(result);
        //}
        [Authorize(Roles = "admin,employe")]
        [HttpGet("activetables")]
        public async Task<IActionResult> GetAllActiveTables()
        {
            var result = await _tableService.GetAllActiveTables();
            return CreateResponse(result);
        }
        [Authorize(Roles = "admin,employe")]
        [HttpPost]
        public async Task<IActionResult> AddTable([FromBody] CreateTableDto dto)
        {
            var result = await _tableService.AddTable(dto);
            return CreateResponse(result);
        }
        [Authorize(Roles = "admin,employe")]
        [HttpPut]
        public async Task<IActionResult> UpdateTable([FromBody] UpdateTableDto dto)
        {
            var result = await _tableService.UpdateTable(dto);
            return CreateResponse(result);
        }
        [Authorize(Roles = "admin,employe")]
        [HttpPut("statusbyid/{id}")]
        public async Task<IActionResult> UpdateTableStatusById(int id)
        {
            var result = await _tableService.UpdateTableStatusById(id);
            return CreateResponse(result);
        }
        [Authorize(Roles = "admin,employe")]
        [HttpPut("statusbytublenumber/{tableNumber}")]
        public async Task<IActionResult> UpdateTableStatusByTableNumber(int tableNumber)
        {
            var result = await _tableService.UpdateTableStatusByTableNumber(tableNumber);
            return CreateResponse(result);
        }
        [Authorize(Roles = "admin,employe")]
        [HttpDelete]
        public async Task<IActionResult> DeleteTable(int id)
        {
            var result = await _tableService.DeleteTable(id);
            return CreateResponse(result);
        }
    }
}
