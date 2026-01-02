using GoldenEurope.Core.Entities;
using GoldenEurope.Core.Enums;
using GoldenEurope.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GoldenEurope.Persistance.Repositories;

public class CarRepository : ICarRepository
{
    private readonly GoldenEuropeDbContext _context;

    public CarRepository(GoldenEuropeDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<Car>> SearchAsync(CarFilter filter)
    {
        var query = _context.Set<Car>()
            .Include(c => c.Model).ThenInclude(m => m.Brand)
            .AsNoTracking()
            .AsQueryable();

        if (filter.ModelId.HasValue)
        {
            query = query.Where(c => c.ModelId == filter.ModelId.Value);
        }
        else if (filter.BrandId.HasValue)
        {
            query = query.Where(c => c.Model.BrandId == filter.BrandId.Value);
        }

        if (filter.Condition.HasValue && filter.Condition != CarCondition.All)
            query = query.Where(c => c.Condition == filter.Condition.Value);
        
        if(filter.YearFrom.HasValue) query = query.Where(c => c.Year >= filter.YearFrom.Value);
        if(filter.YearTo.HasValue) query = query.Where(c => c.Year <= filter.YearTo.Value);
        
        if(filter.PriceFrom.HasValue) query = query.Where(c => c.Price >= filter.PriceFrom.Value);
        if(filter.PriceTo.HasValue) query = query.Where(c => c.Price <= filter.PriceTo.Value);
        
        if(filter.Fuel.HasValue) query = query.Where(c => c.Fuel >= filter.Fuel.Value);
        if(filter.Transmission.HasValue) query = query.Where(c => c.Transmission >= filter.Transmission.Value);
        if(filter.Body.HasValue) query = query.Where(c => c.Body >= filter.Body.Value);
        
        return await query.OrderByDescending(c => c.CreatedAt).ToListAsync();
    }

    public async Task<Car?> GetByIdAsync(Guid id)
    {
        return await _context.Set<Car>()
            .Include(c => c.Model).ThenInclude(m => m.Brand)
            .FirstOrDefaultAsync(c => c.Id == id);
    }

    public async Task AddAsync(Car car)
    {
        await _context.Set<Car>().AddAsync(car);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Car car)
    {
        _context.Set<Car>().Update(car);
        await _context.SaveChangesAsync();
    }
}