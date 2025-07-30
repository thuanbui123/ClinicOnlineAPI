namespace ClinicOnline.Core.Entities;

/// <summary>
/// Đơn thuốc
/// </summary>
public partial class Prescription
{
    public Guid Id { get; set; }

    /// <summary>
    /// Gắn với lịch hẹn khám
    /// </summary>
    public Guid AppointmentId { get; set; }

    /// <summary>
    /// Ghi chú đơn thuốc
    /// </summary>
    public string? DoctorNote { get; set; }

    public bool? IsDeleted { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdateBy { get; set; }

    public virtual Appointment Appointment { get; set; } = null!;

    public virtual ICollection<PrescriptionItem> PrescriptionItems { get; set; } = new List<PrescriptionItem>();
}
