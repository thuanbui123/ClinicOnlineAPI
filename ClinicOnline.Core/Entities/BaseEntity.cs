namespace ClinicOnline.Core.Entities;

public class BaseEntity
{
    public Guid Id { get; set; }
    public bool? IsDeleted { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdateBy { get; set; }
}
