using AutoMapper;
using GoldenEurope.Business.DTOs.BrandDto;
using GoldenEurope.Business.Interfaces;
using GoldenEurope.Core.Entities;
using GoldenEurope.Core.Interfaces;
using Microsoft.Extensions.Logging;

namespace GoldenEurope.Business.Services;

public class BrandService : IBrandService
{
    private readonly IBrandRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<BrandService> _logger;

    public BrandService(IBrandRepository repository, IMapper  mapper, ILogger<BrandService> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }
    public async Task<IEnumerable<BrandDto>> GetAllBrandsAsync()
    {
        _logger.LogInformation("Getting all Brands");
        var brand = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<BrandDto>>(brand);
    }

    public async Task<BrandDto> GetBrandByIdAsync(Guid id)
    {
        _logger.LogInformation($"Getting Brand with id {id}");
        var brand =  await _repository.GetByIdAsync(id);
        if (brand == null)
        {
            _logger.LogError($"Brand with id {id} not found");
            throw new KeyNotFoundException($"Brand with id {id} not found");
        }
        return _mapper.Map<BrandDto>(brand);
    }

    public async Task CreateBrandAsync(CreateBrandDto dto)
    {
        _logger.LogInformation("Creating Brand");
        var brand = _mapper.Map<Brand>(dto);
        await _repository.AddAsync(brand);
        _logger.LogInformation($"Brand with id {brand.Id} created");
    }

    public async Task UpdateBrandAsync(Guid id, UpdateBrandDto dto)
    {
        _logger.LogInformation("Updating Brand");
        var brand = await _repository.GetByIdAsync(id);
        if (brand == null)
        {
            _logger.LogError($"Brand with id {id} not found");
            throw new KeyNotFoundException($"Brand with id {id} not found");
        }
        _mapper.Map(dto, brand);
        await _repository.UpdateAsync(brand);
        _logger.LogInformation($"Brand with id {brand.Id} updated");
    }

    public async Task DeleteBrandAsync(Guid id)
    {
        _logger.LogInformation("Deleting Brand");
        var brand = await _repository.GetByIdAsync(id);
        if (brand == null)
        {
            _logger.LogError($"Brand with id {id} not found");
            throw new KeyNotFoundException($"Brand with id {id} not found");
        }
        await _repository.DeleteAsync(brand);
        _logger.LogInformation($"Brand with id {brand.Id} deleted");
    }
}