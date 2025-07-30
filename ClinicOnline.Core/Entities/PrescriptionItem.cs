namespace ClinicOnline.Core.Entities;

/// <summary>
/// Các dòng thuốc trong đơn
/// </summary>
public partial class PrescriptionItem
{
    public Guid Id { get; set; }

    /// <summary>
    /// Gắn với đơn thuốc
    /// </summary>
    public Guid PrescriptionId { get; set; }

    public Guid MedicineId { get; set; }

    /// <summary>
    /// Liều lượng kê
    /// </summary>
    public int? Quantity { get; set; }

    /// <summary>
    /// Hướng dẫn dùng thuốc (uống 2 lần/ngày, sau ăn…)
    /// </summary>
    public string? Usage { get; set; }

    public bool? IsDeleted { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdateBy { get; set; }

    public virtual Medicine Medicine { get; set; } = null!;

    public virtual Prescription Prescription { get; set; } = null!;
}
