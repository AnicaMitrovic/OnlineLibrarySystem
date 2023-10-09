﻿using Microsoft.AspNetCore.Mvc;
using OnlineLibrary.Application.Interfaces;
using OnlineLibrary.Domain.Entities.Dtos.Request;
using OnlineLibrary.Infrastructure.Interfaces;

namespace OnlineLibrary.Api.Controllers
{
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("Login")]
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
