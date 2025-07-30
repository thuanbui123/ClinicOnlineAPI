namespace ClinicOnline.Core.Entities;

/// <summary>
/// Lịch sử nhập/xuất thuốc
/// </summary>
public partial class StockTransaction
{
    public Guid Id { get; set; }

    public Guid MedicineId { get; set; }

    /// <summary>
    /// Import, Export, Use – loại giao dịch
    /// </summary>
    public string Type { get; set; } = null!;

    /// <summary>
    /// Số lượng thay đổi
    /// </summary>
    public int Quantity { get; set; }

    /// <summary>
    /// Lý do (kê đơn, hỏng, hết hạn…)
    /// </summary>
    public string? Reason { get; set; }

    public bool? IsDeleted { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdateBy { get; set; }

    public virtual Medicine Medicine { get; set; } = null!;
}
