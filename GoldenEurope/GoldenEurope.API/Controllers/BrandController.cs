using Asp.Versioning;
using GoldenEurope.API.Models;
using GoldenEurope.Business.DTOs.BrandDto;
using GoldenEurope.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GoldenEurope.API.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/brands")]
public class BrandController : ControllerBase
{
    private readonly IBrandService _service;
    private readonly ILogger<BrandController> _logger;

    public BrandController(IBrandService service, ILogger<BrandController> logger)
    {
        _service = service;
        _logger = logger;
    }

    [HttpGet]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<BrandDto>>), StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResponse<IEnumerable<BrandDto>>>> GetAll()
    {
        _logger.LogInformation("Getting all Brands");
        var result = await _service.GetAllBrandsAsync();
        return Ok(ApiResponse<IEnumerable<BrandDto>>.SuccessResult(result));
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponse<BrandDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ApiResponse<BrandDto>>> Get(Guid id)
    {
        _logger.LogInformation("Fetching brand name with id", id);
        var result = await _service.GetBrandByIdAsync(id);
        return Ok(ApiResponse<BrandDto>.SuccessResult(result));
    }

    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ApiResponse<object>>> Create([FromBody] CreateBrandDto dto)
    {
        _logger.LogInformation("Creating brand");
        await _service.CreateBrandAsync(dto);
        return StatusCode(201, ApiResponse<object>.SuccessResult(null, 201));
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status204NoContent)]
    public async Task<ActionResult<ApiResponse<object>>> Update(Guid id, [FromBody] UpdateBrandDto dto)
    {
        _logger.LogInformation("Updating brand");
        await _service.UpdateBrandAsync(id, dto);
        return NoContent();
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ApiResponse<object>>> Delete(Guid id)
    {
        _logger.LogInformation("Deleting brand");
        await _service.DeleteBrandAsync(id);
        return NoContent();
    }
    
}