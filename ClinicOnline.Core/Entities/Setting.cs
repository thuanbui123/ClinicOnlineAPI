namespace ClinicOnline.Core.Entities;

public class Setting : BaseEntity
{
    public int Type { get; set; }
    public int Keys { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Value { get; set; } = string.Empty;
}
