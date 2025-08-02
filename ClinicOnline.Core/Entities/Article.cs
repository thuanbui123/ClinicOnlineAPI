namespace ClinicOnline.Core.Entities;

/// <summary>
/// Bài viết tư vấn
/// </summary>
public partial class Article : BaseEntity
{
    public Guid DoctorId { get; set; }

    /// <summary>
    /// Tiêu đề bài viết
    /// </summary>
    public string Title { get; set; } = null!;

    /// <summary>
    /// Nội dung chính
    /// </summary>
    public string Content { get; set; } = null!;

    public virtual ICollection<ArticleComment> ArticleComments { get; set; } = new List<ArticleComment>();

    public virtual Doctor Doctor { get; set; } = null!;
}
