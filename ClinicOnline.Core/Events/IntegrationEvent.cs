namespace ClinicOnline.Core.Events;

/// <summary>
/// Lớp cơ sở cho các sự kiện tích hợp (integration event) dùng để giao tiếp giữa các microservice.
/// </summary>
public abstract class IntegrationEvent
{
    /// <summary>
    /// ID duy nhất của sự kiện.
    /// </summary>
    public Guid Id { get; private set; } = Guid.NewGuid();

    /// <summary>
    /// Thời gian tạo ra sự kiện.
    /// </summary>
    public DateTime CreatedAt { get; private set; } = DateTime.UtcNow;
}
