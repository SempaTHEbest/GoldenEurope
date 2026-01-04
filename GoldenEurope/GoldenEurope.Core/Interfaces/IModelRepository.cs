using GoldenEurope.Core.Entities;

namespace GoldenEurope.Core.Interfaces;

public interface IModelRepository
{
    Task<IEnumerable<Model>> GetAllAsync();
    Task<IEnumerable<Model>> GetByBrandIdAsync(Guid brandId);
    Task<Model?> GetByIdAsync(Guid id);
    Task AddAsync(Model model);
    Task UpdateAsync(Model model);
    Task DeleteAsync(Model model);
}