namespace ClinicOnline.Core.Entities;

/// <summary>
/// Ghi nhận gửi thông báo
/// </summary>
public partial class Notification : BaseEntity
{
    /// <summary>
    /// Ai nhận thông báo
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Email hoặc Web
    /// </summary>
    public string? Type { get; set; }

    /// <summary>
    /// Nội dung tin nhắn/email
    /// </summary>
    public string? Content { get; set; }

    /// <summary>
    /// Thời điểm gửi
    /// </summary>
    public DateTime? SentAt { get; set; }

    public virtual User User { get; set; } = null!;
}
