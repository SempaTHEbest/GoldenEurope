namespace GoldenEurope.Core.Entities;

public class CarImage : BaseEntity
{
    public string Url { get; set; } = string.Empty;
    public string PublicId { get; set; } = string.Empty;
    
    public Guid CarId { get; set; }
    public Car? Car { get; set; }
}