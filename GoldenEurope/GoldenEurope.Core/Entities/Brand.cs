namespace GoldenEurope.Core.Entities;

public class Brand : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;

    public ICollection<Model> Models { get; set; } = new List<Model>();
}