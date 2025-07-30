using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClinicOnline.API.Controllers;

/// <summary>
/// Controller chứa các API cần xác thực hoặc phân quyền
/// </summary>
[Route("api/[controller]")]
[ApiController]
public class SecureController : ControllerBase
{
    /// <summary>
    /// API yêu cầu người dùng phải đăng nhập
    /// </summary>
    [Authorize]
    [HttpGet("any")]
    public IActionResult AnyUser() => Ok("✅ Người dùng đã xác thực");

    /// <summary>
    /// API chỉ cho phép người có Role = Admin
    /// </summary>
    [Authorize(Roles = "Admin")]
    [HttpGet("admin")]
    public IActionResult AdminOnly() => Ok("🔒 Chỉ Admin mới truy cập được");

    /// <summary>
    /// API áp dụng chính sách phân quyền theo claim (Policy)
    /// </summary>
    [Authorize(Policy = "DoctorOnly")]
    [HttpGet("doctor")]
    public IActionResult DoctorOnly() => Ok("🔒 Chỉ Doctor được phép truy cập");
}
