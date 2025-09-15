using KafeAPI.Application.Services.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace KafeAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : BaseController
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("generateToken")]
        public async Task<IActionResult> GenerateToken(string email)
        {
            var result = await _authService.GenerateToken(email);
            return CreateResponse(result);
        }
    }
}
