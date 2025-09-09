using KafeAPI.Application.Dtos.OrderItemDtos;
using KafeAPI.Application.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace KafeAPI.API.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class OrderItemController : BaseController
    {
        private readonly IOrderItemService _orderItemService;

        public OrderItemController(IOrderItemService orderItemService)
        {
            _orderItemService = orderItemService;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllOrderItems()
        {
            var result = await _orderItemService.GetAllOrderItems();
            return CreateResponse(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdOrderItem(int id)
        {
            var result = await _orderItemService.GetOrderItemById(id);
            return CreateResponse(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddOrderItem([FromBody] CreateOrderItemDto dto)
        {
            var result = await _orderItemService.AddOrderItem(dto);
            return CreateResponse(result);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateOrderItem([FromBody] UpdateOrderItemDto dto)
        {
            var result = await _orderItemService.UpdateOrderItem(dto);
            return CreateResponse(result);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteOrderItem(int id)
        {
            var result = await _orderItemService.DeleteOrderItem(id);
            return CreateResponse(result);
        }
    }
}
