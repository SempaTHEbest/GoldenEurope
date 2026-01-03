namespace GoldenEurope.Business.DTOs.BrandDto;

public record BrandDto(
    Guid Id,
    string? Name,
    string? Country);