namespace GoldenEurope.Business.DTOs;

public record CarDto(
    Guid Id,
    string Vin,
    string BrandName,
    string ModelName,
    string Category,
    int Year,
    decimal Price,
    int Mileage,
    string Condition,
    string Fuel,
    string Transmission,
    string Body,
    string Drivetrain,
    string Color,
    string Description,
    bool IsSold,
    DateTime? CreatedAt);