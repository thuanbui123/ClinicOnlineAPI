using ClinicOnline.Core.DTOs;
using ClinicOnline.Core.Enums;
using ClinicOnline.Core.Interfaces;
using Microsoft.Extensions.Options;
using static ClinicOnline.Core.DTOs.LoginDTO;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace ClinicOnline.Core.Services;

public class AuthService : IAuthService
{
    private readonly JwtSettings _jwtSettings;

    public AuthService(IOptions<JwtSettings> jwtOptions)
    {
        _jwtSettings = jwtOptions.Value;
    }

    public async Task<LoginResponse> LoginAsync(LoginRequest request) 
    { 
        if (request.Username == "admin" && request.Password == "123")
        {
            // Khai báo các claims lưu trong token
            var claims = new[]
            {
                new Claim(ClaimTypes.Name, request.Username),
                new Claim(ClaimTypes.Role, Roles.Admin.ToString()),
            };

            // Tạo signing key từ cấu hình
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            // Tạo JWT token
            var token = new JwtSecurityToken(
                _jwtSettings.Issuer,
                _jwtSettings.Audience,
                claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                signingCredentials: creds
            );

            return new LoginResponse
            {
                Token = new JwtSecurityTokenHandler().WriteToken(token)
            };
        }

        throw new UnauthorizedAccessException("Thông tin đăng nhập không hợp lệ");
    }
}
