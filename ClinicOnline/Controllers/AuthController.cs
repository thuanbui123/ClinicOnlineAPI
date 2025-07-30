using ClinicOnline.Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using static ClinicOnline.Core.DTOs.LoginDTO;

namespace ClinicOnline.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    /// <summary>
    /// API đăng nhập và lấy token JWT
    /// </summary>
    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginRequest request)
    {
        var result = await _authService.LoginAsync(request);
        return Ok(result);
    }
}
