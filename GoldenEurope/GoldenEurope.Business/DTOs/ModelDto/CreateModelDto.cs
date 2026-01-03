namespace GoldenEurope.Business.DTOs.ModelDto;

public record CreateModelDto(
    string Name,
    string Category,
    Guid BrandId);