namespace ClinicOnline.Core.Entities;

/// <summary>
/// bảng lưu các bình luận về bài viết
/// </summary>
public partial class ArticleComment : BaseEntity
{
    /// <summary>
    /// Bài viết nào
    /// </summary>
    public Guid ArticleId { get; set; }

    /// <summary>
    /// Ai bình luận
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Nội dung bình luận
    /// </summary>
    public string Comment { get; set; } = null!;

    /// <summary>
    /// Lưu Id của bình luận gốc nếu đây là trả lời (reply)
    /// </summary>
    public Guid? ParentCommentId { get; set; }

    public virtual Article Article { get; set; } = null!;

    public virtual ICollection<ArticleComment> InverseParentComment { get; set; } = new List<ArticleComment>();

    public virtual ArticleComment? ParentComment { get; set; }

    public virtual User User { get; set; } = null!;
}
