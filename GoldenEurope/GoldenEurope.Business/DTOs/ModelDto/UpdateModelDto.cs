using GoldenEurope.Core.Enums;

namespace GoldenEurope.Business.DTOs.ModelDto;

public record UpdateModelDto(
    string Name,
    VehicleCategory Category,
    Guid BrandId);