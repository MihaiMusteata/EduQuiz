using Authentication.Application.DTOs.Auth;
using Authentication.Application.Services.Auth;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.API.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController(IAuthService authService) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto dto)
    {
        var response = await authService.RegisterAsync(dto);
        return response.Success ? Ok(response.Data) : BadRequest(response.Message);
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        var response = await authService.LoginAsync(dto);
        return response.Success ? Ok(response.Data) : Unauthorized(response.Message);
    }
}