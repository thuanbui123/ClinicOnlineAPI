using static ClinicOnline.Core.DTOs.LoginDTO;

namespace ClinicOnline.Core.Interfaces;

public interface IAuthService
{
    /// <summary>
    /// Xác thực thông tin đăng nhập và trả về JWT token nếu hợp lệ.
    /// </summary>
    /// <param name="request">Thông tin đăng nhập</param>
    /// <returns>Token JWT hợp lệ</returns>
    Task<LoginResponse> LoginAsync(LoginRequest request);
}
