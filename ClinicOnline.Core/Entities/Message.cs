namespace ClinicOnline.Core.Entities;

/// <summary>
/// Tin nhắn giữa người dùng
/// </summary>
public partial class Message : BaseEntity
{
    /// <summary>
    /// Người gửi
    /// </summary>
    public Guid SenderId { get; set; }

    /// <summary>
    /// Người nhận
    /// </summary>
    public Guid ReceiverId { get; set; }

    /// <summary>
    /// Nội dung tin nhắn
    /// </summary>
    public string Content { get; set; } = null!;

    /// <summary>
    /// Thời điểm gửi
    /// </summary>
    public DateTime? SentAt { get; set; }

    /// <summary>
    /// Đã đọc hay chưa
    /// </summary>
    public bool? IsRead { get; set; }

    public virtual User Receiver { get; set; } = null!;

    public virtual User Sender { get; set; } = null!;
}
