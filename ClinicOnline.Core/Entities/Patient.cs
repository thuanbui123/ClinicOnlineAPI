namespace ClinicOnline.Core.Entities;

/// <summary>
/// Hồ sơ bệnh nhân
/// </summary>
public partial class Patient
{
    public Guid Id { get; set; }

    public Guid UserId { get; set; }

    /// <summary>
    /// Giới tính
    /// </summary>
    public string? Gender { get; set; }

    /// <summary>
    /// Ngày sinh
    /// </summary>
    public DateOnly? Dob { get; set; }

    /// <summary>
    /// Địa chỉ cư trú
    /// </summary>
    public string? Address { get; set; }

    /// <summary>
    /// Tiền sử bệnh án
    /// </summary>
    public string? MedicalHistory { get; set; }

    /// <summary>
    /// Mã bảo hiểm y tế nếu có
    /// </summary>
    public string? InsuranceNumber { get; set; }

    public bool? IsDeleted { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdateBy { get; set; }

    public virtual ICollection<Appointment> Appointments { get; set; } = new List<Appointment>();

    public virtual ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();

    public virtual ICollection<MedicalRecord> MedicalRecords { get; set; } = new List<MedicalRecord>();

    public virtual User User { get; set; } = null!;
}
