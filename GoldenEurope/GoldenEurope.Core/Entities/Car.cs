using GoldenEurope.Core.Enums;

namespace GoldenEurope.Core.Entities;

public class Car : BaseEntity
{
    public string Vin { get; set; } = string.Empty;
    public int Year { get; set; }
    public int Mileage { get; set; }
    public decimal Price { get; set; }
    
    //technical info (enums)
    public FuelType Fuel { get; set; }
    public TransmissionType Transmission { get; set; }
    public BodyType Body { get; set; }
    public DrivetrainType Drivetrain { get; set; }
    
    public decimal EngineVolume { get; set; }
    public int HorsePower { get; set; }
    
    //appearance
    public string Color { get; set; } = string.Empty;
    public CarCondition Condition { get; set; }
    
    //Description
    public string Description { get; set; } = string.Empty;
    public bool IsSold { get; set; } = false;
    
    public Guid ModelId { get; set; }
    public Model? Model { get; set; }

    //For number
    public string SellerPhone { get; set; } = string.Empty;
    public int PhoneViewCount {get; set; } = 0;

}