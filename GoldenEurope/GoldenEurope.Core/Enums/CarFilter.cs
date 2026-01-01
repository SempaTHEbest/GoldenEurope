namespace GoldenEurope.Core.Enums;

public class CarFilter
{
    public Guid? BrandId { get; set; }
    public Guid? ModelId { get; set; }
    public CarCondition? Condition { get; set; }
    public int? YearFrom { get; set; }
    public int? YearTo { get; set; }
    public decimal? PriceFrom { get; set; }
    public decimal? PriceTo { get; set; }
    public FuelType? Fuel { get; set; }
    public TransmissionType? Transmission { get; set; }
    public BodyType? Body { get; set; }
    public DrivetrainType? Drivetrain { get; set; }
}