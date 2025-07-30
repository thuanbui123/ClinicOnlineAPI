namespace ClinicOnline.Core.DTOs;

/// <summary>
/// Cấu hình JWT dùng để sinh và xác thực token.
/// Được bind từ appsettings.json.
/// </summary>
public class JwtSettings
{
    /// <summary>
    /// Khóa bí mật để mã hóa JWT token
    /// </summary>
    public string Key { get; set; } = string.Empty;

    /// <summary>
    /// Người phát hành token (issuer)
    /// </summary>
    public string Issuer { get; set; } = string.Empty;

    /// <summary>
    /// Đối tượng chấp nhận token (audience)
    /// </summary>
    public string Audience { get; set; } = string.Empty;

    /// <summary>
    /// Thời gian sống của token (tính bằng phút)
    /// </summary>
    public int DurationInMinutes { get; set; }
}
