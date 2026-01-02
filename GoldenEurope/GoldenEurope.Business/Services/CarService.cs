using AutoMapper;
using GoldenEurope.Business.DTOs;
using GoldenEurope.Business.Interfaces;
using GoldenEurope.Core.Entities;
using GoldenEurope.Core.Enums;
using GoldenEurope.Core.Interfaces;
using Microsoft.Extensions.Logging;

namespace GoldenEurope.Business.Services;

public class CarService : ICarService
{
    private readonly ICarRepository _repository;
    private readonly IMapper _mapper;
    private readonly ILogger<CarService> _logger;

    public CarService(ICarRepository repository, IMapper mapper, ILogger<CarService> logger)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
    }
    public async Task<IEnumerable<CarDto>> SearchCarsAsync(CarSearchDto dto)
    {
        _logger.LogInformation("Searching cars...", dto);

        var filter = _mapper.Map<CarFilter>(dto);
        var cars = await _repository.SearchAsync(filter);
        return _mapper.Map<IEnumerable<CarDto>>(cars);
    }

    public async Task<CarDto> GetCarByIdAsync(Guid id)
    {
        var car = await _repository.GetByIdAsync(id);
        if(car == null)
            throw new KeyNotFoundException($"Car with id {id} not found");
        return _mapper.Map<CarDto>(car);
    }

    public async Task CreateCarAsync(CreateCarDto dto)
    {
        _logger.LogInformation("Creating car...", dto);
        
        var car = _mapper.Map<Car>(dto);
        car.IsSold = false;
        await _repository.AddAsync(car);
    }
}