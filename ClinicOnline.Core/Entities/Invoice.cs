namespace ClinicOnline.Core.Entities;

/// <summary>
/// Hóa đơn
/// </summary>
public partial class Invoice
{
    public Guid Id { get; set; }

    /// <summary>
    /// Liên kết lịch khám
    /// </summary>
    public Guid AppointmentId { get; set; }

    /// <summary>
    /// Ai thanh toán
    /// </summary>
    public Guid PatientId { get; set; }

    /// <summary>
    /// Số tiền cần trả
    /// </summary>
    public decimal? Amount { get; set; }

    /// <summary>
    /// Pending, Paid, Failed
    /// </summary>
    public string? Status { get; set; }

    /// <summary>
    /// MoMo, ZaloPay, Tiền mặt…
    /// </summary>
    public string? PaymentMethod { get; set; }

    public bool? IsDeleted { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdateBy { get; set; }

    public virtual Appointment Appointment { get; set; } = null!;

    public virtual Patient Patient { get; set; } = null!;
}
