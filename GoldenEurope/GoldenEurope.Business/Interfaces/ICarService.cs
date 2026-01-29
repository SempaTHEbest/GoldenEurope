using GoldenEurope.Business.DTOs;

namespace GoldenEurope.Business.Interfaces;

public interface ICarService
{
    Task<IEnumerable<CarDto>> SearchCarsAsync(CarSearchDto dto);
    Task<CarDto> GetCarByIdAsync(Guid id);
    Task CreateCarAsync(CreateCarDto dto);
    Task UpdateCarAsync(Guid id, UpdateCarDto dto);
    Task DeleteCarAsync(Guid id);
    Task IncrementPhoneViewCountAsync(Guid id);
}