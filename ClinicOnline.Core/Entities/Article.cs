namespace ClinicOnline.Core.Entities;

/// <summary>
/// Bài viết tư vấn
/// </summary>
public partial class Article
{
    public Guid Id { get; set; }

    public Guid DoctorId { get; set; }

    /// <summary>
    /// Tiêu đề bài viết
    /// </summary>
    public string Title { get; set; } = null!;

    /// <summary>
    /// Nội dung chính
    /// </summary>
    public string Content { get; set; } = null!;

    public bool? IsDeleted { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdateBy { get; set; }

    public virtual ICollection<ArticleComment> ArticleComments { get; set; } = new List<ArticleComment>();

    public virtual Doctor Doctor { get; set; } = null!;
}
