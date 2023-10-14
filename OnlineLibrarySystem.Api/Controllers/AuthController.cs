using Microsoft.AspNetCore.Mvc;
using OnlineLibrary.Application.Interfaces;
using OnlineLibrary.Domain.Entities.Dtos.Request;

namespace OnlineLibrary.Api.Controllers
{
    [Route("api/v1/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] UserLoginRequestDto request)
        {
            if (request is null)
            {
                return BadRequest(request);
            }

            var response = await _authService.Login(request.Username, request.Password);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }
    }
}
