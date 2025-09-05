using KafeAPI.Application.Dtos.OrderDtos;
using KafeAPI.Application.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace KafeAPI.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderController : BaseController
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var result = await _orderService.GetAllOrder();
            return CreateResponse(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdOrder(int id)
        {
            var result = await _orderService.GetOrderById(id);
            return CreateResponse(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddOrder([FromBody] CreateOrderDto dto)
        {
            var result = await _orderService.CreateOrder(dto);
            return CreateResponse(result);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateOrder([FromBody] UpdateOrderDto dto)
        {
            var result = await _orderService.UpdateOrder(dto);
            return CreateResponse(result);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var result = await _orderService.DeleteOrder(id);
            return CreateResponse(result);
        }
    }
}
