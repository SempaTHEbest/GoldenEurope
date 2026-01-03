using GoldenEurope.Core.Entities;
using GoldenEurope.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GoldenEurope.Persistance.Repositories;

public class BrandRepository :  IBrandRepository
{
    private readonly GoldenEuropeDbContext _context;

    public BrandRepository(GoldenEuropeDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<Brand>> GetAllAsync()
    {
        return await _context.Set<Brand>().AsNoTracking().ToListAsync();
    }

    public async Task<Brand?> GetByIdAsync(Guid id)
    {
        return await _context.Set<Brand>().FindAsync(id);
    }

    public async Task AddAsync(Brand brand)
    {
        await _context.Set<Brand>().AddAsync(brand);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Brand brand)
    {
        _context.Set<Brand>().Update(brand);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Brand brand)
    {
        _context.Set<Brand>().Remove(brand);
        await _context.SaveChangesAsync();
    }
}