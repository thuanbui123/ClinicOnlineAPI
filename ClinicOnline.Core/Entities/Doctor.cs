namespace ClinicOnline.Core.Entities;

/// <summary>
/// Bảng lưu thông tin bác sĩ
/// </summary>
public partial class Doctor : BaseEntity
{
    public Guid UserId { get; set; }

    /// <summary>
    /// Chuyên khoa: Nội, Nhi, Tim mạch,…
    /// </summary>
    public string? Speciality { get; set; }

    /// <summary>
    /// Nơi làm việc chính
    /// </summary>
    public string? Hospital { get; set; }

    /// <summary>
    /// Học vị: Cử nhân, Thạc sĩ, Tiến sĩ,…
    /// </summary>
    public string? Degree { get; set; }

    /// <summary>
    /// Số năm kinh nghiệm hành nghề
    /// </summary>
    public int? ExperienceYears { get; set; }

    /// <summary>
    /// URL ảnh đại diện
    /// </summary>
    public string? ProfilePhoto { get; set; }

    /// <summary>
    /// Mô tả bản thân, kinh nghiệm,…
    /// </summary>
    public string? Bio { get; set; }

    /// <summary>
    /// URL file chứng chỉ, văn bằng
    /// </summary>
    public string? CertificateFile { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    public virtual ICollection<Article> Articles { get; set; } = new List<Article>();

    public virtual ICollection<MedicalRecord> MedicalRecords { get; set; } = new List<MedicalRecord>();

    public virtual ICollection<Schedule> Schedules { get; set; } = new List<Schedule>();

    public virtual User User { get; set; } = null!;
}
