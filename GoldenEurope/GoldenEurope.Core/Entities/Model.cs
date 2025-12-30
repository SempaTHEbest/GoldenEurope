namespace GoldenEurope.Core.Entities;

public class Model :  BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty; // it's about motocycle or car
    public Guid BrandId { get; set; }
    public Brand?  Brand { get; set; }

    public ICollection<Car> Cars { get; set; } = new List<Car>();
}