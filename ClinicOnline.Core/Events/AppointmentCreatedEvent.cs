namespace ClinicOnline.Core.Events;

/// <summary>
/// Sự kiện được tạo ra khi một cuộc hẹn mới được tạo.
/// </summary>
public class AppointmentCreatedEvent : IntegrationEvent
{
    /// <summary>
    /// ID cuộc hẹn vừa được tạo.
    /// </summary>
    public Guid AppointmentId { get; set; }

    /// <summary>
    /// ID của bệnh nhân đã đặt cuộc hẹn.
    /// </summary>
    public Guid PatientId { get; set; }

    /// <summary>
    /// ID của bác sĩ được chỉ định (nếu có).
    /// </summary>
    public Guid? DoctorId { get; set; }

    /// <summary>
    /// Thời gian bắt đầu cuộc hẹn.
    /// </summary>
    public DateTime Time { get; set; }

    /// <summary>
    /// Ghi chú đi kèm (nếu có).
    /// </summary>
    public string? Note { get; set; }

    /// <summary>
    /// Trạng thái ban đầu của cuộc hẹn (Pending, Confirmed, etc.).
    /// </summary>
    public string Status { get; set; } = "Pending";
}
