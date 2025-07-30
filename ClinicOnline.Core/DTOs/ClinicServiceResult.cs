namespace ClinicOnline.Core.DTOs;

public class ClinicServiceResult
{
    public bool Susscess { get; set; }
    public object? Data { get; set; }
    public List<String> Errors { get; set; } = new List<String>();
}
