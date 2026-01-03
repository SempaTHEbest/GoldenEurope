namespace GoldenEurope.Business.DTOs.ModelDto;

public record ModelDto(
    Guid Id,
    string Name,
    string Category,
    string BrandName,
    Guid BrandId);