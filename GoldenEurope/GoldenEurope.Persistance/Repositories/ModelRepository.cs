using GoldenEurope.Core.Entities;
using GoldenEurope.Core.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace GoldenEurope.Persistance.Repositories;

public class ModelRepository :  IModelRepository
{
    private readonly GoldenEuropeDbContext _context;

    public ModelRepository(GoldenEuropeDbContext context)
    {
        _context = context;
    }
    public async Task<IEnumerable<Model>> GetAllAsync()
    {
        return await _context.Set<Model>()
            .Include(m => m.Brand)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<IEnumerable<Model>> GetByBrandIdAsync(Guid brandId)
    {
        return await _context.Set<Model>()
            .Where(m => m.BrandId == brandId)
            .Include(m => m.Brand)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Model?> GetByIdAsync(Guid id)
    {
        return await _context.Set<Model>()
            .Include(m => m.Brand)
            .FirstOrDefaultAsync(m => m.Id == id);
    }

    public async Task AddAsync(Model model)
    {
        await  _context.Set<Model>().AddAsync(model);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Model model)
    {
        _context.Set<Model>().Update(model);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(Model model)
    {
        _context.Set<Model>().Remove(model);
        await _context.SaveChangesAsync();
    }
}