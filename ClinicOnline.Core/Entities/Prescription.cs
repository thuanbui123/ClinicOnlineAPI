namespace ClinicOnline.Core.Entities;

/// <summary>
/// Đơn thuốc
/// </summary>
public partial class Prescription : BaseEntity
{
    /// <summary>
    /// Gắn với lịch hẹn khám
    /// </summary>
    public Guid AppointmentId { get; set; }

    /// <summary>
    /// Ghi chú đơn thuốc
    /// </summary>
    public string? DoctorNote { get; set; }

    public virtual Appointment Appointment { get; set; } = null!;

    public virtual ICollection<PrescriptionItem> PrescriptionItems { get; set; } = new List<PrescriptionItem>();
}
