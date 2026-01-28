using GoldenEurope.Core.Enums;

namespace GoldenEurope.Business.DTOs;

public record CarSearchDto(
    Guid? BrandId = null,
    Guid? ModelId = null,
    VehicleCategory? Category = null,
    CarCondition? Condition = null,
    int? YearFrom = null,
    int? YearTo = null,
    decimal? PriceFrom = null,
    decimal? PriceTo = null,
    FuelType? Fuel = null,
    TransmissionType? Transmission = null,
    BodyType? Body = null,
    DrivetrainType? Drivetrain = null);