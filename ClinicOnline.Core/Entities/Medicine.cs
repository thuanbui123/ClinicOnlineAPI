namespace ClinicOnline.Core.Entities;

/// <summary>
/// Danh mục thuốc
/// </summary>
public partial class Medicine : BaseEntity
{
    /// <summary>
    /// Tên thuốc
    /// </summary>
    public string Name { get; set; } = null!;

    /// <summary>
    /// Đơn vị thuốc
    /// </summary>
    public string? Unit { get; set; }

    /// <summary>
    /// Mô tả công dụng
    /// </summary>
    public string? Description { get; set; }

    /// <summary>
    /// Ngày hết hạn sử dụng
    /// </summary>
    public DateOnly? ExpiryDate { get; set; }

    public virtual ICollection<PrescriptionItem> PrescriptionItems { get; set; } = new List<PrescriptionItem>();

    public virtual ICollection<StockTransaction> StockTransactions { get; set; } = new List<StockTransaction>();

    public virtual ICollection<Stock> Stocks { get; set; } = new List<Stock>();
}
