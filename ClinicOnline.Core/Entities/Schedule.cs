namespace ClinicOnline.Core.Entities;

/// <summary>
/// Lịch làm việc bác sĩ
/// </summary>
public partial class Schedule : BaseEntity
{
    public Guid DoctorId { get; set; }

    /// <summary>
    /// Ngày làm việc
    /// </summary>
    public DateOnly WorkDate { get; set; }

    /// <summary>
    /// Khung giờ cụ thể (ví dụ: &quot;08:00-08:30&quot;)
    /// </summary>
    public string TimeSlot { get; set; } = null!;

    /// <summary>
    /// Còn trống không? Nếu false là đã có người đặt
    /// </summary>
    public bool? IsAvailable { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    public virtual Doctor Doctor { get; set; } = null!;
}
