using AutoMapper;
using GoldenEurope.Business.DTOs.ModelDto;
using GoldenEurope.Business.Interfaces;
using GoldenEurope.Core.Entities;
using GoldenEurope.Core.Interfaces;
using Microsoft.Extensions.Logging;

namespace GoldenEurope.Business.Services;

public class ModelService : IModelService
{
    private readonly IModelRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<ModelService> _logger;

    public ModelService(IModelRepository repository, IMapper mapper, ILogger<ModelService> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }
    public async Task<IEnumerable<ModelDto>> GetAllModelsAsync()
    {
        _logger.LogInformation("Getting all Models");
        var models = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<ModelDto>>(models);
    }

    public async Task<IEnumerable<ModelDto>> GetModelByBrandAsync(Guid brandId)
    {
        _logger.LogInformation("Getting models by Brand");
        var models = await _repository.GetByBrandIdAsync(brandId);
        return _mapper.Map<IEnumerable<ModelDto>>(models);
    }

    public async Task<ModelDto> GetModelByIdAsync(Guid id)
    {
        _logger.LogInformation("Getting model by id");
        var model = await _repository.GetByIdAsync(id);
        if (model == null)
        {
            _logger.LogWarning($"Model with id {id} not found");
            throw new KeyNotFoundException($"Model with id {id} not found");
        }
        return _mapper.Map<ModelDto>(model);
    }

    public async Task CreateModelAsync(CreateModelDto dto)
    {
        _logger.LogInformation("Creating new model");
        var model = _mapper.Map<Model>(dto);
        await _repository.AddAsync(model);
        _logger.LogInformation($"Model with id {model.Id} created");
    }

    public async Task UpdateModelAsync(Guid id, UpdateModelDto dto)
    {
        _logger.LogInformation("Updating model");
        var model = await _repository.GetByIdAsync(id);
        if (model == null)
        {
            _logger.LogWarning("Model with id {id} not found");
            throw new KeyNotFoundException($"Model with id {id} not found");
        }
        _mapper.Map(dto, model);
        await _repository.UpdateAsync(model);
    }

    public async Task DeleteModelAsync(Guid id)
    {
        _logger.LogInformation("Deleting model");
        var model = await _repository.GetByIdAsync(id);
        if (model == null)
        {
            _logger.LogWarning("Model with id {id} not found");
            throw new KeyNotFoundException($"Model with id {id} not found");
        }
        await _repository.DeleteAsync(model);
        _logger.LogInformation($"Model with id {id} deleted");
    }
}