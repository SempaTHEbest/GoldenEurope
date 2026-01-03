using GoldenEurope.Business.DTOs.BrandDto;
using GoldenEurope.Core.Entities;

namespace GoldenEurope.Business.Interfaces;

public interface IBrandService
{
    Task<IEnumerable<BrandDto>> GetAllBrandsAsync();
    Task<BrandDto> GetBrandByIdAsync(Guid id);
    Task CreateBrandAsync(CreateBrandDto dto);
    Task UpdateBrandAsync(Guid id, UpdateBrandDto dto);
    Task DeleteBrandAsync(Guid id);
}