using KafeAPI.Application.Dtos.UserDtos;
using KafeAPI.Application.Services.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace KafeAPI.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : BaseController
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("generateToken")]
        public async Task<IActionResult> GenerateToken(LoginDto dto)
        {
            var result = await _authService.GenerateToken(dto);
            return CreateResponse(result);
        }
    }
}
