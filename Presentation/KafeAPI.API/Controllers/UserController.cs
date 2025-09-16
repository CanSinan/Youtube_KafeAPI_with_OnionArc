using KafeAPI.Application.Dtos.UserDtos;
using KafeAPI.Application.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KafeAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterDto dto)
        {
            var result = await _userService.Register(dto);
            return CreateResponse(result);
        }
        [HttpPost("createRole")]
        public async Task<IActionResult> CreateRole(string roleName)
        {
            var result = await _userService.CreateRole(roleName);
            return CreateResponse(result);
        }
        [HttpPost("addRoleToUser")]
        public async Task<IActionResult> AddRoleToUser(string email, string role)
        {
            var result = await _userService.AddRoleToUser(email, role);
            return CreateResponse(result);
        }

    }
}
