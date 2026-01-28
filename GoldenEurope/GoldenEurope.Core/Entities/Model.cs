using GoldenEurope.Core.Enums;

namespace GoldenEurope.Core.Entities;

public class Model :  BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public VehicleCategory  Category { get; set; }
    public Guid BrandId { get; set; }
    public Brand?  Brand { get; set; }

    public ICollection<Car> Cars { get; set; } = new List<Car>();
}