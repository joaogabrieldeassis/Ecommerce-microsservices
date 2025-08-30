using EShop.Catalog.Domain.Interfaces;
using EShop.Catalog.Domain.Models;
using EShop.Catalog.Infrestructure.Context;
using Microsoft.EntityFrameworkCore;

namespace EShop.Catalog.Infrestructure.Repositories;

public class CatalogRepository(CatalogContext context) : ICatalogRepository
{
    private readonly CatalogContext _context = context;

    public async Task<IEnumerable<ProductCatalog>> GetAllAsync()
    {
        return await _context.CatalogItems.AsNoTracking().Include(x => x.ProductCatalogBrand).ToListAsync();
    }

    public async Task<ProductCatalog?> GetByIdAsync(Guid id)
    {
        return await _context.CatalogItems.Include(x => x.ProductCatalogBrand).FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<bool> AddAsync(ProductCatalog entity)
    {
        await _context.CatalogItems.AddAsync(entity);
        return await SaveChangesAsync();
    }

    public async Task UpdateAsync()
    {
        await SaveChangesAsync();
    }

    public async Task<IEnumerable<ProductCatalogBrand>> GetAllProductsCatalogBranchAsync()
    {
        return await _context.CatalogBrands.AsNoTracking().ToListAsync();
    }

    private async Task<bool> SaveChangesAsync()
    {
        var result = await _context.SaveChangesAsync();

        return result > 0;
    }    
}