using GoldenEurope.Core.Enums;

namespace GoldenEurope.Business.DTOs.ModelDto;

public record CreateModelDto(
    string Name,
    VehicleCategory Category,
    Guid BrandId);