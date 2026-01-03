namespace GoldenEurope.Business.DTOs.ModelDto;

public record UpdateModelDto(
    string Name,
    string Category,
    Guid BrandId);