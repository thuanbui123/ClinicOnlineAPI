namespace ClinicOnline.Core.Entities;

/// <summary>
/// Hồ sơ khám bệnh
/// </summary>
public partial class MedicalRecord
{
    public Guid Id { get; set; }

    /// <summary>
    /// Gắn với cuộc hẹn tương ứng
    /// </summary>
    public Guid AppointmentId { get; set; }

    public Guid PatientId { get; set; }

    public Guid DoctorId { get; set; }

    /// <summary>
    /// Chẩn đoán bệnh chính
    /// </summary>
    public string Diagnosis { get; set; } = null!;

    /// <summary>
    /// Triệu chứng mô tả
    /// </summary>
    public string? Symptoms { get; set; }

    /// <summary>
    /// Kế hoạch điều trị hoặc tư vấn
    /// </summary>
    public string? TreatmentPlan { get; set; }

    /// <summary>
    /// Ngày tái khám (nếu có)
    /// </summary>
    public DateOnly? FollowUpDate { get; set; }

    public bool? IsDeleted { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdateBy { get; set; }

    public virtual Appointment Appointment { get; set; } = null!;

    public virtual Doctor Doctor { get; set; } = null!;

    public virtual Patient Patient { get; set; } = null!;
}
