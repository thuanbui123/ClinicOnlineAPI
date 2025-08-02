namespace ClinicOnline.Core.Entities;

/// <summary>
/// Tồn kho thuốc
/// </summary>
public partial class Stock : BaseEntity
{
    public Guid MedicineId { get; set; }

    /// <summary>
    /// Số lượng tồn kho hiện tại
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Vị trí kho hoặc chi nhánh lưu thuốc
    /// </summary>
    public string? Location { get; set; }

    public virtual Medicine Medicine { get; set; } = null!;
}
