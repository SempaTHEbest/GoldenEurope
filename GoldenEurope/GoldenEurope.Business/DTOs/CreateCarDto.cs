using GoldenEurope.Core.Enums;

namespace GoldenEurope.Business.DTOs;

public record CreateCarDto(
    string Vin,
    Guid ModelId,
    CarCondition Condition,
    int Year,
    decimal Price,
    int Mileage,
    FuelType Fuel,
    TransmissionType Transmission,
    BodyType Body,
    DrivetrainType Drivetrain,
    decimal EngineVolume,
    int HorsePower,
    string Color,
    string Description,
    string SellerPhone);