using Asp.Versioning;
using GoldenEurope.API.Models;
using GoldenEurope.Business.DTOs;
using GoldenEurope.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace GoldenEurope.API.Controllers;

[ApiController]
[ApiVersion("1.0")]
[Route("api/v{version:apiVersion}/[controller]")]
public class CarsController : ControllerBase
{
    private readonly ICarService _carService;
    private readonly ILogger<CarsController> _logger;

    public CarsController(ICarService carService, ILogger<CarsController> logger)
    {
        _carService = carService;
        _logger = logger;
    }

    [HttpGet("search")]
    [ProducesResponseType(typeof(ApiResponse<IEnumerable<CarDto>>), StatusCodes.Status200OK)]
    public async Task<ActionResult<ApiResponse<IEnumerable<CarDto>>>> Search([FromQuery] CarSearchDto searchDto)
    {
        var result = await _carService.SearchCarsAsync(searchDto);
        return Ok(ApiResponse<IEnumerable<CarDto>>.SuccessResult(result));
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(ApiResponse<CarDto>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ApiResponse<CarDto>>> GetById(Guid id)
    {
        var result = await _carService.GetCarByIdAsync(id);
        return Ok(ApiResponse<CarDto>.SuccessResult(result));
    }

    [HttpPost]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse<object>), StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ApiResponse<object>>> Create([FromBody] CreateCarDto createDto)
    {
        await _carService.CreateCarAsync(createDto);
        return StatusCode(201, ApiResponse<object>.SuccessResult(null, 201));
    }
}