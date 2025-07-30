namespace ClinicOnline.Core.DTOs;

public class LoginDTO
{
    public partial class LoginRequest
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }

    public partial class LoginResponse
    {
        public string Token { get; set; } = string.Empty;
    }
}
