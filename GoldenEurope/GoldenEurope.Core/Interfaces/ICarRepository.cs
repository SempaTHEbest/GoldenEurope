using GoldenEurope.Core.Entities;
using GoldenEurope.Core.Enums;

namespace GoldenEurope.Core.Interfaces;

public interface ICarRepository
{
    Task<IEnumerable<Car>> SearchAsync(CarFilter filter);
    Task<Car?> GetByIdAsync(Guid id);
    Task AddAsync(Car car);
    Task UpdateAsync(Car car);
}