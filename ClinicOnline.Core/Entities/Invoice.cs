namespace ClinicOnline.Core.Entities;

/// <summary>
/// Hóa đơn
/// </summary>
public partial class Invoice : BaseEntity
{
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

    public virtual Appointment Appointment { get; set; } = null!;

    public virtual Patient Patient { get; set; } = null!;
}
