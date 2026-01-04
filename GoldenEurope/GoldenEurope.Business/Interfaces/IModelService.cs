using GoldenEurope.Business.DTOs.ModelDto;

namespace GoldenEurope.Business.Interfaces;

public interface IModelService
{
    Task<IEnumerable<ModelDto>> GetAllModelsAsync();
    Task<IEnumerable<ModelDto>> GetModelByBrandAsync(Guid brandId); 
    Task<ModelDto> GetModelByIdAsync(Guid id);
    Task CreateModelAsync(CreateModelDto dto);
    Task  UpdateModelAsync(Guid id, UpdateModelDto dto);
    Task DeleteModelAsync(Guid id);
}