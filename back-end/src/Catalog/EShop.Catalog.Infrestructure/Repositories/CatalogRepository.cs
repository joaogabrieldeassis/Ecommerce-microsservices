using EShop.Catalog.Domain.Interfaces;
using EShop.Catalog.Domain.Models;
using EShop.Catalog.Infrestructure.Context;
using Microsoft.EntityFrameworkCore;

namespace EShop.Catalog.Infrestructure.Repositories;

public class CatalogRepository(CatalogContext context) : ICatalogRepository
{
    private readonly CatalogContext _context = context;

    public async Task<bool> AddAsync(ProductCatalog entity)
    {
        await _context.CatalogItems.AddAsync(entity);
        return await SaveChangesAsync();
    }

    public async Task<IEnumerable<ProductCatalog>> GetAllAsync()
    {
        return await _context.CatalogItems.Include(x => x.CatalogBrand).ToListAsync();
    }

    public async Task<ProductCatalog?> GetByIdAsync(Guid id)
    {
        return await _context.CatalogItems.Include(x => x.CatalogBrand).FirstOrDefaultAsync(x => x.Id == id);
    }

    private async Task<bool> SaveChangesAsync()
    {
        var result = await _context.SaveChangesAsync();

        return result > 0;
    }
}
