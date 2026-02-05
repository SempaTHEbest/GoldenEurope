using GoldenEurope.Core.Enums;
using Microsoft.AspNetCore.Http;

namespace GoldenEurope.Business.DTOs;

public record UpdateCarDto(
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
    string? Color,
    string? Description,
    string SellerPhone,
    bool IsSold,
    IFormFileCollection? Images);