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
    private readonly IPhotoService _photoService;

    public CarService(ICarRepository repository, IMapper mapper, ILogger<CarService> logger, IPhotoService photoService)
    {
        _repository = repository;
        _mapper = mapper;
        _logger = logger;
        _photoService = photoService;
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

        if (dto.Images != null && dto.Images.Count > 0)
        {
            foreach (var file in dto.Images)
            {
                var (url, publicId) = await _photoService.AddPhotoAsync(file);
                
                car.Images.Add(new CarImage
                {
                    Url = url,
                    PublicId = publicId
                });
            }
        }
        await _repository.AddAsync(car);
        
    }

    public async Task UpdateCarAsync(Guid id, UpdateCarDto dto)
    {
        _logger.LogInformation("Updating car", id);
        var existingCar = await _repository.GetByIdAsync(id);
        if(existingCar == null)
            throw new KeyNotFoundException($"Car with id {id} not found");
        _mapper.Map(dto, existingCar);
        await _repository.UpdateAsync(existingCar);
    }

    public async Task DeleteCarAsync(Guid id)
    {
        var car = await _repository.GetByIdAsync(id);
        if(car == null) throw new KeyNotFoundException($"Car with id {id} not found");
        await _repository.DeleteAsync(car);
        
    }

    public async Task IncrementPhoneViewCountAsync(Guid id)
    {
        await _repository.IncrementPhoneViewCountAsync(id);
    }
}