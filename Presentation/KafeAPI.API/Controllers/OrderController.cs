using KafeAPI.Application.Dtos.OrderDtos;
using KafeAPI.Application.Services.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KafeAPI.API.Controllers
{
    [Route("api/order")]
    [ApiController]
    public class OrderController : BaseController
    {
        private readonly IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [Authorize(Roles = "admin,employe")]
        [HttpGet]
        public async Task<IActionResult> GetAllOrders()
        {
            var result = await _orderService.GetAllOrder();
            return CreateResponse(result);
        }
        [Authorize(Roles = "admin,employe")]
        [HttpGet("withdeatils")]
        public async Task<IActionResult> GetAllOrderWithDetail()
        {
            var result = await _orderService.GetAllOrderWithDetail();
            return CreateResponse(result);
        }
        [Authorize(Roles = "admin,employe")]
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdOrder(int id)
        {
            var result = await _orderService.GetOrderById(id);
            return CreateResponse(result);
        }

        [Authorize(Roles = "admin,employe")]
        [HttpPost]
        public async Task<IActionResult> AddOrder([FromBody] CreateOrderDto dto)
        {
            var result = await _orderService.AddOrder(dto);
            return CreateResponse(result);
        }
        //[HttpPut("addOrderItemByOrder")]
        //public async Task<IActionResult> AddOrderItemByOrder([FromBody] AddOrderItemByOrderDto dto)
        //{
        //    var result = await _orderService.AddOrderItemByOrderId(dto);
        //    return CreateResponse(result);
        //}

        [Authorize(Roles = "admin,employe")]
        [HttpPut]
        public async Task<IActionResult> UpdateOrder([FromBody] UpdateOrderDto dto)
        {
            var result = await _orderService.UpdateOrder(dto);
            return CreateResponse(result);
        }
        [Authorize(Roles = "admin,employe")]
        [HttpPut("status/hazir")]
        public async Task<IActionResult> UpdateOrderStatusHazır(int orderId)
        {
            var result = await _orderService.UpdateOrderStatusHazir(orderId);
            return CreateResponse(result);
        }
        [Authorize(Roles = "admin,employe")]
        [HttpPut("status/teslimedildi")]
        public async Task<IActionResult> UpdateOrderStatusTeslimEdildi(int orderId)
        {
            var result = await _orderService.UpdateOrderStatusTeslimEdildi(orderId);
            return CreateResponse(result);
        }
        [Authorize(Roles = "admin,employe")]
        [HttpPut("status/iptaledildi")]
        public async Task<IActionResult> UpdateOrderStatusIptalEdildi(int orderId)
        {
            var result = await _orderService.UpdateOrderStatusIptalEdildi(orderId);
            return CreateResponse(result);
        }
        [Authorize(Roles = "admin,employe")]
        [HttpPut("status/ödendi")]
        public async Task<IActionResult> UpdateOrderStatusOdendi(int orderId)
        {
            var result = await _orderService.UpdateOrderStatusOdendi(orderId);
            return CreateResponse(result);
        }

        [Authorize(Roles = "admin,employe")]
        [HttpDelete]
        public async Task<IActionResult> DeleteOrder(int id)
        {
            var result = await _orderService.DeleteOrder(id);
            return CreateResponse(result);
        }
    }
}
