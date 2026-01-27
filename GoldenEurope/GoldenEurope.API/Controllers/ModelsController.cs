using Asp.Versioning;
using GoldenEurope.API.Models;
using GoldenEurope.Business.DTOs.ModelDto;
using GoldenEurope.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GoldenEurope.API.Controllers;
[ApiController]
[ApiVersion(1.0)]
[Route("api/v{version:apiVersion}/[controller]")]
public class ModelsController : ControllerBase
{
    private readonly IModelService _service;
    private readonly ILogger<ModelsController> _logger;

    public ModelsController(IModelService service, ILogger<ModelsController> logger)
    {
        _service = service;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<ModelDto>>), StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResponse<IEnumerable<ModelDto>>>> GetAll()
    {
        _logger.LogInformation("Get all models");
        var result = await _service.GetAllModelsAsync();
        return Ok(ApiResponse<IEnumerable<ModelDto>>.SuccessResult(result));
    }
    [HttpGet("by-brand/{brandId}")]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<ModelDto>>), StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResponse<IEnumerable<ModelDto>>>> GetByBrendId(Guid brendId)
    {
        _logger.LogInformation("Get by BrandId {brandId}", brendId);
        var result = await _service.GetModelByBrandAsync(brendId);
        return Ok(ApiResponse<IEnumerable<ModelDto>>.SuccessResult(result));
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponse<ModelDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ApiResponse<ModelDto>>> GetById(Guid id)
    {
        _logger.LogInformation("Get by id {id}", id);
        var result = await _service.GetModelByIdAsync(id);
        return Ok(ApiResponse<ModelDto>.SuccessResult(result));
    }

    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ApiResponse<object>>> Create([FromBody] CreateModelDto dto)
    {
        _logger.LogInformation("Create model");
        await _service.CreateModelAsync(dto);
        return StatusCode(201, ApiResponse<object>.SuccessResult(null, 201));
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ApiResponse<object>>> Update(Guid id, [FromBody] UpdateModelDto dto)
    {
        _logger.LogInformation("Update model");
        await _service.UpdateModelAsync(id, dto);
        return NoContent();
    }
    
    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status204NoContent)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ApiResponse<object>>> Delete(Guid id)
    {
        _logger.LogInformation("Delete model");
        await _service.DeleteModelAsync(id);
        return NoContent();
    }
}